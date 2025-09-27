using UnityEngine;

public class ClickedDieParent : MonoBehaviour
{
    public static ClickedDieParent Instance { get; private set; }

    public float LerpSpeed = 5.0f;

    Transform currDie = null;

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
        transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), LerpSpeed * Time.deltaTime);
    }

    public void SetCurrentDie(Transform newDie)
    {
        currDie = newDie;
        if (currDie == null)
        {
            return;
        }

        currDie.SetParent(transform);
    }
}
