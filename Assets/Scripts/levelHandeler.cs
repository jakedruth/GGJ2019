using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class levelHandeler : MonoBehaviour
{

    public static levelHandeler instance;

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

 
    public void loadScene(string sceneName)
    {

        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
