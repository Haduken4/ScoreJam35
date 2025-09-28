using System.Collections.Generic;
using UnityEngine;

public class SharkToothNecklace : BaseItem
{
    public int Increase = 1;

    protected override void Start()
    {
        base.Start();

        GameObject huntingAction = GameObject.Find("GoHuntingAction");
        huntingAction.GetComponent<BaseDiceAction>().OnActionPerformed += OnHuntingAction;
    }

    public override void ActivateItem()
    {
        // Doesn't need to do anything
    }

    void OnHuntingAction()
    {
        List<Transform> dice = DiceManager.Instance.GetDice();

        DieLogic randomDie = dice[Random.Range(0, dice.Count)].GetComponent<DieLogic>();

        randomDie.ChangeDie(Increase);
        randomDie.GetComponent<DieFaceDisplay>().Buffed = true;
    }
}
