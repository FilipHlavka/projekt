//using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class ingameStatusy : MonoBehaviour
{
    [SerializeField]
    bool enemy;
    pohybEnemy enemyPohyb;
    enemy Enemy;
    Stat stav = Stat.nic;
    float zakRange;
    zakl hrac;
    [SerializeField]
    Canvas canvas;
    List<GameObject> objekty = new List<GameObject>();
   
    //bool pom = false;

    // Start is called before the first frame update
    void Start()
    {
        
        akceProStatus.nastavStatus.AddListener(Zmena);
        if (enemy)
        {
            enemyPohyb = gameObject.GetComponent<pohybEnemy>();
            Enemy = gameObject.GetComponent<enemy>();
            
            zakRange = Enemy.range;
            
        }
        else
        {
            hrac = gameObject.GetComponent<zakl>();
            zakRange = hrac.dosah;
        }
    }

    private void Zmena(Stat druh, Collider2D col)
    {
        if (col.name == gameObject.name)
        {
            stav = druh;
            if (enemy)
            {
              
                enemyCont();
                
                enemyStatus();
            }
            else
            {
                 hracCont();
                 hracStatus();
            }
        }
    }
    private void UdelejObr(string jmeno)
    {
        GameObject obrObj = new GameObject(jmeno);
        Image obr = obrObj.AddComponent<Image>();

        obr.sprite = Resources.Load<Sprite>("statusy/" + jmeno);
        if (canvas != null && obr.sprite != null && obrObj != null)
        {
            obrObj.transform.SetParent(canvas.transform, false);
            objekty.Add(obrObj);

        }

    }
    private void OdstranObr()
    {
        if (canvas != null )
        {
            foreach (GameObject stat in objekty)
            {
                stat.transform.SetParent(null,false);
            }
            objekty.Clear();
        }
    }

    private void enemyStatus()
    {

    }

    private void hracStatus()
    {

    }

    private void enemyCont()
    {
        if (stav == Stat.high)
        {
            enemyPohyb.AtkRange = enemyPohyb.AtkRange * 1.5f;
            Debug.Log(enemyPohyb.AtkRange);
        }
        else if (stav == Stat.low)
        {
            enemyPohyb.AtkRange = enemyPohyb.AtkRange * 0.75f;
            Debug.Log(enemyPohyb.AtkRange);

        }
        else
        {
            enemyPohyb.AtkRange = zakRange;
            Debug.Log(enemyPohyb.AtkRange);

        }
    }

    private void hracCont()
    {
        if (stav == Stat.high)
        {
            hrac.dosah = hrac.dosah * 1.5f;
            UdelejObr("terc"); 
            UdelejObr("terc");


        } else if (stav == Stat.low)
        {
            hrac.dosah = hrac.dosah * 0.75f;
        }
        else
        {
            hrac.dosah = zakRange;
            OdstranObr();
        }
        Debug.Log(hrac.dosah);
    }
    
}
public enum Stat
{
    high,
    low,
    nic
}
