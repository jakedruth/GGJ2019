using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AnimateLineRenderer))]
public class ElectricDoor : MonoBehaviour
{
    private AnimateLineRenderer _animatedLine;
    public float damageIfTouched;
    public bool IsOn { get; private set; } = true;
    public LayerMask playerLayer;
    Vector3 displacement;
    Vector3 perpidicular;
    Plane _plane;

    public void Awake()
    {
        _animatedLine = GetComponent<AnimateLineRenderer>();
        displacement = _animatedLine.endPoint.position - _animatedLine.startPoint.position;
        perpidicular = Vector3.Cross(displacement, Vector3.forward);
        Debug.DrawRay(_animatedLine.startPoint.position, perpidicular, Color.blue, 5.0f);
        _plane = new Plane(perpidicular, _animatedLine.startPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOn)
        {
            Ray2D ray = new Ray2D(_animatedLine.startPoint.position, displacement);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, displacement.magnitude, playerLayer);
            if (hit)
            {
                if (hit.transform.tag == "Player")
                {
                    PlayerController pc = hit.transform.GetComponent<PlayerController>();
                    float force = 20;
                    bool sameSide = _plane.GetSide(hit.transform.position);

                    Debug.Log(sameSide);

                    if (!sameSide)
                        force *= -1;

                    pc.ApplyForce(_plane.normal * force);
                    pc.GetComponent<Entity>().DealDamage(damageIfTouched);
                }
            }
        }
    }

    [ContextMenu("Toggle Door")]
    public bool ToggleDoor()
    {
        IsOn = !IsOn;

        GetComponent<LineRenderer>().enabled = IsOn;

        return IsOn;
    }
}
