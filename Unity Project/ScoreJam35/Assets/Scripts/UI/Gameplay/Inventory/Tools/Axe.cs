using TMPro;
using UnityEngine;

public class Axe : BaseItem
{
    public string NewText = "";
    public int Increase = 1;

    public override void ActivateItem()
    {
        GameObject WoodAction = GameObject.Find("GatherWoodAction");
        WoodAction.GetComponent<ResourceAction>().ExtraFlatValue += Increase;

        TextMeshProUGUI[] texts = WoodAction.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in texts)
        {
            if(text.gameObject.name == "DescriptionText")
            {
                text.text = NewText;
            }
        }
    }
}
