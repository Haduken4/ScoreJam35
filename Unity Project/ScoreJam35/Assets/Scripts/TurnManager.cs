using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public DiceManager DiceHandManager = null;

    public GameObject UniversalActionsParent;
    public GameObject DaytimeActionsParent;
    public GameObject NightTimeActionsParent;

    public float TimeBetweenTurns = 5;
    public float InitialTimer = 1.0f;

    float timer = 0;
    bool betweenTurns = false;
    int turn = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        betweenTurns = true;
        timer = InitialTimer;
    }

    // Update is called once per frame
    void Update()
    {
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
            }
        }

        SlotGroup[] timeSpecificActions = turn % 2 == 0 ? DaytimeActionsParent.GetComponentsInChildren<SlotGroup>() : NightTimeActionsParent.GetComponentsInChildren<SlotGroup>();
        foreach (SlotGroup action in timeSpecificActions)
        {
            action.UnslotAllDie();
            // Should be guaranteed shrunk alrdy
            action.StartScaleIn();
        }
    }

    public void OnEndTurn()
    {
        if (betweenTurns)
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

        DiceHandManager.EndTurn();
        betweenTurns = true;
        timer = TimeBetweenTurns;
        ++turn;
    }
}
