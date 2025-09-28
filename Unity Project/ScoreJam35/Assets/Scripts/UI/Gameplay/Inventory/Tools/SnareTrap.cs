using System.Resources;
using UnityEngine;

public class SnareTrap : BaseItem
{
    public float FoodChance = 0.333f;
    public int FoodToGain = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        TurnManager.Instance.OnStartDay += OnStartDay;
    }

    public override void ActivateItem()
    {
        // Doesn't need to do anything
    }

    void OnStartDay()
    {
        float chance = Random.Range(0f, 1f);
        if (FoodChance >= chance)
        {
            PlayerResourceManager.Instance.ChangeFood(FoodToGain);
        }
    }
}
