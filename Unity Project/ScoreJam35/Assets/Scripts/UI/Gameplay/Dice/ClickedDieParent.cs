using UnityEngine;
using UnityEngine.InputSystem;

public class ClickedDieParent : MonoBehaviour
{
    public static ClickedDieParent Instance { get; private set; }

    public float LerpSpeed = 5.0f;
    public float ZPosition = 0;

    Transform currDie = null;
    Transform thisFrameLastDie = null;

    [HideInInspector]
    public BaseDieSlot hoveredDieSlot = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
        Vector3 pos = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), LerpSpeed * Time.deltaTime);
        pos.z = ZPosition;
        transform.position = pos;
    }

    private void LateUpdate()
    {
        thisFrameLastDie = null;
    }

    public void SetCurrentDie(Transform newDie)
    {
        thisFrameLastDie = currDie;
        currDie = newDie;
        if (currDie == null)
        {
            return;
        }

        currDie.SetParent(transform);
        currDie.localPosition = Vector3.zero;
    }

    public Transform GetCurrentDie()
    {
        return currDie;
    }

    public Transform GetThisFrameLastDie()
    {
        return thisFrameLastDie;
    }
}
