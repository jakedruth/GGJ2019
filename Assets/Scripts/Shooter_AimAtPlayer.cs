using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Shooter_AimAtPlayer : Shooter
{
    private Enemy _enemy;

    public float fireRange;
    public float fireCoolDown;
    private float _fireCoolDownTimer;

    public void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.lookAtPlayer = true;
        _fireCoolDownTimer = 0;
    }

    private void Update()
    {
        PlayerController pc = _enemy.GetClosestPlayer();
        Vector3 displacement = pc.transform.position - transform.position;
        if (displacement.sqrMagnitude < fireRange * fireRange)
        {
            _fireCoolDownTimer += GameManager.DeltaTime;
            if(_fireCoolDownTimer >= fireCoolDown)
            {
                Shoot();
            }
        }
        else
        {
            _fireCoolDownTimer = 0;
        }
    }

    public void Shoot()
    {
        _fireCoolDownTimer = 0;
        ShootTowards(_enemy.facing);
    }
}
