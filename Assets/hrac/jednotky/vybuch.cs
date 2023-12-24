using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vybuch : MonoBehaviour
{
    Vector2 zv = new Vector2(1,1);

    public float raycastDistance = 10f;

    void Update()
    {
        RaycastInDirection(Vector3.forward);

       
        RaycastInDirection(Vector3.back);
    }

    void RaycastInDirection(Vector3 direction)
    {
      
        Ray ray = new Ray(transform.position, direction);

        
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
           
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
            Debug.Log("Hit something at: " + hit.point);
        }
        else
        {
           
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);
        }
    }
}
