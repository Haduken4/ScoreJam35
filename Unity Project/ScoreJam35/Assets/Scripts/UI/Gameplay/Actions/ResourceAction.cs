using UnityEngine;

public class ResourceAction : BaseDiceAction
{
    public E_Resource ResourceToChange = E_Resource.FOOD;
    public float DieValueMultiplier = 1;
    public int ExtraFlatValue = 0;

    public override void PerformDiceValueAction(int totalValue)
    {
        base.PerformDiceValueAction(totalValue);
        PlayerResourceManager.Instance.ChangeResource(ResourceToChange, (int)(totalValue * DieValueMultiplier) + ExtraFlatValue);
    }
}
