using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple.ReplayKit;
using UnityEngine.Events;

public class zakl : MonoBehaviour
{
    [SerializeField]
    public int Zivoty;
    [SerializeField]
    public int Def;
    [SerializeField]
    protected int range; // vyditelnost
    [SerializeField]
    public int Atk;                     // kontrolovat vzdálenost pøed raycastem
    [SerializeField]
    public float Rychlost;
    NavMeshAgent agent;
    Transform maska;
    [SerializeField]
    bool strelbaPresPrekazky;
    List<GameObject> enemies;
    [SerializeField]
    public LayerMask enemyMaska;
    [SerializeField]
    public LayerMask maskaPrekazka;
    [SerializeField]
    float dosah;
    UnityEvent<bool,GameObject> utoc;
    bool stuj = false;
    public void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        Debug.Log("rychlost" + Rychlost);
        agent.speed = Rychlost; // rychlost
        gameObject.GetComponent<hracData>().zivoty = Zivoty;
        gameObject.GetComponent<hracData>().atk = Atk;
        gameObject.GetComponent<hracData>().def = Def;
        gameObject.GetComponent<hracData>().rychlost = Rychlost;
        gameObject.GetComponent<hracData>().range = dosah;
        
    }

    public void Start()
    {
        
        Zacni();

       


    }

    private void Zacni()
    {
        if (utoc == null)
            utoc = new UnityEvent<bool, GameObject>();



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

    public void Update()
    {
        
            if (!stuj)
                Raycastuj();
        
        
    }
    
    protected void Raycastuj()
    {
        
           
            foreach (GameObject enemy in enemies)
            {
            if (enemy != null)
            {


                if (strelbaPresPrekazky)
                {

                    RaycastHit2D hit = Physics2D.Raycast(transform.position, (enemy.transform.position - transform.position).normalized, dosah, enemyMaska);





                    Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.gray);

                    if (hit.collider != null)
                    {


                        if (hit.collider.CompareTag("enemy"))
                        {

                            Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.red);
                            if (Input.GetButtonDown("Jump"))
                            {
                                utoc.Invoke(false, enemy);
                            }

                        }

                    }

                }
                if (!strelbaPresPrekazky)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, enemy.transform.position - transform.position, dosah, maskaPrekazka);




                    Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.gray);

                    if (hit.collider != null)
                    {


                        if (hit.collider.CompareTag("enemy"))
                        {

                            Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.red);
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                utoc.Invoke(false, enemy);
                            }

                        }

                    }
                }
            }
            }
        
    }
  

}
