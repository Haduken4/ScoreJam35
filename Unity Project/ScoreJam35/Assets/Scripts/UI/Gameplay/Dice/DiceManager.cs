using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; } = null;

    public event Action OnDiceInPlay;

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

    void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        VerifyDice();

        if (!gettingInPlay)
        {
            UpdateDicePos();
        }
        else
        {
            UpdateDicePosInPlay();
        }
    }

    void VerifyDice()
    {
        for(int i = 0; i < dice.Count;)
        {
            Transform die = dice[i];
            if(die == null || die.parent != transform)
            {
                dice.RemoveAt(i);
                continue;
            }
            ++i;
        }
    }

    void UpdateDicePos()
    {
        for(int i = 0; i < dice.Count; ++i)
        {
            Vector3 correctPos = DiceStartPos + (Vector2.right * DiceXSpacing * i);
            correctPos.y = InPlayYPos;
            correctPos.z = transform.position.z + DiceZ;

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
            OnDiceInPlay?.Invoke();
        }

        foreach (Transform die in dice)
        {
            float currY = Mathf.Lerp(DiceStartPos.y, InPlayYPos, 1.0f - (timer / InPlayTime));
            Vector3 diePos = die.position;
            diePos.y = currY;
            diePos.z = transform.position.z + DiceZ;
            die.position = diePos;
        }
    }

    public void StartTurn()
    {
        for(int i = 0; i < DicePerTurn; i++)
        {
            Vector3 pos = DiceStartPos + (Vector2.right * DiceXSpacing * i);
            pos.z = transform.position.z + DiceZ;

            GameObject die = Instantiate(DicePrefab, pos, Quaternion.identity, transform);
            dice.Add(die.transform);
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
        DieLogic logic = toAdd.GetComponent<DieLogic>();
        if (logic)
        {
            logic.NullCurrentSlot();
        }

        toAdd.transform.SetParent(transform);

        dice.Add(toAdd.transform);
    }

    public void DiscardDice()
    {
        foreach (Transform die in dice)
        {
            // just kill em
            Destroy(die.gameObject);
        }
        dice.Clear();
    }

    public List<Transform> GetDice()
    {
        return dice;
    }
}
