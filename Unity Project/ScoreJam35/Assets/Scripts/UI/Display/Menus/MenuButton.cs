using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public string LevelName = "";

    public void OnClick()
    {
        if (LevelName == "Quit")
        {
            Application.Quit();
            return;
        }

        SceneManager.LoadScene(LevelName);
    }
}
