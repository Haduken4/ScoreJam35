using UnityEngine;
using UnityEngine.UI;

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
    }

    void UpdateImageFill(Image img, int amount, int max)
    {
        img.fillAmount = (float)amount / max;
    }

    public void ChangeWater(int change)
    {
        water += change;
        if (water < 0)
        {

        }
        water = Mathf.Clamp(water, 0, MaxWater);
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
        }
    }
}
