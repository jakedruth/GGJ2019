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

    [Header("Movement")]
    public float maxMoveSpeed;
    public float acceleration;
    public float friction;
    public Vector3 velocity { get; set; }
    public Vector3 force { get; set; }
    public Vector3 constantSpeed { get; set; }

    public Vector3 Direction { get; set; }

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
        if (_healthBar == null)
        {
            HealthBar healthBarPrefab = Resources.Load<HealthBar>("Prefabs/HealthBar");
            _healthBar = Instantiate(healthBarPrefab, transform);
        }

        _healthBar.SetHealthBar(CurrentHP / maxHP);
        _healthBar.ResetTimer();
    }

    public void Update()
    {
        if (IsInvincible())
            _coolDownTimer -= GameManager.DeltaTime;

        force = Vector3.MoveTowards(force, Vector3.zero, friction * GameManager.DeltaTime);
    }


    public List<KeyValuePair<Vector3, float>> PushDirections { get; set; } = new List<KeyValuePair<Vector3, float>>();

    public void FixedUpdate()
    {
        Vector3 pushDir = Vector3.zero;
        float pushSpeed = 0;
        foreach (KeyValuePair<Vector3, float> pair in PushDirections)
        {
            pushDir += pair.Key;
            pushSpeed = Mathf.Max(pushSpeed, pair.Value);
        }

        Vector3 pos = (velocity + force + pushDir.normalized * pushSpeed) * GameManager.FidexDeltaTime;
        transform.position += pos;
    }

    public void ApplyForce(Vector3 force)
    {
        this.force += force;
    }

    private bool IsInvincible()
    {
        return _coolDownTimer > 0;
    }
}
