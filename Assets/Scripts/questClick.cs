using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questClick : MonoBehaviour
{


    public quest currentQuest;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadQuestRoom()
    {

        Debug.Log("loadroom");
        currentQuest.loadRoom();
    }
}
