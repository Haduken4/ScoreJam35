using UnityEngine;

public class ValueRangeSlot : BaseDieSlot
{
    public Vector2 AcceptableRange = new Vector2(1, 6);

    public override bool AttemptApplyDie(DieLogic die)
    {
        if (!base.AttemptApplyDie(die))
        {
            return false;
        }


        return (die.CurrentValue >= AcceptableRange.x) && (die.CurrentValue <= AcceptableRange.y);
    }
}
