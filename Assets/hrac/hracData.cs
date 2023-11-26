using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

public class hracData : MonoBehaviour
{
    [NonSerialized]         // pro každý druh hrace jiný
    public float zivoty;
    [NonSerialized]
    public int atk;
    [NonSerialized]
    public float def;
    [NonSerialized]
    public float rychlost;
    [NonSerialized]
    public float range;
    [NonSerialized]
    public float dosahDetekce;
    [NonSerialized]
    public float atkMulti = 1;
    [NonSerialized]
    public float defenseMulti = 1;

    internal bool Bojuj(data enemy)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Utoc(enemy);
            Debug.Log("útok hráè");
            Debug.Log(enemy.zivoty + " " + enemy.def + "enemy");
            return true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            BranSe();
            Debug.Log("Defense hráè");
            return true;

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ZvisAtk();
            Debug.Log("buff hráè");
            return true;

        }
        return false;

    }
    private void Update()
    {
        if(zivoty <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Utoc(data enemy)
    {
        if (enemy.def - (atk * atkMulti) <= 0)
        {
            enemy.zivoty += enemy.def - (atk * atkMulti);
            enemy.def = 0;
        } 
        else
            enemy.def -= (atk * atkMulti);
        
    }
    
    public void BranSe()
    {
        defenseMulti += 5;
    }
    public void ZvisAtk()
    {
        atkMulti += (float)0.5;
    }
}
