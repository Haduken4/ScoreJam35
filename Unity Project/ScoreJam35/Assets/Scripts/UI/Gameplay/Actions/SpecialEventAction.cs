using System.Text.RegularExpressions;
using UnityEngine;

public class SpecialEventAction : BaseDiceAction
{
    public bool EndsEvent = true;

    SlotGroup group = null;
    SpecialEventTrigger trigger = null;

    private void Start()
    {
        group = GetComponent<SlotGroup>();
        trigger = GetComponent<SpecialEventTrigger>();
    }

    public override void PerformDiceValueAction(int totalValue)
    {
        if (EndsEvent)
        {
            trigger.EndEvent();
            group.StartShrinking(true, true);
        }
    }
}
