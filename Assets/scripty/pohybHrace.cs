using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using Unity.AI;
using UnityEngine.AI;
using System;
using Unity.Burst.CompilerServices;

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
    Quaternion pomRotace;
    [SerializeField]
    public Terrain terrain;
    private Quaternion lookRotation;
    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
       // agent.updateRotation = false;

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
       // Narovnej(); fuj tajbl
        if(!nehejbat){ 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                LayerMask layerMask = ~LayerMask.GetMask("player");

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    
                    agent.SetDestination(hit.point);
                
                }



            }
        }
       

    }

    void Narovnej()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        LayerMask layerMask = ~LayerMask.GetMask("player");

        if (Physics.Raycast(ray, out RaycastHit hit, 5f, layerMask))
        {

            /*Vector3 normVektor = hit.normal;
            Quaternion rotace = Quaternion.FromToRotation(Vector3.up, normVektor);
            Quaternion pomRotation = Quaternion.Euler(rotace.eulerAngles.x, transform.rotation.eulerAngles.y , rotace.eulerAngles.z);
            
            transform.rotation = rotace;*/
            /*Vector3 normVektor = hit.normal;
             Quaternion rotace = Quaternion.FromToRotation(Vector3.up, normVektor);
             Vector3 pomRotation = rotace.eulerAngles;
             pomRotation.y = transform.rotation.eulerAngles.y;

             transform.rotation = Quaternion.Euler(pomRotation);*/


            
        }
        
        
    }
}
