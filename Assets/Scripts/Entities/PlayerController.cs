using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class PlayerController : MonoBehaviour
{
    private Entity _entity;
    private SwordHandler _sword;

    [Header("Movement")]
    public LayerMask wallLayer;
    public float maxMoveSpeed;
    public float acceleration;
    public float friction;
    private Vector3 _velocity;
    private Vector3 _force;
    public float rotateSpeed;
    private float _facing;

    [Header("Enemy Interaction")]
    public LayerMask enemyLayer;
    public float swordLength;
    public float swordDamage;

    public void Start()
    {
        _entity = GetComponent<Entity>();
        _entity.isPlayer = true;
        _entity.onDeath += PlayerDied;

        _sword = transform.GetComponentInChildren<SwordHandler>();
    }

    public void RecieveInput(Vector3 direction, bool attackKeyDown, bool shieldKeyDown)
    {
        if (attackKeyDown)
        {
            _sword.StartAttack(_facing);
        }

        // check for entities
        if (_sword.IsAttacking())
        {
            //Debug.DrawRay(_sword.transform.position, _sword.transform.right, Color.blue, 1.0f);
            Ray2D ray = new Ray2D(_sword.transform.position, _sword.transform.right);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, swordLength, enemyLayer);
            if(hit)
            {
                //Debug.Log($"Hit game objec: {hit.transform.name}");
                if (hit.transform != transform)
                {
                    Entity entity = hit.transform.GetComponent<Entity>();
                    entity.DealDamage(swordDamage);
                }
            }
        }

        const float deadZone = 0.01f;
        float targetSpeed = 0;
        if (direction.sqrMagnitude > deadZone * deadZone && !_sword.IsAttacking())
        {
            targetSpeed = maxMoveSpeed;
            float angle = Vector3.SignedAngle(direction, Vector3.right, Vector3.back);
            //float targetAngle = Mathf.Round(angle / 45f) * 45f; // created choppy rotation some how?
            _facing = Mathf.MoveTowardsAngle(_facing, angle, rotateSpeed * GameManager.DeltaTime);
            transform.GetChild(0).rotation = Quaternion.AngleAxis(_facing, Vector3.forward);
        }

        _velocity = Vector3.MoveTowards(_velocity, direction * targetSpeed, acceleration * GameManager.DeltaTime);
        _force = Vector3.MoveTowards(_force, Vector3.zero, friction * GameManager.DeltaTime);
    }

    public void FixedUpdate()
    {
        Vector3 pos = (_velocity + _force) * GameManager.FidexDeltaTime;
        transform.position += pos;
    }



    public void ApplyForce(Vector3 force)
    {
        _force += force;

    }

    public void Attack()
    {

    }

    public void Shield()
    {

    }

    public void PlayerDied()
    {
        Debug.Log("Player Died");
    }
}
