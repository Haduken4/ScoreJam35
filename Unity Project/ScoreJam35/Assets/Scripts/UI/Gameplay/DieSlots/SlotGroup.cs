using System.Collections.Generic;
using UnityEngine;

public class SlotGroup : MonoBehaviour
{
    public BaseDiceAction ActionOnFilled = null;

    List<BaseDieSlot> slots = new List<BaseDieSlot>();
    int totalDieValue = 0;
    int currentFilledSlots = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalDieValue = 0;
        currentFilledSlots = 0;
    }

    public void PerformAction()
    {
        //ActionOnFilled.PerformDiceValueAction(totalDieValue);
        for(int i = 0; i < slots.Count;)
        {
            slots[i].UnslotDie(true);
        }
    }

    public bool AllSlotsFilled()
    {
        return currentFilledSlots == slots.Count;
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
