using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(0, 1)]
    private static float _gameSpeedMultiplier = 1.0f;

    public static float DeltaTime
    {
        get { return Time.deltaTime * _gameSpeedMultiplier; }
    }

    public static float FidexDeltaTime
    {
        get { return Time.fixedDeltaTime * _gameSpeedMultiplier; }
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
