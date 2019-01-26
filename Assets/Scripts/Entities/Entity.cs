﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHP;
    public float CurrentHP { get; private set; }

    public bool isDead { get; private set; }

    public delegate void OnDeath();
    public OnDeath onDeath;

    /// <summary>
    /// Deal a certain amount of damage to the entity
    /// </summary>
    /// <param name="value"> Amount to deal damage </param>
    public void DealDamage(float value)
    {
        CurrentHP -= value;

        isDead = CurrentHP <= 0;

        if (isDead)
        {
            CurrentHP = 0;
            if (onDeath != null)
                onDeath();
        }

    }

    public void Awake()
    {
        CurrentHP = maxHP;
    }
}