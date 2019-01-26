using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldHubManager : MonoBehaviour
{

    public quest firstQuest;
    public quest secondQuest;
    public quest thirdQuest;


    public static worldHubManager instance;

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


        firstQuest.current_room = 0;
        secondQuest.current_room = 0;
        thirdQuest.current_room = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadQuestRoom(int questNum)
    {

        Debug.Log("loadroom");
        if (questNum == 1)
        {
            firstQuest.loadRoom();
        }else if (questNum == 2)
        {
            secondQuest.loadRoom();
        }else if (questNum == 3)
        {
            thirdQuest.loadRoom();
        }
    }

    public void checkQuestReward(int questNum)
    {

        Debug.Log("checking reward");
        if (questNum == 1)
        {
            firstQuest.checkReward();
        }
        else if (questNum == 2)
        {
            secondQuest.checkReward();
        }
        else if (questNum == 3)
        {
            thirdQuest.checkReward();
        }
    }
}
