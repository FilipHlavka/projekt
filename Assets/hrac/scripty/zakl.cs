using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple.ReplayKit;
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
    List<GameObject> enemies;
    [SerializeField]
    public LayerMask maskaPrekazka;
    
    public float dosah;
    UnityEvent<bool,GameObject> utoc;
    bool stuj = false;
    hracData hracdata;
    int blbost= 0;
    public string nameHr;
    public Vector2 pozice;
    public void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        hracdata = gameObject.GetComponent<hracData>();
        //Debug.Log("rychlost" + Rychlost);
        agent.speed = Rychlost; // rychlost
        hracdata.zivoty = Zivoty;
        hracdata.atk = Atk;
        hracdata.def = Def;
        hracdata.rychlost = Rychlost;
        hracdata.range = dosah;
        dosah = (float)(range / 3.5);

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

        utoc.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<cont>().Prepnuti);
    }

    public void AktualizujSeznamEnemy()
    {
        
        enemies = GameObject.FindGameObjectsWithTag("enemy").ToList(); // pozdìjš pøidání dalších eventem
        Debug.Log(enemies.Count);
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
                                

                                Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.red);
                                if (Input.GetKeyDown(KeyCode.Space))
                                {
                                    if(blbost == 0)
                                    utoc.Invoke(false, enemy);

                                    blbost++;
                                }

                            }

                        }
                    }

                
                }
            }
        
    }
  

}
