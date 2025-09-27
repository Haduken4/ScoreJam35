using UnityEngine;

public class ColorTransitionCycle : MonoBehaviour
{
    public Gradient FullCycle;

    SpriteRenderer sr = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
