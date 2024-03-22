using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class zakl : MonoBehaviour
{
    [SerializeField]
    public int Zivoty;
    [SerializeField]
    public int Def;
    [SerializeField]
    public int range; // vyditelnost
    [SerializeField]
    public int Atk;
    [SerializeField]
    public float Rychlost;
    NavMeshAgent agent;
    Transform maska;
    [SerializeField]
    List<GameObject> enemies;
    [SerializeField]
    public LayerMask maskaPrekazka;
    EnemyRespawn? enresp;
    public float dosah;
    UnityEvent<bool,GameObject> utoc;
    bool stuj = false;
    hracData hracdata;
    int blbost= 0;
    public string nameHr;
    public Vector2 pozice;
    LineRenderer lineRenderer;
    GameObject controller;
    public void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = Rychlost; // rychlost
       
        dosah = (float)(range / 3.5);

        lineRenderer = GameObject.FindWithTag("line").GetComponent<LineRenderer>() ;
       
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    public virtual void Start()
    {
/*
        if (!cont.prvniInstance)
        {
            Destroy(gameObject);
        }
*/

        Zacni();
       
    }

    private void Zacni()
    {
        if (utoc == null)
            utoc = new UnityEvent<bool,GameObject>();


        
        maska = transform.Find("SpriteMask");
        maska.localScale = new Vector3(range, range, range); // pøiøazení dohledu
        AktualizujSeznamEnemy();
        
        Debug.Log(enemies.Count);
        controller = GameObject.FindGameObjectWithTag("GameController");
        enresp = controller.GetComponent<EnemyRespawn>();
        if(enresp != null)
        {
            enresp.enemyRespawn.AddListener(SeznamEnemyPridej);
        }
        utoc.AddListener(controller.GetComponent<cont>().Prepnuti);
    }

    public void AktualizujSeznamEnemy()
    {
        
        enemies = GameObject.FindGameObjectsWithTag("enemy").ToList(); // pozdìjš pøidání dalších eventem
        Debug.Log(enemies.Count);
    }
    public void SeznamEnemyPridej(GameObject enem)
    {
       
       enemies.Add(enem);
       Debug.Log(enemies);
    }

    public void Zastav()
    {
        //AktualizujSeznamEnemy();
        stuj = !stuj;
    }

    public virtual void Update()
    {
        
            if (!stuj)
                Raycastuj();
        updatePozice();
        
    }

    private void updatePozice()
    {
        pozice = transform.position;
    }

    protected void Raycastuj()
    {
        
           
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    if (Vector2.Distance(transform.position, enemy.transform.position) < range/3)
                    {
                    
                   
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, enemy.transform.position - transform.position,dosah,maskaPrekazka);
                        

                        Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.gray);

                        if (hit.collider != null)
                        {

                        

                            if (hit.collider.CompareTag("enemy"))
                            {

                            lineRenderer.positionCount = 2;

                            lineRenderer.SetPosition(0, transform.position);
                            lineRenderer.SetPosition(1, enemy.transform.position);
                            Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.red);
                                if (Input.GetKeyDown(KeyCode.Space))
                                {
                                    if(blbost == 0)
                                    utoc.Invoke(false, enemy);

                                    blbost++;
                                }

                            }
                            
                            
                           
                            





                        }
                    else
                    {
                        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
                        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
                        Debug.Log("aa");
                    }


                }

                
                }
            }
        
    }
  

}
