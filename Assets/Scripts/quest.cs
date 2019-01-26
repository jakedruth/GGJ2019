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
    private int current_room = 0;
    
    //modifyers

    public void loadRoom()
    {
        Debug.Log($"loading room {rooms[current_room]}");





    }


    public void checkReward()
    {
        current_room++;
        if(rooms[current_room] != null)
        {
            loadRoom();
            return;
        }

        //give rewards load hub


        current_room = 0;

    }

    

    
    
}
