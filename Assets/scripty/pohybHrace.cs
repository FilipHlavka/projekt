using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using Unity.AI;
using UnityEngine.AI;
using System;

public class pohybHrace : MonoBehaviour
{
    Rigidbody rigitbody;
    //SpriteRenderer spriteRenderer;
    Vector3 vec;
    //Vector2 direction;
    /*[SerializeField]
    float speed;*/
    
    public NavMeshAgent agent;
    bool stuj = false;
    public static bool nehejbat = false;
    GameObject camGm;
    Camera cam;

    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();


        if (!cont.prvniInstance) // muže dìlat problém !!!!!!!!!!!!!!!!!!
        {
            Destroy(gameObject);
            Debug.Log("zniceno");
        }

    }
    void Start()
    {
      

        Zacni();

    }
   

    private void Zacni()
    {
        
       
        rigitbody = gameObject.GetComponent<Rigidbody>();
        camGm = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camGm.GetComponent<Camera>();
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
       
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        rigitbody.velocity = Vector2.zero;
    }
   
    // Update is called once per frame
    void Update()
    {
       
        
            if (!stuj)
                HejbniSe();
            else
                agent.isStopped = true;
        
        
       
    }
    public void Zastav()
    {
        stuj = !stuj;
        agent.isStopped = false;
    }
    private void HejbniSe()
    {
        if(!nehejbat){ 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                LayerMask layerMask = ~LayerMask.GetMask("player");

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {

                    agent.SetDestination(hit.point);
                   // Debug.Log(hit.point);

                }



            }
        }
        if (Vector3.Distance(rigitbody.position, vec) < 0.1f)
        {
            rigitbody.velocity = Vector3.zero;
        }

    }
}
