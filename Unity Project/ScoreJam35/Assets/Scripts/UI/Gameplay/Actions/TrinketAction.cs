using UnityEngine;

public class TrinketAction : BaseDiceAction
{
    public override void PerformDiceValueAction(int totalValue)
    {
        base.PerformDiceValueAction(totalValue);

        InventoryManager.Instance.GainNewTrinket();
    }
}
