using UnityEngine;
using System;

public abstract class BaseDiceAction : MonoBehaviour
{
    public event Action OnActionPerformed;

    public virtual void PerformDiceValueAction(int totalValue)
    {
        OnActionPerformed?.Invoke();
    }
}
