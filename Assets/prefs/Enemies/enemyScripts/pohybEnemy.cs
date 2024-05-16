using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class pohybEnemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField]
    public Transform player;
    [SerializeField]
    LayerMask Mask;
    public NavMeshAgent agent;
   
    public float AtkRange = 0;
    public float dosahDetekce = 0;
    bool vidiHrace = false;
    enemy Enemy;
   
    UnityEvent<bool,GameObject> utoc;
    //bool stuj = false;
    bool protekce = false;
    bool megaPom = true;
    bool pomNaNeco = true;
    // Start is called before the first frame update
    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
       
        if (!cont.prvniInstance)        // muže být problém!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! <byl>
            Destroy(gameObject);
    }

    void Start()
    {
       

        Zacni();
       
        
    }
    /*public void Zastav()
    {
        stuj = !stuj;
       
    }*/
    private void Zacni()
    {

        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (utoc == null)
            utoc = new UnityEvent<bool,GameObject>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        Enemy = gameObject.GetComponent<enemy>();

        
        AtkRange = Enemy.range;
        dosahDetekce = Enemy.dosahDetekce;
        agent.speed = Enemy.rychlost;

        StartCoroutine(Protection());
        utoc.AddListener(cont.instance.Prepnuti);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cont.prvniInstance && player != null && protekce && pomNaNeco)
        Delej();

        if (player == null)
        {
            /* try { 
                 player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

                 }
             catch { }*/
            if(megaPom)
            StartCoroutine(pockej());
        }
        

    }
   
    IEnumerator Protection()
    {
        yield return new WaitForSeconds(5);
        protekce = true;
    }
    IEnumerator pockej()
    {
        megaPom = false;
        yield return new WaitForSeconds(1.5f);
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        }
        catch { Debug.Log("zkouška"); }
        megaPom = true;
       
    }

    
    private void Delej()
    {
            Debug.Log(vidiHrace);
        Vector3 dir = player.position - transform.position;
        LayerMask ignore = LayerMask.GetMask("player");
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, Mathf.Infinity, ~ignore))
        {
            Debug.DrawRay(transform.position, dir.normalized * hit.distance, Color.blue);
            // Debug.Log("aaa");
            vidiHrace = !hit.collider.CompareTag("hora");
        }
        else
        {
            if (Vector3.Distance(transform.position, player.position) <= dosahDetekce)
                vidiHrace = true;
        }

        if (Vector3.Distance(transform.position, player.position) <= AtkRange && vidiHrace)
        {
           
            
                agent.isStopped = true;
                utoc.Invoke(true, gameObject);
                pomNaNeco = false;
            
            
                        
                // utok enemy
        }
        else if(Vector3.Distance(transform.position, player.position) <= dosahDetekce)
        {
            

            if (vidiHrace)
            {
                agent.isStopped = false;

                agent.SetDestination(player.position);
            }
                


        }


        
    }
}
