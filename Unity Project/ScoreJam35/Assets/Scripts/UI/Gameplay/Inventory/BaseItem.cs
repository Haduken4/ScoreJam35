using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public bool Draggable = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void ActivateItem();
}
