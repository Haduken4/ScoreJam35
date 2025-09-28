using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseItem : MonoBehaviour
{
    public bool Useable = false;
    public Transform MySlot = null;

    public string Title = "";
    public string Description = "";
    public Sprite ItemIcon = null;

    public float RectScale = 1.0f;

    protected RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        ActivateItem();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if(Useable && ClickedDieParent.Instance.GetCurrentDie() != null && CheckHovered())
            {
                ActivateDieEffect(ClickedDieParent.Instance.GetCurrentDie().GetComponent<DieLogic>());
            }
        }
    }

    public abstract void ActivateItem();

    protected virtual void ActivateDieEffect(DieLogic die)
    {
    }

    protected bool CheckHovered()
    {
        if (Mouse.current == null)
        {
            return false;
        }

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
}
