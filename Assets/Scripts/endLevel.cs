using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLevel : MonoBehaviour
{
    public string hubWorld = "hubWorld";

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();
        if (entity != null && entity.isPlayer)
        {
            levelHandeler.instance.loadScene(hubWorld);
        }
    }
}
