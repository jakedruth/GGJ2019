using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Gradient bombFuseGradient;
    public float bombFuseTime;
    private float bombFuseTimer;
    public float maxSpeed;
    private Vector3 _velocity;
    public float friction;

    void Awake()
    {
        anim.SetFloat("SpeedMultiplier", 0);
    }

    // Update is called once per frame
    void Update()
    {
        _velocity = Vector3.MoveTowards(_velocity, Vector3.zero, friction * GameManager.DeltaTime);
        anim.SetFloat("SpeedMultiplier", _velocity.magnitude / maxSpeed * 4);

        bombFuseTimer += GameManager.DeltaTime;
        bombFuseTimer %= bombFuseTime;
        float per = bombFuseTimer / bombFuseTime;
        spriteRenderer.color = bombFuseGradient.Evaluate(per);
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
}
