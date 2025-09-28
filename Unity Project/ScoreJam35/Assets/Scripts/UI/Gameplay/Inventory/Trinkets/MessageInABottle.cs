using UnityEngine;
using UnityEngine.UI;

public class MessageInABottle : BaseItem
{
    public float UsedOpacity = 0.5f;
    public bool ResetPerDay = true;

    bool useable = true;

    Image img = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        img = GetComponent<Image>();
        TurnManager.Instance.OnStartDay += OnStartDay;
    }

    public override void ActivateItem()
    {
        // Does nothing
    }

    protected override void ActivateDieEffect(DieLogic die)
    {
        if (!useable)
        {
            return;
        }

        base.ActivateDieEffect(die);

        die.RollDie();
        die.GetComponent<DieFaceDisplay>().Buffed = true;

        Color c = img.color;
        c.a = UsedOpacity;
        img.color = c;
        useable = false;
    }

    void OnStartDay()
    {
        Color c = img.color;
        c.a = 1;
        img.color = c;
        useable = true;
    }
}
