using UnityEngine;

public class CampfireAction : BaseDiceAction
{
    public GameObject CampfireObject = null;

    public override void PerformDiceValueAction(int totalValue)
    {
        base.PerformDiceValueAction(totalValue);
        TurnManager.Instance.CampfireStartedTonight = true;
        CampfireObject.SetActive(true);
    }
}
