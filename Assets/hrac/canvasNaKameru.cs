using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasNaKameru : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject camera;
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(camera.transform.position);
    }
}
