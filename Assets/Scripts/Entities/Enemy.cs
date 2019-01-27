using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Enemy : MonoBehaviour
{
    public float pushForce;
    public float damage;
    public bool lookAtPlayer;

    public void Update()
    {
        if(lookAtPlayer)
        {
            PlayerController pc = GetClosestPlayer();
            Vector3 displacement = pc.transform.position - transform.position;
            float angle = Vector3.SignedAngle(displacement, Vector3.right, Vector3.back);
            transform.GetChild(0).rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Entity pc = collision.transform.GetComponent<Entity>();
        if (pc != null)
        {
            if (pc.tag == "Player")
            {
                Vector3 displacement = pc.transform.position - transform.position;
                Vector3 direction = displacement.normalized;
                pc.ApplyForce(direction * pushForce);
                Entity entity = pc.GetComponent<Entity>();
                entity.DealDamage(damage);
            }
        }
    }

    public PlayerController GetClosestPlayer()
    {
        PlayerController[] pcs = FindObjectsOfType<PlayerController>();

        if (pcs.Length == 1)
            return pcs[0];

        PlayerController closest = pcs[0];
        float minDistSqrd = float.PositiveInfinity;
        foreach (PlayerController pc in pcs)
        {
            float sqrDist = (pc.transform.position - transform.position).sqrMagnitude;
            if(sqrDist < minDistSqrd)
            {
                minDistSqrd = sqrDist;
                closest = pc;
            }
        }

        return closest;
    }
}
