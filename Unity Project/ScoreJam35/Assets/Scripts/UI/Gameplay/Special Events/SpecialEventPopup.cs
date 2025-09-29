using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEventPopup : MonoBehaviour
{
    public TextMeshProUGUI TitleText = null;
    public TextMeshProUGUI DescriptionText = null;
    public Image IconImage = null;
    public GameObject EventToEnable = null;
    public AudioPlayer Player = null;

    public void EnableEvent()
    {
        EventToEnable.SetActive(true);

        if (Player != null)
        {
            Player.PlaySound();
        }

        SlotGroup[] eventActions = EventToEnable.GetComponentsInChildren<SlotGroup>();
        foreach(SlotGroup action in eventActions)
        {
            action.StartScaleIn();
            SpecialEventTrigger trigger = action.GetComponent<SpecialEventTrigger>();
            if (trigger != null)
            {
                trigger.StartEvent();
            }
        }
    }
}
