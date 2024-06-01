using FOVMapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class zakl : MonoBehaviour, IProSchopnost
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
    [SerializeField]
    public FOVAgent fovAgent;
    [SerializeField]
    List<GameObject> enemies;
    [SerializeField]
    public LayerMask maskaPrekazka;
    EnemyRespawn enresp; // mu�e b�t null
    public float dosah;
    UnityEvent<bool,GameObject> utoc;
    bool stuj = false;
    hracData hracdata;
    int blbost= 0;
    public string nameHr;
    //public Vector3 pozice;
    //public Vector3 rotace;
    
    GameObject controller;
    public float bonusRychlost;
    public float zaklRychlost;
    public float aktDef;
    public float rangePom;
    public SphereCollider kolajdr;
    [SerializeField]
    public GameObject RaycastObj;
    public List<Vector3> poziceLineRendereru;
    int vEnemy;
    
    public void Awake()
    {
        //agent = gameObject.GetComponent<NavMeshAgent>();
        //agent.speed = Rychlost; // rychlost
       
        dosah = (float)(range / 3.5);

       
       
    }

    public virtual void Start()
    {
        if (!cont.prvniInstance)
        {
            Destroy(gameObject);
           
        }
       

        Zacni();
       
    }

    private void Zacni()
    {
        //FOVManager.instance.FindAllFOVAgents();
       // Invoke("resetLineRenderer", 0.1f);
        if (utoc == null)
            utoc = new UnityEvent<bool,GameObject>();

        aktDef = Def;
        zaklRychlost = Rychlost;
        
                                                                                 // p�i�azen� dohledu !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        kolajdr = gameObject.GetComponent<SphereCollider>();
        kolajdr.radius = fovAgent.sightRange * rangePom;
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
        
        enemies = GameObject.FindGameObjectsWithTag("enemy").ToList(); // pozd�j� p�id�n� dal��ch eventem
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
          
           
        //updatePozice();
        
    }


    protected void Raycastuj()
    {
        


        foreach (GameObject enemy in enemies)
        {
                if (enemy != null)
                {
                if (Vector3.Distance(transform.position, enemy.transform.position) < range / 3)
                {

                    


                    bool hit = Physics.Raycast(RaycastObj.transform.position, enemy.transform.position - RaycastObj.transform.position, out RaycastHit hitRay, dosah, maskaPrekazka);


                    Debug.DrawRay(RaycastObj.transform.position, enemy.transform.position - transform.position, Color.yellow);
                   
                    if (hit && hitRay.collider != null && (hitRay.collider.CompareTag("enemy") || hitRay.collider.CompareTag("enemyCollider")))
                    {
                       // Debug.Log(hitRay.collider.name + " " + hitRay.collider.tag);
                        Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.red);

                        ukazZarovku.instance.sviti = true;
                       
                    
                       

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                           if (blbost == 0)
                                utoc.Invoke(false, enemy);

                            blbost++;
                        }

                    }
                    else
                    {
                      
                        ukazZarovku.instance.sviti = false;
                        //resetLineRenderer();

                    }

                    
                }
               
                      
                    
                }
          

        }

        
    }
    

    

    public void TakeDamage(int dmg)
    {
        if(Zivoty - dmg > 0)
            Zivoty -= dmg;
        else
            Zivoty = 1;
    }

    
}
