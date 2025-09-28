using TMPro;
using UnityEngine;

public class Spear : BaseItem
{
    public string NewText = "";
    public int Increase = 1;

    public override void ActivateItem()
    {
        GameObject huntingAction = GameObject.Find("GoHuntingAction");
        GameObject fishingAction = GameObject.Find("GoFishingAction");

        EditAction(huntingAction);
        EditAction(fishingAction);
    }

    void EditAction(GameObject action)
    {
        action.GetComponent<ResourceAction>().ExtraFlatValue += Increase;

        TextMeshProUGUI[] texts = action.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in texts)
        {
            if (text.gameObject.name == "DescriptionText")
            {
                text.text = NewText;
            }
        }
    }
}
