using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEventPopup : MonoBehaviour
{
    public TextMeshProUGUI TitleText = null;
    public TextMeshProUGUI DescriptionText = null;
    public Image IconImage = null;
    public GameObject EventToEnable = null;

    public void EnableEvent()
    {
        EventToEnable.SetActive(true);

        SlotGroup[] eventActions = EventToEnable.GetComponentsInChildren<SlotGroup>();
        foreach(SlotGroup action in eventActions)
        {
            action.StartScaleIn();
        }
    }
}
