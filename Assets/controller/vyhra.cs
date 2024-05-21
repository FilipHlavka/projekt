using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class vyhra : MonoBehaviour
{   
    public static vyhra instance;
    [SerializeField]
    public static int pocetEnemy = 0; //nestíhá kontrola
    bool pom = false;
    public bool stuj = false;
    public bool nenene = false;
    [SerializeField]
    public static int pocetZivotu = 2;
   
    public static bool prohra;
    public UnityEvent konec;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (konec == null)
            konec = new UnityEvent();
        StartCoroutine(pockej());
        StartCoroutine(kontrolaNumerator());
    }

    // Update is called once per frame
    
    private void kontrolaVyhry()
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
                    foreach (var idk in list)
                    {
                        Destroy(idk);
                    }
                    vyhralHrac();
                }
            }
        }
    }
 
    private void vyhralHrac()
    {
        prohra = false;
        konec.Invoke();
    }

    IEnumerator kontrolaNumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            //kontrolaVyhry();
        }
      
       

    }
    IEnumerator pockej()
    {
   
        yield return new WaitForSeconds(0.3f);
        pom = true; 
       
    }
}
