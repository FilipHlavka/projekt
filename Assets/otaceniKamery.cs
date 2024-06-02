using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otaceniKamery : MonoBehaviour
{
    public float speed = 100f;
    void Update()
    {
       
        float jakMoc = speed* Time.unscaledDeltaTime;


        transform.rotation *= Quaternion.Euler(0,jakMoc,0);
    }
}
