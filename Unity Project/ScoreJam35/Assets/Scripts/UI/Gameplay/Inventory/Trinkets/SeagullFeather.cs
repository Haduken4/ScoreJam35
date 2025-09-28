using UnityEngine;

public class SeagullFeather : BaseItem
{
    int NewMinimum = 2;

    public override void ActivateItem()
    {
        GlobalGameData.MinimumDieRoll = NewMinimum;
    }
}
