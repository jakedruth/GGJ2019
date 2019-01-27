using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class Entity : MonoBehaviour
{
    public bool isPlayer;
    public float maxHP;
    public float CurrentHP { get; private set; }

    private float _hitCoolDownTimer = 0.25f;
    private float _coolDownTimer;

    private HealthBar _healthBar;

    public bool isDead { get; private set; }

    public UnityEvent OnEntityDeath;
    
    public void Awake()
    {
        CurrentHP = maxHP;
    }

    /// <summary>
    /// Deal a certain amount of damage to the entity
    /// </summary>
    /// <param name="value"> Amount to deal damage </param>
    public void DealDamage(float value, bool ignoreInvincibilty = false)
    {
        if (isDead)
            return;

        //Debug.Log($"dealing {value} damage to {name}");

        if (!IsInvincible() || ignoreInvincibilty)
        {
            CurrentHP = Mathf.Clamp(CurrentHP - value, 0, maxHP);
            HandleHealthBar();
            isDead = CurrentHP <= 0;
            if (isDead)
            {
                CurrentHP = 0;
                OnEntityDeath.Invoke();
            }
        }

        if (!ignoreInvincibilty)
            _coolDownTimer = _hitCoolDownTimer;
    }

    private void HandleHealthBar()
    {
        if(_healthBar == null)
        {
            HealthBar bar = Resources.Load<HealthBar>("Prefabs/HealthBar");
            _healthBar = Instantiate(bar, transform);
        }

        _healthBar.SetHealthBar(CurrentHP / maxHP);
        _healthBar.ResetTimer();
    }

    public void Update()
    {
        if (IsInvincible())
            _coolDownTimer -= GameManager.DeltaTime;
    }

    private bool IsInvincible()
    {
        return _coolDownTimer > 0;
    }
}
