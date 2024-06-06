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
    public Transform playerTf;
   
    [SerializeField]
    LayerMask Mask;
    public NavMeshAgent agent;
    [SerializeField]
    GameObject raycastObj;
   
    public float AtkRange = 0;
    public float dosahDetekce = 0;
    bool vidiHrace = false;
    enemy Enemy;
   
    UnityEvent<bool,GameObject> utoc;
    //bool stuj = false;
    bool protekce = false;
    bool megaPom = true;
    bool mohu = true;
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

        try
        {
            playerTf = GameObject.FindGameObjectWithTag("PlayerCollider").transform;

        }
        catch
        {

        }


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
        if(cont.prvniInstance && playerTf != null && protekce && mohu)
        Checkuj();

        if (playerTf == null)
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
            playerTf = GameObject.FindGameObjectWithTag("PlayerCollider").transform;
        }
        catch { Debug.Log("zkouška"); }
        megaPom = true;
       
    }

    
    private void Checkuj()
    {
       
        //Debug.Log(vidiHrace);
        Vector3 dir = playerTf.position - raycastObj.transform.position;
        LayerMask ignore = LayerMask.GetMask("player","Water","Default","UI","enemy");
        if (Physics.Raycast(raycastObj.transform.position, dir, out RaycastHit hit, Vector3.Distance(playerTf.position, raycastObj.transform.position), ~ignore))
        {
            Debug.DrawRay(raycastObj.transform.position, dir, Color.blue);
           /*  Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.point);*/
            vidiHrace = !hit.collider.CompareTag("hora");
        }
        else
        {
            if (Vector3.Distance(transform.position, playerTf.position) <= dosahDetekce)
                vidiHrace = true;
        }

        if (Vector3.Distance(transform.position, playerTf.position) <= AtkRange && vidiHrace)
        {
           
            
               // agent.isStopped = true;
                utoc.Invoke(true, gameObject);
                mohu = false;
            
            
                        
                // utok enemy
        }
        else if(Vector3.Distance(transform.position, playerTf.position) <= dosahDetekce)
        {

            if (vidiHrace)
            {
                agent.isStopped = false;
               

                agent.SetDestination(playerTf.position);
            }
                


        }


        
    }
}
