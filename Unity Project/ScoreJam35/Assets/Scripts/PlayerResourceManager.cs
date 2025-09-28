using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum E_Resource { FOOD, WATER, HEALTH, WOOD }

public class PlayerResourceManager : MonoBehaviour
{
    public static PlayerResourceManager Instance = null;

    public int MaxWater = 20;
    public int StartingWater = 20;
    public Image WaterFill = null;
    int water = 0;

    public int MaxFood = 20;
    public int StartingFood = 20;
    public Image FoodFill = null;
    int food = 0;

    public int MaxHealth = 20;
    public int StartingHealth = 20;
    public Image HealthFill = null;
    int health = 0;

    public int MaxWood = 5;
    public int StartingWood = 0;
    public TextMeshProUGUI WoodText = null;
    int wood = 0;

    public GameObject DeathPopupParent = null;

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
        water = StartingWater;
        UpdateImageFill(WaterFill, water, MaxWater);
        food = StartingFood;
        UpdateImageFill(FoodFill, food, MaxFood);
        health = StartingHealth;
        UpdateImageFill(HealthFill, health, MaxHealth);
        wood = StartingWood;
        UpdateWoodText();
    }

    void UpdateImageFill(Image img, int amount, int max)
    {
        img.fillAmount = (float)amount / max;
    }

    void UpdateWoodText()
    {
        if (WoodText)
        {
            WoodText.text = wood.ToString();
        }
    }

    public void ChangeWater(int change)
    {
        water += change;
        if (water < 0)
        {

        }
        water = Mathf.Clamp(water, 0, MaxWater);
        UpdateImageFill(WaterFill, water, MaxWater);
    }
    
    public int GetWater()
    {
        return water;
    }

    public void ChangeFood(int change)
    {
        food += change;
        if (food < 0)
        {
            
        }
        food = Mathf.Clamp(food, 0, MaxFood);
        UpdateImageFill(FoodFill, food, MaxFood);
    }

    public int GetFood()
    {
        return food;
    }

    public void ChangeHealth(int change)
    {
        health += change;
        if (health <= 0)
        {
            //dead
            TurnManager.Instance.OnFinish();
        }
        health = Mathf.Clamp(health, 0, MaxHealth);
        UpdateImageFill(HealthFill, health, MaxHealth);
    }

    public int GetHealth()
    {
        return health;
    }

    public void ChangeWood(int change)
    {
        wood = Mathf.Clamp(wood + change, 0, MaxWood);
        UpdateWoodText();
    }

    public int GetWood()
    {
        return wood;
    }

    public int GetResource(E_Resource resource)
    {
        switch (resource)
        {
            case E_Resource.FOOD:
                return GetFood();
            case E_Resource.WATER:
                return GetWater();
            case E_Resource.HEALTH:
                return GetHealth();
            case E_Resource.WOOD:
                return GetWood();
        }

        return 0;
    }

    public void ChangeResource(E_Resource resource, int change)
    {
        switch (resource)
        {
            case E_Resource.FOOD:
                ChangeFood(change);
                break;
            case E_Resource.WATER:
                ChangeWater(change);
                break;
            case E_Resource.HEALTH:
                ChangeHealth(change);
                break;
            case E_Resource.WOOD:
                ChangeWood(change);
                break;
        }
    }
}
