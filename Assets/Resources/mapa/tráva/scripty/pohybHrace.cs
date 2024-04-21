using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using Unity.AI;
using UnityEngine.AI;
using System;

public class pohybHrace : MonoBehaviour
{
    Rigidbody2D rigitbody;
    SpriteRenderer spriteRenderer;
    Vector2 vec;
    //Vector2 direction;
    /*[SerializeField]
    float speed;*/
    bool flip = false;
    public NavMeshAgent agent;
    bool stuj = false;
    

    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false; // otoèilo by se to o 90%
        
        if (!cont.prvniInstance)
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
        
       
        rigitbody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
       
    }

   /* public void OnTriggerEnter2D(Collider2D collision)
    {
        rigitbody.velocity = Vector2.zero;
    }*/
   
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            //direction = (vec - rigitbody.position).normalized;

            agent.SetDestination(vec);


            /*   if (hit.collider != null || hitDva.collider != null || hitTri.collider != null)
               {
                   direction = Vector2.zero;

               }
            */

            if (vec.x < rigitbody.position.x)
            {
                flip = true;
            }
            else
            {
                flip = false;
            }

            // rigitbody.velocity = direction * speed;
        }
        if (Vector2.Distance(rigitbody.position, vec) < 0.1f)
        {
            rigitbody.velocity = Vector2.zero;
        }


        if (flip)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
