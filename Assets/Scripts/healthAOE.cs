using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthAOE : MonoBehaviour
{

    public float damage_dealt = 0.0f;
    public bool affects_player = false;
    public bool affects_enemy = false;
    private List<Entity> affects_list;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Entity entity in affects_list)
        {
            if(entity.isPlayer & affects_player)
            {
                entity.DealDamage(GameManager.GameTime * damage_dealt);
            }

            if(!entity.isPlayer & affects_enemy)
            {
                entity.DealDamage(GameManager.GameTime * damage_dealt);
            }
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        affects_list.Add(collision.GetComponent<Entity>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        affects_list.Remove(collision.GetComponent<Entity>());
        
    }
}
