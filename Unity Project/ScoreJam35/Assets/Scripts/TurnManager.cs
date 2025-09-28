using System.Resources;
using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance = null;

    public event Action OnEndOfTurn;

    public DiceManager DiceHandManager = null;

    public GameObject UniversalActionsParent;
    public GameObject DaytimeActionsParent;
    public GameObject NightTimeActionsParent;

    public int FoodCostPerTurn = 4;
    public int WaterCostPerTurn = 4;

    public int HealthLossPerColdNight = 5;

    public float TimeBetweenTurns = 5;
    public float InitialTimer = 1.0f;

    public bool CampfireStartedTonight = false;

    float timer = 0;
    bool betweenTurns = false;
    int turn = 0;

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
            // Should be guaranteed shrunk alrdy
            action.StartScaleIn();
            action.Useable = true;
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

        OnEndOfTurn?.Invoke();
    }

    public void OnFinish()
    {
        DiceHandManager.DiscardDice();
        finished = true;
    }
}
