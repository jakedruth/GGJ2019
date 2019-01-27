using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public int reward;
    public bool IsOpened { get; private set; }

    public void OpenChest()
    {
        if (IsOpened)
            return;

        IsOpened = true;
        // TODO: give reward
        Destroy(transform.Find("ChestTop").gameObject);
    }
}
