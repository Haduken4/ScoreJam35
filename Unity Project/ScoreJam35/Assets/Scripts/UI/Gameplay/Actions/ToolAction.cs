using UnityEngine;

public class ToolAction : BaseDiceAction
{
    public override void PerformDiceValueAction(int totalValue)
    {
        base.PerformDiceValueAction(totalValue);

        InventoryManager.Instance.GainNewTool();
    }
}
