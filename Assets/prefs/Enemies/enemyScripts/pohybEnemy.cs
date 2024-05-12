using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        agent.updateRotation = false;
        agent.updateUpAxis = false;
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
    void Update()
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
        Debug.Log("co se to dìje");
            if (Vector3.Distance(transform.position, player.position) <= AtkRange)
            {
                    agent.isStopped = true;

                    
                     utoc.Invoke(true,gameObject);
                    pomNaNeco = false;
                        
                // utok enemy
            }
            else if(Vector3.Distance(transform.position, player.position) <= dosahDetekce)
            {
                agent.isStopped = false;

                //Debug.DrawRay(transform.position, player.position - transform.position, Color.green);
                agent.SetDestination(player.position);


            }


        
    }
}
