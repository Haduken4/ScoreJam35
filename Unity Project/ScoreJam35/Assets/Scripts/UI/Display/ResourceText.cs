using TMPro;
using UnityEngine;

public class ResourceText : MonoBehaviour
{
    public E_Resource ToDisplay = E_Resource.FOOD;

    TextMeshProUGUI text = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = PlayerResourceManager.Instance.GetResource(ToDisplay).ToString();
    }
}
