using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class soubojContainer : MonoBehaviour
{
    
   
    ingamemanager ingamemanager;
   /* public GameObject hrac;
    public GameObject enemy;*/
    UnityEvent<string, bool,float> prepniScenu;
    public UnityEvent<bool> uloz;


    void Start()
    {
        if (uloz == null)
            uloz = new UnityEvent<bool>();
        if (prepniScenu == null)
            prepniScenu = new UnityEvent<string,bool,float>();
        
        cont.instance.zacniBojovat.AddListener(DoBoje);
        ingamemanager = ingamemanager.instance;
        prepniScenu.AddListener(ingamemanager.PrepniNascenu);

        
    }
        
    
    public void DoBoje(bool EnemyNaRade)
    {
       
       
        uloz.Invoke(EnemyNaRade);

        prepniScenu.Invoke("souboj",false,0);
        
        

    }
    
    void Update()
    {
        //Debug.Log(enemy+" "+hrac+" "+enemyNaTahu);

    }
}


