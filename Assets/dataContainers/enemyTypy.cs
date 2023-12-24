using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class enemyTypy : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> enemaci, hraci;
    [SerializeField]
    public List<string> enemyNames, playerNames;

    [SerializeField]
    public Dictionary<string, GameObject> slovnik = new Dictionary<string, GameObject>();

    //Zabal zabal;
    // Start is called before the first frame update
    void Awake()
    {
        Vytvor();
        
    }

    private void Vytvor()
    {
        
    }
    public void DejDoSceny()
    {
       
    }

   
    void Update()
    {
        
    }
}
