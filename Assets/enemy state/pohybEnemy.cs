using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class pohybEnemy : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    LayerMask Mask;
    NavMeshAgent agent;
    
    UnityEvent<bool> utoc;
   
    
    
    // Start is called before the first frame update

    void Start()
    {
            if (utoc == null)
            utoc = new UnityEvent<bool>();


        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;


       
        utoc.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<cont>().Prepnuti);
        

    }
    public void zk()
    {
        Debug.Log("lol");
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position-transform.position,10,Mask);
        
        
        if (hit.collider != null)
        {
            
           //Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.GetComponent<pohybHrace>() == null )
            {
               
               // Debug.DrawRay(transform.position, player.position - transform.position, Color.red);

               
            }else if(Vector2.Distance(transform.position, player.position) <= 6)
            {
                agent.isStopped = true;
                utoc.Invoke(true);
                // utok enemy
            }
            else
            {
                agent.isStopped= false;
                //Debug.DrawRay(transform.position, player.position - transform.position, Color.green);
                agent.SetDestination(player.position);

               
            }


        }
        

    }

}
