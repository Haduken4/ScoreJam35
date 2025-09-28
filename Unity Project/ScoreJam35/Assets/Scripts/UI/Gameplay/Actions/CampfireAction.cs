using UnityEngine;

public class CampfireAction : BaseDiceAction
{
    public override void PerformDiceValueAction(int totalValue)
    {
        TurnManager.Instance.CampfireStartedTonight = true;
    }
}
