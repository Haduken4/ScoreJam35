using System.Collections.Generic;
using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance = null;

    public event Action OnEndOfTurn;
    public event Action OnStartOfTurn;
    public event Action OnStartDay;
    public event Action OnStartNight;

    public DiceManager DiceHandManager = null;

    [Header("Basic Action Parents")]
    public GameObject UniversalActionsParent;
    public GameObject DaytimeActionsParent;
    public GameObject NightTimeActionsParent;

    [Header("Special Events")]
    public List<GameObject> SpecialEventPopups = new List<GameObject>();
    public Vector2Int FirstEventTurnRange = new Vector2Int(6, 8);
    public Vector2Int TurnsBetweenEventsRange = new Vector2Int(4, 6);

    [Header("Upkeep")]
    public int FoodCostPerTurn = 4;
    public int WaterCostPerTurn = 4;
    public int HealthLossPerColdNight = 5;

    [Header("Timing")]
    public float TimeBetweenTurns = 5;
    public float InitialTimer = 1.0f;

    [Header("Data")]
    public bool CampfireStartedTonight = false;
    public bool CurrentlyRaining = false;

    float timer = 0;
    bool betweenTurns = false;
    int turn = 0;
    int turnsUntilEvent = 0;

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
        betweenTurns = true;
        timer = InitialTimer;
        turnsUntilEvent = UnityEngine.Random.Range(FirstEventTurnRange.x, FirstEventTurnRange.y + 1);
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

        DiceHandManager.EndTurn();
        betweenTurns = true;
        timer = TimeBetweenTurns;
        ++turn;
        --turnsUntilEvent;

        OnEndOfTurn?.Invoke();
    }

    public void OnFinish()
    {
        DiceHandManager.DiscardDice();
        finished = true;
    }
}
