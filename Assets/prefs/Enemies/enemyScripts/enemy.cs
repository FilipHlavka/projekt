using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class enemy : MonoBehaviour
{
    [SerializeField]
    public int zivoty;
    [SerializeField]
    public int atk;
    [SerializeField]
    public float def;
    [SerializeField]
    public float rychlost;
    [SerializeField]
    public float range;
    [SerializeField]
    public float dosahDetekce;
    public float aktDef;
    //public bool JeEnemy = true;
   // data enemydata;
    public float atkMulti;
    public string nazev;
    public bool bojuje = false;
    public Vector3 pozice;
    public bool jeNaTahu = false;
    
    public abstract void Delej();
    protected void Awake()
    {
        vyhra.pocetEnemy++;

    }
    public void Update()
    {
        //updatePozice();
    }
    protected void Start()
    {

        aktDef = def;
    }
    private void updatePozice()
    {
        pozice = transform.position;
    }
    private void OnDestroy()
    {
        vyhra.pocetEnemy--;
    }

}
