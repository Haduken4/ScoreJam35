using UnityEngine;
using TMPro;

public class ScoreDisplayText : MonoBehaviour
{
    public bool DisplaysHighScore = false;
    TextMeshProUGUI text = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (DisplaysHighScore)
        {
            if(GlobalGameData.PersonalBest < GlobalGameData.Score)
            {
                GlobalGameData.PersonalBest = GlobalGameData.Score;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int toDisplay = DisplaysHighScore ? GlobalGameData.PersonalBest : GlobalGameData.Score;
        text.text = toDisplay.ToString();
    }
}
