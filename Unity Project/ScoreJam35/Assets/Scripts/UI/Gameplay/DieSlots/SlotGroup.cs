using System.Collections.Generic;
using UnityEngine;

public class SlotGroup : MonoBehaviour
{
    public bool ResetOnTurn = true;
    public bool ResetOnDay = true;
    public bool ShrinkOnUse = true;
    public bool ScaleIn = true;
    public float ScaleTime = 1.0f;
    public Vector3 BaseScale = Vector3.one;

    public List<BaseDiceAction> ActionsOnFilled = new List<BaseDiceAction>();
    public int SlotCount = 1;

    List<BaseDieSlot> slots = new List<BaseDieSlot>();
    int totalDieValue = 0;
    int currentFilledSlots = 0;
    float scaleTimer = 0;
    bool scaling = false;
    bool unslotDieOnScale = false;
    Vector3 targetScale = Vector3.zero;
    Vector3 startScale = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalDieValue = 0;
        currentFilledSlots = 0;

        if(ScaleIn)
        {
            StartScaling(Vector3.zero, BaseScale);
        }
    }

    private void Update()
    {
        if (scaling)
        {
            scaleTimer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, scaleTimer / ScaleTime);
            if (scaleTimer >= ScaleTime)
            {
                scaling = false;
                if (unslotDieOnScale)
                {
                    UnslotAllDie(true);
                    unslotDieOnScale = false;
                }
            }
        }
    }

    public void UnslotAllDie(bool destroy = true)
    {
        for (int i = 0; i < slots.Count;)
        {
            slots[i].UnslotDie(destroy);
        }
    }

    public void PerformAction()
    {
        foreach (BaseDiceAction action in ActionsOnFilled)
        {
            action.PerformDiceValueAction(totalDieValue);
        }

        if (ShrinkOnUse)
        {
            foreach (BaseDieSlot slot in slots)
            {
                slot.GetSlottedDie().Draggable = false;
            }

            StartScaling(BaseScale, Vector3.zero);
            unslotDieOnScale = true;
        }
    }

    void StartScaling(Vector3 start, Vector3 end)
    {
        scaling = true;
        scaleTimer = 0;
        startScale = start;
        targetScale = end;
        transform.localScale = start;
    }

    public bool IsScaling()
    {
        return scaling;
    }

    public void StartShrinking(bool unslotDice)
    {
        StartScaling(BaseScale, Vector3.zero);
        unslotDieOnScale = true;
    }

    public void StartScaleIn()
    {
        StartScaling(Vector3.zero, BaseScale);
    }

    public bool AllSlotsFilled()
    {
        return currentFilledSlots == SlotCount;
    }

    public void FillSlot(int dieValue, BaseDieSlot slot)
    {
        totalDieValue += dieValue;
        ++currentFilledSlots;
        slots.Add(slot);
    }

    public void EmptySlot(int dieValue, BaseDieSlot slot)
    {
        totalDieValue -= dieValue;
        --currentFilledSlots;
        slots.Remove(slot);
    }
}
