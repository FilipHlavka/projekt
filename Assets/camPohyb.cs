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
    float movex = 0;
    float movey = 0;
    bool zoom = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
                movex = 0;
                movey = 0;

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
