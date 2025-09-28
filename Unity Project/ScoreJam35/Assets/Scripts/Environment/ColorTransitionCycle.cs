using UnityEngine;

public class ColorTransitionCycle : MonoBehaviour
{
    public Gradient FullCycle;

    public float DaytimePoint = 0.4f;
    public float NightTimePoint = 0.9f;

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
