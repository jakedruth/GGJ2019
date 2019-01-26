using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Entity : MonoBehaviour
{
    public bool isPlayer;
    public float maxHP;
    public float CurrentHP { get; private set; }

    public bool isDead { get; private set; }

    public delegate void OnDeath();
    public OnDeath onDeath;

    public void Awake()
    {
        CurrentHP = maxHP;
    }

    /// <summary>
    /// Deal a certain amount of damage to the entity
    /// </summary>
    /// <param name="value"> Amount to deal damage </param>
    public void DealDamage(float value)
    {
        if (isDead)
            return;

        Debug.Log($"dealing {value} damage to {name}");

        CurrentHP = Mathf.Clamp(CurrentHP - value, 0, maxHP);
        isDead = CurrentHP <= 0;
        if (isDead)
        {
            CurrentHP = 0;
            onDeath?.Invoke();
        }
    }
}
