using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AnimateLineRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector3[] _points = new Vector3[5];

    private enum PointIndex : int
    {
        POINT_BEGIN = 0,
        POINT_MIDDLE_LEFT,
        POINT_CENTER,
        POINT_MIDDLE_RIGHT,
        POINT_END,
    }

    public Transform startPoint;
    public Transform endPoint;

    public float randomPosOffset = 0.3f;
    public RangedFloat widthRange;
    public float frameTime = 0.05f;
    public bool shaderIsAnimated;
    public bool useRandomStartFrame;
    public int shaderNumOfFrames;
    private float _shaderTiling;
    private int _frameIndex;
    private Material _material;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _material = _lineRenderer.material;
        _lineRenderer.positionCount = 5;

        if (shaderIsAnimated)
        {
            _shaderTiling = 1.0f / (float)shaderNumOfFrames;
            _frameIndex = useRandomStartFrame ? Random.Range(0, shaderNumOfFrames) : 0;
            _material.mainTextureScale = new Vector2(1, _shaderTiling);
            _material.mainTextureOffset = new Vector2(0, _shaderTiling * _frameIndex);
        }

        if (startPoint == null)
            startPoint = transform;

        StartCoroutine(Beam());
    }

    private IEnumerator Beam()
    {
        while (true)
        {
            _points[(int) PointIndex.POINT_BEGIN] = startPoint.position;
            _points[(int) PointIndex.POINT_END] = endPoint.position;
            CalculateMiddle();
            _lineRenderer.SetPositions(_points);
            //_lineRenderer.SetWidth(RandomWidthOffset(), RandomWidthOffset());
            _lineRenderer.startWidth = RandomWidthOffset();
            _lineRenderer.endWidth = RandomWidthOffset();

            if (shaderIsAnimated)
            {
                _frameIndex = (_frameIndex + 1) % shaderNumOfFrames;
                _material.mainTextureOffset = new Vector2(0, _shaderTiling * _frameIndex);
            }

            yield return new WaitForSeconds(frameTime);
        }
        //StartCoroutine(Beam());
    }

    private float RandomWidthOffset()
    {
        return widthRange.GetRandomValue;
    }

    private void CalculateMiddle()
    {
        Vector3 center = GetMiddleWithRandomness(startPoint.position, endPoint.position);

        _points[(int) PointIndex.POINT_CENTER] = center;
        _points[(int) PointIndex.POINT_MIDDLE_LEFT] = GetMiddleWithRandomness(startPoint.position, center);
        _points[(int) PointIndex.POINT_MIDDLE_RIGHT] = GetMiddleWithRandomness(center, endPoint.position);
    }

    private Vector3 GetMiddleWithRandomness(Vector3 point1, Vector3 point2)
    {
        return (point1 + point2) / 2.0f + Random.insideUnitSphere * randomPosOffset;
    }

    public void SetEndPoint(Vector3 pos)
    {
        endPoint.position = pos;
    }
}
