using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Color bright;
    public Color dark;
    public float damage;
    public float bombFuseTime;
    public float explosionRadius;
    public LayerMask explosionLayer;

    [Header("Movement")]
    public float maxSpeed;
    private Vector3 _velocity;
    public float friction;
    public UnityEngine.Events.UnityEvent onBombExplode;

    void Awake()
    {
        anim.SetFloat("SpeedMultiplier", 0);
    }

    // Update is called once per frame
    void Update()
    {
        _velocity = Vector3.MoveTowards(_velocity, Vector3.zero, friction * GameManager.DeltaTime);
        anim.SetFloat("SpeedMultiplier", _velocity.magnitude / maxSpeed * 4);

        
    }

    private void FixedUpdate()
    {
        transform.position += _velocity * GameManager.FidexDeltaTime;
    }

    public void ApplyForce(Vector3 direction)
    {
        _velocity = direction * maxSpeed;
        float angle = Vector3.SignedAngle(_velocity, Vector3.right, Vector3.back);
        spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void StartFuse()
    {
        StartCoroutine(Fuse());
    }

    private IEnumerator Fuse()
    {
        float timer = 0;

        while (timer < bombFuseTime)
        {
            timer += GameManager.DeltaTime;

            float p = 0.5f * Mathf.Cos(5 * timer * timer * timer) + 0.5f;

            Color c = Color.Lerp(bright, dark, p);

            spriteRenderer.color = c;

            yield return null;
        }

        Explode();
    }

    public void Explode()
    {
        ParticleSystem psPrefab = Resources.Load<ParticleSystem>("Prefabs/Explosion");
        ParticleSystem ps = Instantiate(psPrefab, transform.position, psPrefab.transform.rotation);
        onBombExplode.Invoke();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayer);
        foreach (Collider2D other in colliders)
        {
            if(other.tag == "Player" || other.tag == "enemy")
            {
                Entity e = other.GetComponent<Entity>();
                e.DealDamage(damage);
            }

            if(other.tag == "Breakable")
            {
                Debug.Log(other.gameObject);
                Destroy(other);
            }
        }

        Destroy(gameObject);
    }
}
