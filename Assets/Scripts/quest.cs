using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "quest", menuName = "create quest", order = 1)]
public class quest : ScriptableObject
{

    public float time_limit = 0.0f;
    public float difficulty = 0.0f;
    public int reward = 0;
    public int cost = 0;
    public List<string> rooms = new List<string>();
    public int current_room = 0;


    public void loadRoom()
    {
        Debug.Log($"loading room {rooms[current_room]}");

        levelHandeler.instance.loadScene(rooms[current_room]);




    }


    public void checkReward()
    {
        current_room++;
        if(rooms.Count != current_room)
        {
            loadRoom();
            return;
        }

        //give rewards load hub

        worldHubManager.instance.GiveMoney(reward);
        current_room = 0;

        levelHandeler.instance.loadScene("hubWorld");

    }

    

    
    
}
