using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayDisplayText : MonoBehaviour
{
    public Image TargetImage = null;
    public TextMeshProUGUI TextTarget = null;

    public Sprite DayIcon = null;
    public Sprite NightIcon = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnManager.Instance.OnStartDay += StartDay;
        TurnManager.Instance.OnStartNight += StartNight;
    }

    public void StartDay()
    {
        TargetImage.sprite = DayIcon;
        TextTarget.text = TurnManager.Instance.GetCurrentDay().ToString();
    }

    void StartNight()
    {
        TargetImage.sprite = NightIcon;
        TextTarget.text = TurnManager.Instance.GetCurrentDay().ToString();
    }
}
