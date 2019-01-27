using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class Pipe : MonoBehaviour
{
    public ParticleSystem spark;
    public Transform path;
    private LineRenderer _lr;
    public float totalTimeForSpark;
    private float _timer;
    private float _totalLength;
    private bool IsRunning;

    [Tooltip("If negative, the onEndSPark will only be called once")]
    public float timeBetweenRecallOnEndSpark = -1;
    public UnityEvent onEndSpark;

    public void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _lr.positionCount = path.childCount;
        for (int i = 0; i < path.childCount; i++)
        {
            _lr.SetPosition(i, path.GetChild(i).position);
        }

        CalculateTotalLengthOfLine();
    }

    [ContextMenu("Start Spark")]
    public void StartSpark()
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

        for (int i = 0; i < path.childCount - 1; i++)
        {
            Vector3 a = path.GetChild(i).position;
            Vector3 b = path.GetChild(i + 1).position;
            float dist = (b - a).magnitude;
            float distPercentage = dist / _totalLength;
            float timePerSegment = totalTimeForSpark * distPercentage;
            float timer = 0;
            while(timer < timePerSegment)
            {
                timer += GameManager.DeltaTime;
                float percentage = timer / timePerSegment;

                Vector3 pos = Vector3.Lerp(a, b, percentage);
                spark.transform.position = pos;

                yield return null;
            }
        }

        IsRunning = false;

        spark.Stop();

        onEndSpark.Invoke();

        if (timeBetweenRecallOnEndSpark > 0)
        {
            yield return new WaitForSeconds(timeBetweenRecallOnEndSpark * GameManager.gameSpeedMultiplier);
            onEndSpark.Invoke();
        }
    }

    private void CalculateTotalLengthOfLine()
    {
        _totalLength = 0;
        for (int i = 0; i < path.childCount - 1; i++)
        {
            Vector3 a = path.GetChild(i).position;
            Vector3 b = path.GetChild(i + 1).position;
            float dist = (b - a).magnitude;
            _totalLength += dist;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(path.GetChild(0).position, Vector3.one * 0.2f);
        for (int i = 0; i < path.childCount - 1; i++)
        {
            Vector3 a = path.GetChild(i).position;
            Vector3 b = path.GetChild(i + 1).position;
            Gizmos.DrawLine(a, b);
            Gizmos.DrawCube(b, Vector3.one * 0.2f);
        }
    }
}
