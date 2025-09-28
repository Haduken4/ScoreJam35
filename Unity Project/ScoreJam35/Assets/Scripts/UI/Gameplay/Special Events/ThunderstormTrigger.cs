using UnityEngine;

public class ThunderstormTrigger : SpecialEventTrigger
{
    public override void StartEvent()
    {
        TurnManager.Instance.CurrentlyRaining = true;
        TurnManager.Instance.InvokeStartRaining();
    }

    public override void EndEvent()
    {
        TurnManager.Instance.CurrentlyRaining = false;
        TurnManager.Instance.InvokeEndRaining();
    }
}
