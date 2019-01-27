using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class PlayerController : MonoBehaviour
{
    private Entity _entity;
    private SwordHandler _sword;

    [Header("Movement")]
    public float rotateSpeed;
    private float _facing;

    [Header("Enemy Interaction")]
    public LayerMask swordInteractLayer;
    public float swordLength;
    public float swordDamage;

    public void Start()
    {
        _entity = GetComponent<Entity>();
        _entity.isPlayer = true;
        _entity.OnEntityDeath.AddListener(PlayerDied);

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
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, swordLength, swordInteractLayer);
            if(hit)
            {
                if (hit.transform.tag == "enemy")
                {
                    //Debug.Log($"Hit game objec: {hit.transform.name}");
                    Entity entity = hit.transform.GetComponent<Entity>();
                    entity.DealDamage(swordDamage);
                    entity.ApplyForce(ray.direction * 10);
                }
                if(hit.transform.tag == "Switch")
                {
                    Switch s = hit.transform.GetComponent<Switch>();
                    s.ActivateSwitch();
                }
            }
        }

        const float deadZone = 0.01f;
        float targetSpeed = 0;
        if (direction.sqrMagnitude > deadZone * deadZone && !_sword.IsAttacking())
        {
            targetSpeed = _entity.maxMoveSpeed;
            float angle = Vector3.SignedAngle(direction, Vector3.right, Vector3.back);
            //float targetAngle = Mathf.Round(angle / 45f) * 45f; // created choppy rotation some how?
            _facing = Mathf.MoveTowardsAngle(_facing, angle, rotateSpeed * GameManager.DeltaTime);
            transform.GetChild(0).rotation = Quaternion.AngleAxis(_facing, Vector3.forward);
        }

        _entity.velocity = Vector3.MoveTowards(_entity.velocity, direction * targetSpeed, _entity.acceleration * GameManager.DeltaTime);
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
