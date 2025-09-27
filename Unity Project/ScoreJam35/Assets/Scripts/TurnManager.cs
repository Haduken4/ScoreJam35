using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public DiceManager DiceHandManager = null;

    public float TimeBetweenTurns = 5;
    public float InitialTimer = 1.0f;

    float timer = 0;
    bool betweenTurns = false;

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
    }

    public void OnEndTurn()
    {
        if (betweenTurns)
        {
            return;
        }

        DiceHandManager.EndTurn();
        betweenTurns = true;
        timer = TimeBetweenTurns;
    }
}
