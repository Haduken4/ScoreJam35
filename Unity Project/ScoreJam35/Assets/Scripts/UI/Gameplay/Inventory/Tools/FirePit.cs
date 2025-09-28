using UnityEngine;
using UnityEngine.SceneManagement;

public class FirePit : BaseItem
{
    public override void ActivateItem()
    {
        GameObject fireAction = GameObject.Find("StartAFireAction");
        GameObject fireActionBuffed = FindInactiveInScene("StartAFireActionBuffed");

        fireAction.SetActive(false);
        fireActionBuffed.SetActive(true);
        fireActionBuffed.GetComponent<SlotGroup>().StartScaleIn();
    }

    GameObject FindInactiveInScene(string name)
    {
        var scene = SceneManager.GetActiveScene();
        var roots = scene.GetRootGameObjects();

        foreach (var root in roots)
        {
            // Check root first
            if (root.name == name)
            {
                return root;
            }

            // Search children (active and inactive)
            var found = FindInChildren(root.transform, name);
            if (found != null)
            {
                return found.gameObject;
            }
        }

        return null;
    }

    Transform FindInChildren(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }

            var result = FindInChildren(child, name);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
