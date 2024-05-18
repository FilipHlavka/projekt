using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class vyhra : MonoBehaviour
{
    [SerializeField]
    public static int pocetEnemy = 0;
    bool pom = false;
    public static bool stuj = false;
    public static bool nenene = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(pockej());
    }

    // Update is called once per frame
    void Update()
    {
        if (!stuj)
        {
            if (pom)
            {
                if (pocetEnemy <= 0)
                {
                    Debug.Log("Vyhra");
                    nenene = true;
                    List<GameObject> list = GameObject.FindGameObjectsWithTag("svine").ToList();
                    foreach(var idk in list)
                    {
                        Destroy(idk);
                    }
                    vyhralHrac();
                }
            }
        }
        
        //Debug.Log(pocetEnemy + "pocet");
    }

    private void vyhralHrac()
    {
        
    }

    IEnumerator pockej()
    {
   
        yield return new WaitForSeconds(0.3f);
        pom = true; 
       
    }
}
