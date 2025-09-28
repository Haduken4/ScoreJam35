using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public bool Draggable = false;
    public Transform MySlot = null;

    public string Title = "";
    public string Description = "";
    public Sprite ItemIcon = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        ActivateItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void ActivateItem();
}
