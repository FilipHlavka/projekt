using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class camPohyb : MonoBehaviour
{
    [SerializeField]
    public int speed;
    [SerializeField]
    float scrol;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Transform org;
    public float movex = 0;
    public float movey = 0;
    bool zoom = false;
    bool stuj = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (je > 0 && cam.orthographicSize > 2)
            {
                //Debug.Log(je);

                cam.orthographicSize -= scrol;
            }
            if (je < 0 && cam.orthographicSize < 20)
            {
                //je = 0;
                //Debug.Log(je);


                cam.orthographicSize += scrol;
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


            }
            else
            {
                cam.orthographicSize = 50;
                zoom = true;
            }
        }

        if (Input.GetKey(KeyCode.A) && cam.transform.position.x > -5.44f)
        {

            movex += -speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movex += speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.W))
        {
            movey += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && cam.transform.position.y > -6f)
        {
            movey += -speed * Time.deltaTime;
        }
        cam.transform.position = new Vector3(movex, movey, -10);
    }
}
