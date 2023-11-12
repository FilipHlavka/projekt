using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pohybEnemy : MonoBehaviour
{
    [SerializeField]
    Transform player;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position-transform.position);
        
        
        if (hit.collider != null)
        {
           //Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.GetComponent<pohybHrace>() == null)
            {
                // Raycast hit something other than the player
                Debug.DrawRay(transform.position, player.position - transform.position, Color.red);

                // Perform actions for hitting something other than the player
            }
            else
            {
                // Raycast hit the player
                Debug.DrawRay(transform.position, player.position - transform.position, Color.green);
                agent.SetDestination(player.position);

                // Perform actions for hitting the player
            }


        }
        

    }

}
