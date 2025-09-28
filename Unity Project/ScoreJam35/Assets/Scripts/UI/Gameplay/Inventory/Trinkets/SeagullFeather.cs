using UnityEngine;

public class SeagullFeather : BaseItem
{
    public int NewMinimum = 2;

    public override void ActivateItem()
    {
        GlobalGameData.MinimumDieRoll = NewMinimum;
    }
}
