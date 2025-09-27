using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class DieFaceDisplay : MonoBehaviour
{
    public List<Sprite> FaceSprites = new List<Sprite>();

    public Image TargetImage = null;
    DieLogic dieLogic = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dieLogic = GetComponent<DieLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetImage.sprite = FaceSprites[dieLogic.CurrentValue - 1];
    }
}
