using UnityEngine;

public class InitialPopup : MonoBehaviour
{
    public void UnfreezeTime()
    {
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        Time.timeScale = 0;
    }
}
