using UnityEngine;
using TMPro;

public class CombatAction : BaseDiceAction
{
    public int TotalHealth = 10;
    public TextMeshProUGUI HealthText = null;

    public int DamagePerTurn = 2;

    SlotGroup group = null;
    int health = 0;
    bool didntInit = false;

    private void OnEnable()
    {
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnEndOfTurn += OnEndOfTurn;
            health = TotalHealth;
            HealthText.text = health.ToString();
            return;
        }
        didntInit = true;
    }

    private void OnDisable()
    {
        TurnManager.Instance.OnEndOfTurn -= OnEndOfTurn;
    }

    private void Start()
    {
        group = GetComponent<SlotGroup>();

        if (didntInit)
        {
            TurnManager.Instance.OnEndOfTurn += OnEndOfTurn;
            health = TotalHealth;
            HealthText.text = health.ToString();
            didntInit = false;
        }
    }

    public override void PerformDiceValueAction(int totalValue)
    {
        health -= totalValue;
        HealthText.text = health.ToString();

        if (health <= 0)
        {
            group.StartShrinking(true, true);
        }
    }

    void OnEndOfTurn()
    {
        PlayerResourceManager.Instance.ChangeHealth(-DamagePerTurn);
    }
}
