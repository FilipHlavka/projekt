using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class soubojContainer : MonoBehaviour
{
    
    cont controller;
    ingamemanager ingamemanager;
   /* public GameObject hrac;
    public GameObject enemy;*/
    UnityEvent<string> prepniScenu;
    public UnityEvent<bool> uloz;


    void Start()
    {
        if (uloz == null)
            uloz = new UnityEvent<bool>();
        if (prepniScenu == null)
            prepniScenu = new UnityEvent<string>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<cont>();
        controller.zacniBojovat.AddListener(DoBoje);
        ingamemanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ingamemanager>();
        prepniScenu.AddListener(ingamemanager.PrepniNascenu);

        
    }
        
    
    public void DoBoje(bool EnemyNaRade)
    {
       
       
        uloz.Invoke(EnemyNaRade);

        prepniScenu.Invoke("souboj");
        
        

    }
    
    void Update()
    {
        //Debug.Log(enemy+" "+hrac+" "+enemyNaTahu);

    }
}


