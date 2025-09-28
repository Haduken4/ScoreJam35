using UnityEngine;

public class ColorTransitionCycle : MonoBehaviour
{
    public Gradient FullCycle;

    public float DaytimePoint = 0.4f;
    public float NightTimePoint = 0.9f;
    public float Precision = 0.02f;

    SpriteRenderer sr = null;

    float speed = 0;
    float currValue = 0;
    float targetValue = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Color c = FullCycle.Evaluate(currValue);
        sr.material.SetColor("_Gradient_Color", c);
        sr.material.SetFloat("_Opacity", c.a);
    }

    // Update is called once per frame
    void Update()
    {
        if (currValue != targetValue)
        {
            currValue += speed * Time.deltaTime;
            if(currValue > 1)
            {
                currValue -= 1.0f;
            }
            if (Mathf.Abs(currValue - targetValue) <= Precision)
            {
                currValue = targetValue;
            }

            Color c = FullCycle.Evaluate(currValue);
            sr.material.SetColor("_Gradient_Color", c);
            sr.material.SetFloat("_Opacity", c.a);
        }
    }

    public void StartTransition(float time, float value)
    {
        float dist = 0;
        if(value < currValue)
        {
            dist = (1.0f - currValue) + value;
        }
        else
        {
            dist = value - currValue;
        }

        speed = dist / time;
        targetValue = value;
    }
}
