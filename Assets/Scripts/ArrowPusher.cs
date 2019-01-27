using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPusher : MonoBehaviour
{
    public float pushSpeed;
    public Vector3 pushDirection;

    Vector3 Direction
    {
        get { return pushDirection.normalized; }
    }

    KeyValuePair<Vector3, float> Pair
    {
        get { return new KeyValuePair<Vector3, float>(Direction, pushSpeed); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity other = collision.GetComponent<Entity>();
        if (other != null)
        {
            Debug.Log($"pushing: {Pair.Key}", gameObject);
            other.PushDirections.Add(Pair);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity other = collision.GetComponent<Entity>();
        if (other != null)
        {
            other.PushDirections.Remove(Pair);
        }
    }
}
