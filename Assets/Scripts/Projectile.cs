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
            e.DealDamage(damage);
        }

        Destroy(gameObject);
    }
}
