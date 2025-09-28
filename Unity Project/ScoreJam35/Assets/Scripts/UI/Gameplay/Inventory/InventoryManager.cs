using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Transform> TrinketSlots = new List<Transform>();
    public List<Transform> ToolSlots = new List<Transform>();

    public List<GameObject> TrinketPrefabs = new List<GameObject>();
    public List<GameObject> ToolPrefabs = new List<GameObject>();

    public TooltipPopup TooltipParent = null;

    int trinkets = 0;
    int tools = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    public void GainNewTrinket()
    {
        if (TrinketPrefabs.Count == 0)
        {
            GainScore("trinkets to find");
            return;
        }

        int prefabIndex = Random.Range(0, TrinketPrefabs.Count);

        GameObject trinket = Instantiate(TrinketPrefabs[prefabIndex], TrinketSlots[trinkets].position, Quaternion.identity, TrinketSlots[trinkets]);
        BaseItem item = trinket.GetComponent<BaseItem>();
        item.MySlot = TrinketSlots[trinkets];

        SetupTooltip(item.Title, item.Description, item.ItemIcon);

        ++trinkets;
        TrinketPrefabs.RemoveAt(prefabIndex);
    }

    public void GainNewTool()
    {
        if (ToolPrefabs.Count == 0)
        {
            GainScore("tools to craft");
            return;
        }

        int prefabIndex = Random.Range(0, ToolPrefabs.Count);

        GameObject tool = Instantiate(ToolPrefabs[prefabIndex], ToolSlots[tools].position, Quaternion.identity, ToolSlots[tools]);
        BaseItem item = tool.GetComponent<BaseItem>();
        item.MySlot = ToolSlots[tools];

        SetupTooltip(item.Title, item.Description, item.ItemIcon);

        ++tools;
        ToolPrefabs.RemoveAt(prefabIndex);
    }

    public void GainScore(string replacementText)
    {
        string descriptionString = $"There are no more {replacementText}! Gain 20 points instead.";

        SetupTooltip("Gain Score", descriptionString, null);
    }

    void SetupTooltip(string title, string description, Sprite icon)
    {
        if (TooltipParent == null)
        {
            return;
        }

        TooltipParent.TitleText.text = title;
        TooltipParent.DescriptionText.text = description;

        TooltipParent.CenterImage.gameObject.SetActive(icon != null);
        TooltipParent.CenterImage.sprite = icon;
    }
}
