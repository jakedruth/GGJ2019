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

    //private void Update()
    //{
    //    foreach (Entity e in entities)
    //    {
    //        Vector3 direction = angled ? (transform.right + transform.up).normalized : transform.right;
    //        e.pushDirection += direction;
    //        e.pushSpeed = Mathf.Max
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity other = collision.GetComponent<Entity>();
        if (other != null)
        {
            other.pushDirections.Add(Pair);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity other = collision.GetComponent<Entity>();
        if (other != null)
        {
            other.pushDirections.Remove(Pair);
        }
    }
}
