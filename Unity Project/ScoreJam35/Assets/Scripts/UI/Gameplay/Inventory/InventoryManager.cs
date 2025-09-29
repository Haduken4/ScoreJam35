using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Transform> InventorySlots = new List<Transform>();

    public List<GameObject> TrinketPrefabs = new List<GameObject>();
    public List<GameObject> ToolPrefabs = new List<GameObject>();

    public TooltipPopup TooltipParent = null;

    public int ScoreReward = 20;

    int items = 0;

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

        GameObject trinket = Instantiate(TrinketPrefabs[prefabIndex], InventorySlots[items].position, Quaternion.identity, InventorySlots[items]);
        BaseItem item = trinket.GetComponent<BaseItem>();
        item.MySlot = InventorySlots[items];

        SetupTooltip(item.Title, item.Description, item.ItemIcon);

        ++items;
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

        GameObject tool = Instantiate(ToolPrefabs[prefabIndex], InventorySlots[items].position, Quaternion.identity, InventorySlots[items]);
        BaseItem item = tool.GetComponent<BaseItem>();
        item.MySlot = InventorySlots[items];

        SetupTooltip(item.Title, item.Description, item.ItemIcon);

        ++items;
        ToolPrefabs.RemoveAt(prefabIndex);
    }

    public void GainScore(string replacementText)
    {
        string descriptionString = $"There are no more {replacementText}! Gain {ScoreReward} points instead.";

        SetupTooltip("Gain Score", descriptionString, null);
        GlobalGameData.Score += ScoreReward;
    }

    void SetupTooltip(string title, string description, Sprite icon)
    {
        if (TooltipParent == null)
        {
            return;
        }

        TooltipParent.gameObject.SetActive(true);

        TooltipParent.TitleText.text = title;
        TooltipParent.DescriptionText.text = description;

        TooltipParent.CenterImage.gameObject.SetActive(icon != null);
        TooltipParent.CenterImage.sprite = icon;
    }
}
