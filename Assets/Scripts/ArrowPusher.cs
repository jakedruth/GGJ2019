using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPusher : MonoBehaviour
{
    public float pushSpeed;
    public bool angled;

    Vector3 Direction
    {
        get { return angled ? (transform.right + transform.up).normalized : transform.right; }
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
