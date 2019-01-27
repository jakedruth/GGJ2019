using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class PlayerController : MonoBehaviour
{
    private Entity _entity;
    public SwordHandler SwordRef { get; private set; }
    public ShieldHandler ShieldRef { get; private set; }
    public bool IsShielding { get; private set; }

    [Header("Movement")]
    public float rotateSpeed;
    public float rotationInDegrees { get; private set; }
    //public Vector3 facing { get; private set; }


    [Header("Enemy Interaction")]
    public LayerMask swordInteractLayer;
    public float swordLength;
    public float swordDamage;

    public void Start()
    {
        _entity = GetComponent<Entity>();
        _entity.isPlayer = true;
        _entity.OnEntityDeath.AddListener(PlayerDied);

        SwordRef = transform.GetComponentInChildren<SwordHandler>();

        ShieldRef = transform.GetComponentInChildren<ShieldHandler>();
    }

    public void RecieveInput(Vector3 direction, bool attackKeyDown, bool shieldKey)
    {
        IsShielding = shieldKey;
        ShieldRef.gameObject.SetActive(IsShielding);

        if (attackKeyDown && !IsShielding)
        {
            SwordRef.StartAttack(rotationInDegrees);
        }

        // check for entities
        if (SwordRef.IsAttacking())
        {
            //Debug.DrawRay(_sword.transform.position, _sword.transform.right, Color.blue, 1.0f);
            Ray2D ray = new Ray2D(SwordRef.transform.position, SwordRef.transform.right);
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
                if(hit.transform.tag == "Bomb")
                {
                    Bomb b = hit.transform.GetComponent<Bomb>();
                    Vector3 displacement = b.transform.position - transform.position;
                    b.ApplyForce(displacement.normalized);
                }
            }
        }

        const float deadZone = 0.01f;
        float targetSpeed = 0;
        if (direction.sqrMagnitude > deadZone * deadZone && !SwordRef.IsAttacking())
        {
            targetSpeed = IsShielding ? 0 : _entity.maxMoveSpeed;
            float angle = Vector3.SignedAngle(direction, Vector3.right, Vector3.back);
            //float targetAngle = Mathf.Round(angle / 45f) * 45f; // created choppy rotation some how?
            rotationInDegrees = Mathf.MoveTowardsAngle(rotationInDegrees, angle, rotateSpeed * GameManager.DeltaTime);
            transform.GetChild(0).rotation = Quaternion.AngleAxis(rotationInDegrees, Vector3.forward);
        }

        _entity.velocity = Vector3.MoveTowards(_entity.velocity, direction * targetSpeed, _entity.acceleration * GameManager.DeltaTime);
    }

    public void PlayerDied()
    {
        Debug.Log("Player Died");
    }
}
