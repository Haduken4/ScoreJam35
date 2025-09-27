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
                DiceManager.Instance.ReAddDie(gameObject);
                // try to apply to colliding die slot?
            }
        }
        else if (mouseOver)
        {
            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                dragging = true;
                ClickedDieParent.Instance.SetCurrentDie(transform);
            }
        }
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
}
