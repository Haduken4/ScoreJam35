using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public int DicePerTurn = 4;
    public GameObject DicePrefab = null;
    public Vector2 DiceStartPos = Vector2.zero;
    public float DiceXSpacing = 200;
    public float DiceZ = 0;
    public float InPlayYPos = 0;
    public float InPlayTime = 1.0f;
    public float DiceLerpSpeed = 5.0f;

    List<Transform> dice = new List<Transform>();
    float timer = 0;
    bool gettingInPlay = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gettingInPlay)
        {
            UpdateDicePos();
        }
        else
        {
            UpdateDicePosInPlay();
        }
    }

    void UpdateDicePos()
    {
        for(int i = 0; i < dice.Count; ++i)
        {
            Vector3 correctPos = DiceStartPos + (Vector2.right * DiceXSpacing * i);
            correctPos.y = InPlayYPos;
            correctPos.z = DiceZ;

            Vector3 diePos = dice[i].position;
            diePos = Vector3.Lerp(diePos, correctPos, DiceLerpSpeed * Time.deltaTime);
            dice[i].position = diePos;
        }
    }

    void UpdateDicePosInPlay()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 0;
            gettingInPlay = false;
        }

        foreach (Transform die in dice)
        {
            float currY = Mathf.Lerp(DiceStartPos.y, InPlayYPos, 1.0f - (timer / InPlayTime));
            Vector3 diePos = die.position;
            diePos.y = currY;
            die.position = diePos;
        }
    }

    public void StartTurn()
    {
        for(int i = 0; i < DicePerTurn; i++)
        {
            Vector3 pos = DiceStartPos + (Vector2.right * DiceXSpacing * i);
            pos.z = DiceZ;

            GameObject die = Instantiate(DicePrefab, pos, Quaternion.identity, transform);
        }

        gettingInPlay = true;
        timer = InPlayTime;
    }

    public void EndTurn()
    {
        DiscardDice();
    }

    public void ReAddDie(GameObject toAdd)
    {
        dice.Add(toAdd.transform);
    }

    public void DiscardDice()
    {
        foreach (Transform die in dice)
        {
            // just kill em
            Destroy(die.gameObject);
        }
    }
}
