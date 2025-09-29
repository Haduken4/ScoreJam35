using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class TooltipCanvasSetup : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 100;

        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null)
        {
            // Assign the main UI camera at runtime
            canvas.worldCamera = Camera.main;
        }
    }
}
