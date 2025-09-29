using System.Collections.Generic;
using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance = null;

    public event Action OnEndOfTurn;
    public event Action OnStartOfTurn;
    public event Action OnStartDay;
    public event Action OnEndDay;
    public event Action OnStartNight;
    public event Action OnEndNight;
    public event Action OnStartedRaining;
    public event Action OnFinishedRaining;

    public DiceManager DiceHandManager = null;

    [Header("Basic Action Parents")]
    public GameObject UniversalActionsParent;
    public GameObject DaytimeActionsParent;
    public GameObject NightTimeActionsParent;

    [Header("Special Events")]
    public List<SpecialEventInfo> SpecialEventParents = new List<SpecialEventInfo>();
    public SpecialEventPopup EventPopup = null;
    public Vector2Int FirstEventTurnRange = new Vector2Int(6, 8);
    public Vector2Int TurnsBetweenEventsRange = new Vector2Int(3, 5);

    [Header("Upkeep")]
    public int FoodCostPerTurn = 4;
    public int WaterCostPerTurn = 4;
    public int HealthLossPerColdNight = 5;

    [Header("Timing")]
    public float TimeBetweenTurns = 5;
    public float InitialTimer = 1.0f;
    public ColorTransitionCycle DayNightColors = null;

    [Header("Data")]
    public bool CampfireStartedTonight = false;
    public GameObject CampfireObject = null;
    public bool CurrentlyRaining = false;

    float timer = 0;
    bool betweenTurns = false;
    int turn = 0;
    int turnsUntilEvent = 0;
    int lastEvent = -1;

    bool finished = false;

    private void Awake()
    {
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalGameData.ResetToDefault();

        betweenTurns = true;
        timer = InitialTimer;
        turnsUntilEvent = UnityEngine.Random.Range(FirstEventTurnRange.x, FirstEventTurnRange.y + 1);
        DayNightColors.StartTransition(TimeBetweenTurns, DayNightColors.DaytimePoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            return;
        }

        if (betweenTurns)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                OnStartTurn();
                betweenTurns = false;
            }
        }
    }

    public void OnStartTurn()
    {
        DiceHandManager.StartTurn();

        if (turnsUntilEvent <= 0)
        {
            // TODO: Trigger an event :)
            int eventToTrigger = lastEvent;
            while (eventToTrigger == lastEvent)
            {
                eventToTrigger = UnityEngine.Random.Range(0, SpecialEventParents.Count);
            }
            lastEvent = eventToTrigger;

            EventPopup.gameObject.SetActive(true);
            SpecialEventInfo info = SpecialEventParents[0];
            EventPopup.EventToEnable = info.gameObject;
            EventPopup.TitleText.text = info.Title;
            EventPopup.DescriptionText.text = info.Description;
            EventPopup.IconImage.sprite = info.Icon;

            turnsUntilEvent = UnityEngine.Random.Range(TurnsBetweenEventsRange.x, TurnsBetweenEventsRange.y + 1);
        }

        SlotGroup[] universalActions = UniversalActionsParent.GetComponentsInChildren<SlotGroup>();
        foreach(SlotGroup action in universalActions)
        {
            action.UnslotAllDie();
            if(action.transform.localScale != action.BaseScale)
            {
                action.StartScaleIn();
                action.Useable = true;
            }
        }

        SlotGroup[] timeSpecificActions = turn % 2 == 0 ? DaytimeActionsParent.GetComponentsInChildren<SlotGroup>() : NightTimeActionsParent.GetComponentsInChildren<SlotGroup>();
        foreach (SlotGroup action in timeSpecificActions)
        {
            action.UnslotAllDie();
            if(action.InactiveDuringRain && CurrentlyRaining)
            {
                continue;
            }
            // Should be guaranteed shrunk alrdy
            action.StartScaleIn();
            action.Useable = true;
        }

        OnStartOfTurn?.Invoke();
        if(turn % 2 == 0)
        {
            OnStartDay?.Invoke();
            CampfireObject.SetActive(false);
        }
        else
        {
            OnStartNight?.Invoke();
        }

    }

    public void OnEndTurn()
    {
        if (betweenTurns || finished)
        {
            return;
        }

        SlotGroup[] timeSpecificActions = turn % 2 == 0 ? DaytimeActionsParent.GetComponentsInChildren<SlotGroup>() : NightTimeActionsParent.GetComponentsInChildren<SlotGroup>();
        foreach (SlotGroup action in timeSpecificActions)
        {
            action.UnslotAllDie();
            if(action.transform.localScale != Vector3.zero)
            {
                action.StartShrinking(true);
            }
        }

        PlayerResourceManager.Instance.ChangeFood(-FoodCostPerTurn);
        PlayerResourceManager.Instance.ChangeWater(-WaterCostPerTurn);

        if (turn % 2 == 1)
        {
            if (!CampfireStartedTonight)
            {
                PlayerResourceManager.Instance.ChangeHealth(-HealthLossPerColdNight);
            }
            CampfireStartedTonight = false;
        }

        int total = PlayerResourceManager.Instance.GetFood() + PlayerResourceManager.Instance.GetWater() + PlayerResourceManager.Instance.GetHealth();
        GlobalGameData.Score += total;

        DiceHandManager.EndTurn();
        betweenTurns = true;
        timer = TimeBetweenTurns;
        ++turn;
        if (!AnySpecialEventActive())
        {
            --turnsUntilEvent;
        }

        // Next turn is day
        if (turn % 2 == 0)
        {
            OnEndNight?.Invoke();
            DayNightColors.StartTransition(TimeBetweenTurns, DayNightColors.DaytimePoint);
        }
        else // Next turn is night
        {
            OnEndDay?.Invoke();
            DayNightColors.StartTransition(TimeBetweenTurns, DayNightColors.NightTimePoint);
        }

        OnEndOfTurn?.Invoke();
    }

    public void OnFinish()
    {
        DiceHandManager.DiscardDice();
        finished = true;
    }

    public bool AnySpecialEventActive()
    {
        foreach(SpecialEventInfo specialEvent in SpecialEventParents)
        {
            if (specialEvent.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    public void InvokeStartRaining()
    {
        OnStartedRaining?.Invoke();
    }

    public void InvokeEndRaining()
    {
        OnFinishedRaining?.Invoke();
    }

    public int GetCurrentDay()
    {
        return (turn / 2) + 1;
    }

    public bool IsCurrentlyDay()
    {
        return turn % 2 == 0;
    }
}
