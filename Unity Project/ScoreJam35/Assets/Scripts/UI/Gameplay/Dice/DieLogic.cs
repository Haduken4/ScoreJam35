using UnityEngine;

public class DieLogic : MonoBehaviour
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
        if (dragging)
        {
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                ClickedDieParent.Instance.SetCurrentDie(null);
                // try to apply to colliding die slot?
            }
        }
        else if (mouseOver)
        {
            if(Input.GetMouseButtonDown(0))
            {
                dragging = true;
                ClickedDieParent.Instance.SetCurrentDie(transform);
            }
        }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
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
