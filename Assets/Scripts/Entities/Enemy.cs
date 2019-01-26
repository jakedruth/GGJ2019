using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Enemy : MonoBehaviour
{
    public float pushForce;
    public float damage;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.transform.GetComponent<PlayerController>();
        if (pc != null)
        {
            Vector3 displacement = pc.transform.position - transform.position;
            Vector3 direction = displacement.normalized;
            pc.ApplyForce(direction * pushForce);
            Entity entity = pc.GetComponent<Entity>();
            entity.DealDamage(damage);
        }
    }
}
