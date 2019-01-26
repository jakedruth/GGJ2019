using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSwitchOnTrigger : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera vcamOnEnter;

    public void Awake()
    {
        vcamOnEnter.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{name} collided with: {collision.transform.name}");
        if(collision.tag == "Player")
        {
            vcamOnEnter.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            vcamOnEnter.enabled = false;
        }
    }
}
