using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float gameSpeedMultiplier { get; private set; } = 1.0f;

    public static float DeltaTime
    {
        get { return Time.deltaTime * gameSpeedMultiplier; }
    }

    public static float FidexDeltaTime
    {
        get { return Time.fixedDeltaTime * gameSpeedMultiplier; }
    }

    public static GameManager instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
