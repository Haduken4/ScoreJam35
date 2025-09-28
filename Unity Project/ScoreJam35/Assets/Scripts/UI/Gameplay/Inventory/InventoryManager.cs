using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Transform> TrinketSlots = new List<Transform>();
    public List<Transform> ToolSlots = new List<Transform>();

    public List<GameObject> TrinketPrefabs = new List<GameObject>();
    public List<GameObject> ToolPrefabs = new List<GameObject>();

    public GameObject ToolPopupParent = null;
    public GameObject TrinketPopupParent = null;
    public GameObject ScorePopupParent = null;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainNewTrinket()
    {
        
    }

    public void GainNewTool()
    {
        
    }
}
