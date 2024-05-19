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
    public NavMeshAgent agent;
    public Transform maska;
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
    public Vector3 pozice;
    public Vector3 rotace;
    LineRenderer lineRenderer;
    GameObject controller;
    public float bonusRychlost;
    public float zaklRychlost;
    public float aktDef;
    public SphereCollider kolajdr;
    [SerializeField]
    GameObject RaycastObj;
    
    public void Awake()
    {
        //agent = gameObject.GetComponent<NavMeshAgent>();
        //agent.speed = Rychlost; // rychlost
       
        dosah = (float)(range / 3.5);

        lineRenderer = GameObject.FindWithTag("line").GetComponent<LineRenderer>() ;
       
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    public virtual void Start()
    {
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        if (!cont.prvniInstance)
        {
            Destroy(gameObject);
           
        }
       

        Zacni();
       
    }

    private void Zacni()
    {
        if (utoc == null)
            utoc = new UnityEvent<bool,GameObject>();

        aktDef = Def;
        zaklRychlost = Rychlost;
        
                                                                                 // pøiøazení dohledu !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        kolajdr = gameObject.GetComponent<SphereCollider>();
        kolajdr.radius = (float)range/2;
        AktualizujSeznamEnemy();
        
        controller = GameObject.FindGameObjectWithTag("GameController");
        enresp = controller.GetComponent<EnemyRespawn>();
        if(enresp != null)
        {
            enresp.enemyRespawn.AddListener(SeznamEnemyPridej);
        }
        utoc.AddListener(cont.instance.Prepnuti);
        
    }

    public void AktualizujSeznamEnemy()
    {
        
        enemies = GameObject.FindGameObjectsWithTag("enemy").ToList(); // pozdìjš pøidání dalších eventem
       // Debug.Log(enemies.Count);
    }
    public void SeznamEnemyPridej(GameObject enem)
    {
       
       enemies.Add(enem);
      // Debug.Log(enemies);
    }

    public void Zastav()
    {
        AktualizujSeznamEnemy();
        stuj = !stuj;
    }

    public virtual void Update()
    {

        
        if (!stuj)
                Raycastuj();
            else
            {
            lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
            }
           
        //updatePozice();
        
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
                    if (Vector3.Distance(transform.position, enemy.transform.position) < range/3)
                    {


                        
                        bool hit = Physics.Raycast(RaycastObj.transform.position, enemy.transform.position - RaycastObj.transform.position, out RaycastHit hitRay, dosah, maskaPrekazka);


                        Debug.DrawRay(RaycastObj.transform.position, enemy.transform.position - transform.position, Color.yellow);

                        if (hitRay.collider != null)
                        {
                        Debug.Log("nyyyyyyyyyyyyyyyyyyyyaaaaaaaaaaa" + hitRay.collider);

                            if (hitRay.collider.CompareTag("enemy"))
                            {
                                

                                lineRenderer.positionCount = 2;

                                lineRenderer.SetPosition(0, RaycastObj.transform.position);
                                lineRenderer.SetPosition(1, enemy.transform.position);
                                Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.red);
                                if (Input.GetKeyDown(KeyCode.Space))
                                {
                                    if(blbost == 0)
                                    utoc.Invoke(false, enemy);

                                    blbost++;
                                }

                        }
                        else
                        {
                            lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
                            lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
                            
                        }










                    }
                        else
                        {
                        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
                        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
                       // Debug.Log("aa");
                        }


                    }
                    else
                    {
                    lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
                    lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
                    }

                
                }

            }

        
    }

    

}
