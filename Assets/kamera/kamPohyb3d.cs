using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class kamPohyb3d : MonoBehaviour
{
    [SerializeField]
    public int speed;
    [SerializeField]
    float scrol;
    [SerializeField]
    Camera cam;

   /* [SerializeField]
    Transform org;*/
    public float movex = 0;
    public float movez = 0;
    bool zoom = false;
    bool stuj = false;
    
    //Transform bod;
    
    [SerializeField]
    Transform dal;
    Vector3 smer;
    Vector3 normVector;
    /*float a;
    float b;
    float c;
    float a2;
    float b2;
    float c2;*/
    Quaternion otoc;
  
    void Start()
    {

       // bod = gameObject.GetComponent<Transform>();
        
    }
    public void Zastav()
    {
        stuj = !stuj;
        cam.enabled = !cam.enabled;
    }
    // Update is called once per frame
    void Update()
    {
        if (!stuj)
            HniSe();

    }

  
  
    private void HniSe()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float je = Input.GetAxis("Mouse ScrollWheel");
            if (je > 0)
            {
                smer = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                
                cam.transform.position -= smer * speed * Time.deltaTime * 5;
            }
            if (je < 0)
            {
                smer = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                if (Vector3.Distance(transform.position, cam.transform.position) < 50)
                    cam.transform.position += smer * speed * Time.deltaTime * 5;
            }

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {

            if (zoom)
            {
                zoom = false;
                cam.orthographicSize = 10;
                /*movex = org.position.x;
                movey = org.position.y;*/
                speed = speed / 3;

            }
            else
            {
                cam.orthographicSize = 50;
                speed = speed * 3;
                zoom = true;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {

            normVector = Vector3.Cross(
                new Vector3(cam.transform.position.x, 0, cam.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z)
                );
            
            transform.position -= normVector * speed * Time.deltaTime /9 / (Vector3.Distance(transform.position, cam.transform.position) / 40);

        }
        if (Input.GetKey(KeyCode.D))
        {


            normVector = Vector3.Cross(
                new Vector3(cam.transform.position.x, 0, cam.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z)
                );

            transform.position += normVector * speed * Time.deltaTime  /9 / (Vector3.Distance(transform.position, cam.transform.position) / 40);

        }
        if (Input.GetKey(KeyCode.W))
        {

            smer = new Vector3(cam.transform.position.x, 0, cam.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
            transform.position -= smer * speed * Time.deltaTime * 9 / (Vector3.Distance(transform.position, cam.transform.position)/10);

        }
        if (Input.GetKey(KeyCode.S))
        {
            smer = new Vector3(cam.transform.position.x,0,cam.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
            transform.position += smer * speed * Time.deltaTime * 9 / (Vector3.Distance(transform.position, cam.transform.position)/10 );
        }


       

        if (Input.GetKey(KeyCode.Q))
        {
            otoc =  Quaternion.Euler(0f, -speed * Time.deltaTime * 20, 0f);
            transform.rotation *= otoc;
                
           
        }
        if (Input.GetKey(KeyCode.E))
        {
            otoc = Quaternion.Euler(0f, speed * Time.deltaTime * 20, 0f);
            transform.rotation *= otoc;


        }

    


    }
    
}
