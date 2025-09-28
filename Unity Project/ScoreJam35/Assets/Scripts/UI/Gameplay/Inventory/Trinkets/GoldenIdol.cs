using System.Collections.Generic;
using UnityEngine;

public class GoldenIdol : BaseItem
{
    public int IncreaseValue = 1;

    protected override void Start()
    {
        base.Start();
        DiceManager.Instance.OnDiceInPlay += OnDiceInPlay;
    }

    public override void ActivateItem()
    {
        // Doesn't need to do anything
    }

    void OnDiceInPlay()
    {
        List<Transform> dice = DiceManager.Instance.GetDice();
        DieLogic lowest = null;

        foreach(Transform die in dice)
        {
            DieLogic logic = die.GetComponent<DieLogic>();

            if(lowest == null || lowest.CurrentValue > logic.CurrentValue)
            {
                lowest = logic;
            }
        }

        lowest.ChangeDie(IncreaseValue);
        lowest.GetComponent<DieFaceDisplay>().Buffed = true;
        // TODO: Play some vfx on the die
    }
}
