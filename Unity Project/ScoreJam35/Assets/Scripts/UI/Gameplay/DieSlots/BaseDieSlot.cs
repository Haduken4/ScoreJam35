using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public abstract class BaseDieSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float RectScale = 1.0f;

    bool hovered = false;
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        bool actualHovered = CheckHovered();

        if(hovered && !actualHovered)
        {
            OnPointerExit(null);
        }
        else if (!hovered && actualHovered)
        {
            OnPointerEnter(null);
        }
    }

    bool CheckHovered()
    {
        if (Mouse.current == null) return false;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Convert screen point to local point
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, Camera.main, out Vector2 localPoint))
        {
            // Get the rect in local space
            Rect rect = rectTransform.rect;

            // Scale around center
            Vector2 center = rect.center;
            rect.size *= RectScale;
            rect.center = center;

            return rect.Contains(localPoint);
        }
        return false;
    }

    public virtual bool AttemptApplyDie(DieLogic die)
    {
        if (!hovered)
        {
            return false;
        }

        return true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        ClickedDieParent.Instance.hoveredDieSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        if (ClickedDieParent.Instance.hoveredDieSlot == this)
        {
            ClickedDieParent.Instance.hoveredDieSlot = null;
        }
    }
}
