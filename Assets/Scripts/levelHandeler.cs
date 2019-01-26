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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void loadScene(string sceneName)
    {

        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
