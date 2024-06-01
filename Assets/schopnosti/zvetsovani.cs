using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zvetsovani : MonoBehaviour
{
    [SerializeField]
    ParticleSystem sys;
    [SerializeField]
    float pust;
    [SerializeField]
    float rozsirO;
    [SerializeField]
    float rychlost;

    private void Start()
    {
        //sys.shape.radius
        Invoke("pockej",pust);
    }
    private void Update()
    {
       
    }
    private void pockej()
    {
        StartCoroutine(rozsir());
    }

    IEnumerator rozsir()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            var tvar = sys.shape;
            tvar.radius += rozsirO;
           
        }
        
    }
}

