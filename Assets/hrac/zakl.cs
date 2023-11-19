using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple.ReplayKit;
using UnityEngine.Events;

public class zakl : MonoBehaviour
{
    [SerializeField]
    protected int zivoty;
    [SerializeField]
    protected int range;
    [SerializeField]
    protected int rychlost;
    NavMeshAgent agent;
    Transform maska;
    [SerializeField]
    bool strelbaPresPrekazky;
    GameObject[] enemies;
    [SerializeField]
    public LayerMask enemyMaska;
    [SerializeField]
    public LayerMask maskaPrekazka;
    [SerializeField]
    int dosah;
    UnityEvent<bool> utoc;
   


    public void Start()
    {

        if (utoc == null)
            utoc = new UnityEvent<bool>();
        agent = gameObject.GetComponent<NavMeshAgent>();


        agent.speed = rychlost; // rychlost
        maska = transform.Find("SpriteMask");
        maska.localScale = new Vector3 (range, range, range); // pøiøazení dohledu
        enemies = GameObject.FindGameObjectsWithTag("enemy"); // enemákù bude fixní poèet takže tohle staèí
        Debug.Log(enemies.Length);

        utoc.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<cont>().Prepnuti);


    }

    public void Update()
    {

        Raycastuj();
       
        
    }
    
    protected void Raycastuj()
    {
       
        foreach (GameObject enemy in enemies)
        {
            
            if (strelbaPresPrekazky) { 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, enemy.transform.position - transform.position, dosah, enemyMaska);

               



                Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.gray);

            if (hit.collider != null)
            {


                if (hit.collider.CompareTag("enemy"))
                {

                    Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.red);
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            utoc.Invoke(false);
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
                            utoc.Invoke(false);
                        }

                    }

                }
            }

        }
    }
  

}
