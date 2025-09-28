using UnityEngine;

public class CampfireAction : BaseDiceAction
{
    public override void PerformDiceValueAction(int totalValue)
    {
        base.PerformDiceValueAction(totalValue);
        TurnManager.Instance.CampfireStartedTonight = true;
    }
}
