using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Pipe : MonoBehaviour
{
    public ParticleSystem spark;
    private LineRenderer _lineRenderer;
    public float totalTimeForSpark;
    private float _timer;
    private float _totalLength;
    private bool IsRunning;

    public void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        CalculateTotalLengthOfLine();
    }

    [ContextMenu("Start Spark")]
    public void StartTimer()
    {
        if(!IsRunning)
        {
            IsRunning = true;
            StartCoroutine(MoveSpark());
        }
    }

    public IEnumerator MoveSpark()
    {
        spark.Play();

        for (int i = 0; i < _lineRenderer.positionCount - 1; i++)
        {
            Vector3 a = _lineRenderer.GetPosition(i);
            Vector3 b = _lineRenderer.GetPosition(i + 1);
            float dist = (b - a).magnitude;
            float distPercentage = dist / _totalLength;
            float timePerSegment = totalTimeForSpark * distPercentage;
            float timer = 0;
            while(timer < timePerSegment)
            {
                timer += GameManager.DeltaTime;
                float percentage = timer / timePerSegment;

                Vector3 pos = Vector3.Lerp(a, b, percentage);
                spark.transform.localPosition = pos;

                yield return null;
            }
        }

        IsRunning = false;
        spark.Stop();
    }

    private void CalculateTotalLengthOfLine()
    {
        _totalLength = 0;
        for (int i = 0; i < _lineRenderer.positionCount - 1; i++)
        {
            float dist = (_lineRenderer.GetPosition(i + 1) - _lineRenderer.GetPosition(i)).magnitude;
            _totalLength += dist;
        }
    }
}
