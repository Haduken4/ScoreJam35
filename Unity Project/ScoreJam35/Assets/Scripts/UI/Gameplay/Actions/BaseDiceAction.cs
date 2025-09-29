using UnityEngine;
using System;
using JetBrains.Annotations;

public abstract class BaseDiceAction : MonoBehaviour
{
    public event Action OnActionPerformed;
    public AudioPlayer Player = null;

    public virtual void PerformDiceValueAction(int totalValue)
    {
        if (Player)
        {
            Player.PlaySound();
        }

        OnActionPerformed?.Invoke();
    }
}
