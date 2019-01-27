using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public int detail;
    public float angle;
    public float shieldDistanceFromCenter;
    public float halfAngle { get { return angle / 2; } }

    // Start is called before the first frame update
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = detail;
        UpdateShield();
    }

    [ContextMenu("Update!")]
    void UpdateShield()
    {
        float startAngle = -halfAngle;
        float deltaAngle = angle / (detail - 1);
        for (int i = 0; i < detail; i++)
        {
            float alpha = (startAngle + deltaAngle * i) * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0) * shieldDistanceFromCenter;
            _lineRenderer.SetPosition(i, pos);
        }
    }
}
