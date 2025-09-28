using UnityEngine;

public class IslandCurseTrigger : SpecialEventTrigger
{
    public int ResourceCostIncrease = 0;

    public override void StartEvent()
    {
        TurnManager.Instance.FoodCostPerTurn += ResourceCostIncrease;
        TurnManager.Instance.WaterCostPerTurn += ResourceCostIncrease;
    }

    public override void EndEvent()
    {
        TurnManager.Instance.FoodCostPerTurn -= ResourceCostIncrease;
        TurnManager.Instance.WaterCostPerTurn -= ResourceCostIncrease;
    }
}
