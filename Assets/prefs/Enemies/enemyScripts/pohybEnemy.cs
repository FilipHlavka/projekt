using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class pohybEnemy : MonoBehaviour
{
    
    Transform player;
    [SerializeField]
    LayerMask Mask;
    public NavMeshAgent agent;
   
    public float AtkRange = 0;
    public float dosahDetekce = 0;
    
    enemy Enemy;
    
    UnityEvent<bool,GameObject> utoc;
    bool stuj = false;


    // Start is called before the first frame update
  
    void Start()
    {
        
        Zacni();
       
        
    }
    public void Zastav()
    {
        stuj = !stuj;
       
    }
    private void Zacni()
    {
        if (utoc == null)
            utoc = new UnityEvent<bool,GameObject>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Enemy = gameObject.GetComponent<enemy>();

        
        AtkRange = Enemy.range;
        dosahDetekce = Enemy.dosahDetekce;
        agent.speed = Enemy.rychlost;


        utoc.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<cont>().Prepnuti);
    }

    // Update is called once per frame
    void Update()
    {
           
        Delej();

       

    }

    private void Delej()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, (float)dosahDetekce, Mask);


        if (hit.collider != null)
        {

            //Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.GetComponent<pohybHrace>() == null)
            {

                // Debug.DrawRay(transform.position, player.position - transform.position, Color.red);


            }
            else if (Vector2.Distance(transform.position, player.position) <= (float)AtkRange)
            {
                agent.isStopped = true;


                utoc.Invoke(true,gameObject);
                // utok enemy
            }
            else
            {
                agent.isStopped = false;

                //Debug.DrawRay(transform.position, player.position - transform.position, Color.green);
                agent.SetDestination(player.position);


            }


        }
    }
}
