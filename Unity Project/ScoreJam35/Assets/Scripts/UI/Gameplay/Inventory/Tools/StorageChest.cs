using UnityEngine;

public class StorageChest : BaseItem
{
    public int NewMaxFood = 40;
    public int NewMaxWater = 40;

    public override void ActivateItem()
    {
        PlayerResourceManager.Instance.MaxFood = NewMaxFood;
        PlayerResourceManager.Instance.MaxWater = NewMaxWater;
        PlayerResourceManager.Instance.UpdateAllDisplays();
    }
}
