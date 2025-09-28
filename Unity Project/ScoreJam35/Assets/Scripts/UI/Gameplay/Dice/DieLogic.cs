using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DieLogic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Max inclusive
    public Vector2Int RollRange = new Vector2Int(1, 6);

    [HideInInspector]
    public int CurrentValue { get; private set; } = 0;

    public bool Draggable = true;
    bool dragging = false;
    bool mouseOver = false;
    BaseDieSlot currentSlot = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RollDie();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Draggable)
        {
            return;
        }

        if (dragging)
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                dragging = false;
                ClickedDieParent.Instance.SetCurrentDie(null);
                AttemptPlayDie();
            }
        }
        else if (mouseOver)
        {
            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (currentSlot != null)
                {
                    currentSlot.UnslotDie();
                    currentSlot = null;
                }

                dragging = true;
                ClickedDieParent.Instance.SetCurrentDie(transform);
            }
        }
    }

    void AttemptPlayDie()
    {
        if (ClickedDieParent.Instance.hoveredDieSlot == null)
        {
            DiceManager.Instance.ReAddDie(gameObject);
            return;
        }

        // If we get into this if statement, we've played the die
        if(ClickedDieParent.Instance.hoveredDieSlot.AttemptApplyDie(this))
        {
            ClickedDieParent.Instance.SetCurrentDie(null);
            currentSlot = ClickedDieParent.Instance.hoveredDieSlot;
            ClickedDieParent.Instance.hoveredDieSlot.SlotDie(this);
            return;
        }

        DiceManager.Instance.ReAddDie(gameObject);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        mouseOver = false;
    }

    public void RollDie()
    {
        // +1 to make it max inclusive
        CurrentValue = Random.Range(RollRange.x, RollRange.y + 1);
    }

    public void FlipDie()
    {
        CurrentValue = (RollRange.y - 1) - CurrentValue;
    }

    public void NullCurrentSlot()
    {
        currentSlot = null;
    }
}
