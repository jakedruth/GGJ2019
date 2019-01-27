using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed;
    public Vector3 direction;
    public bool hurtEnemy;
    public bool hurtPlayer;

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * GameManager.DeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!hurtEnemy && collision.tag == "enemy") || (!hurtPlayer && collision.tag == "Player"))
            return;

        Entity e = collision.transform.GetComponent<Entity>();

        if (e != null)
        {
            if(e.tag == "Player")
            {
                PlayerController pc = e.GetComponent<PlayerController>();
                if(pc.IsShielding)
                {
                    float projectileAngle = Vector3.SignedAngle(direction, Vector3.right, Vector3.back);
                    float delta = Mathf.Abs(Mathf.DeltaAngle(projectileAngle + 180, pc.rotationInDegrees));

                    Debug.Log($"projectile + 180: {projectileAngle + 180} pc: {pc.rotationInDegrees} delta abs: {delta}");

                    if (delta < pc.ShieldRef.halfAngle)
                    {
                        direction *= -1;
                        hurtEnemy = true;
                        hurtPlayer = false;
                        return;
                    }
                }
            }

            e.DealDamage(damage);
        }

        Destroy(gameObject);
    }
}
