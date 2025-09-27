using UnityEngine;
using TMPro;

public class DieFaceDisplay : MonoBehaviour
{
    TextMeshProUGUI text = null;
    DieLogic dieLogic = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        dieLogic = GetComponent<DieLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = dieLogic.CurrentValue.ToString();
    }
}
