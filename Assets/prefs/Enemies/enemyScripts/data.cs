using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class data : MonoBehaviour
{
    [NonSerialized]         // script pouze pro základního enemy !!!
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
    public float atkMulti= 1;
    [NonSerialized]
    public float defenseMulti = 1;
    [NonSerialized]
    pohybEnemy pohyb;
    public void Start()
    {/*
        pohyb = GetComponent<pohybEnemy>();
        pohyb.agent.speed = rychlost;
        pohyb.AtkRange = range;
        pohyb.dosahDetekce = dosahDetekce;
       
        */
    }
    /*
    private void Update()
    {
        if (zivoty <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Bojuj(hracData hrac)
    {
        int rnd = UnityEngine.Random.Range(0, 101);
        if (rnd <= 20)
        {
            BranSe();
            Debug.Log("štíty");
        }
        if (rnd > 20 && rnd <50)
        {
            ZvisAtk();
            Debug.Log("buff");
        }
        if (rnd > 49)
        {
            Utoc(hrac);
            Debug.Log(hrac.zivoty + " " + hrac.def + "hraè");
            Debug.Log("útok");
        }
    }

    private void Utoc(hracData hrac)
    {
        if (hrac.def - (atk * atkMulti) <= 0)
        {
            hrac.zivoty += hrac.def - (atk * atkMulti);
            hrac.def = 0;
        }
        else
            hrac.def -= (atk * atkMulti);
    }

    public void BranSe()
    {
        defenseMulti += 5;
    }
    public void ZvisAtk()
    {
        atkMulti += (float)0.5;

    }*/
}
