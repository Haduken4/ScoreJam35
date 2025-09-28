using UnityEngine;

public class FirePit : BaseItem
{
    public override void ActivateItem()
    {
        GameObject buffedNightActions = GameObject.Find("NighttimeActionsBuffed");
        TurnManager.Instance.NightTimeActionsParent = buffedNightActions;
    }
}
