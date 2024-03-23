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
    float Rychlost;
    zakl hrac;
    [SerializeField]
    Canvas canvas;
    Transform maska;
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
            Rychlost = enemyPohyb.agent.speed;
            zakRange = Enemy.range;
            
        }
        else
        {
            hrac = gameObject.GetComponent<zakl>();
            maska = hrac.maska;
            zakRange = hrac.dosah;
            Rychlost = hrac.Rychlost;
        }
    }

    private void Zmena(Staty st, Collider2D col)
    {
        if (col.name == gameObject.name)
        {
            stav = st.druh;
            if (enemy)
            {
              
                enemyCont(); // dosah atacku 
                
                PridejStatus(st); // ostatni
            }
            else
            {
                 hracCont();
                 PridejStatus(st);
            }
        }
    }
    private void PridejSpeedBuff(Staty st)
    {
        if (enemy)
        {
            if (st.druhSpeed == Speed.rychly)
            {
                enemyPohyb.agent.speed = enemyPohyb.agent.speed * 1.5f;
            }
            else if (st.druhSpeed == Speed.pomaly)
            {
                enemyPohyb.agent.speed = enemyPohyb.agent.speed * 0.6f;
            }
            else
            {
                enemyPohyb.agent.speed = Rychlost;
            }
        }
        else
        {
            if (st.druhSpeed == Speed.rychly)
            {
                hrac.agent.speed = hrac.agent.speed * 1.5f;
            }
            else if (st.druhSpeed == Speed.pomaly)
            {
                hrac.agent.speed = hrac.agent.speed * 0.6f;
            }
            else
            {
                hrac.agent.speed = Rychlost + hrac.bonusRychlost;
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

    private void PridejStatus(Staty st)
    {
        PridejSpeedBuff(st);
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
            


        } else if (stav == Stat.low)
        {
            hrac.dosah = hrac.dosah * 0.75f;
            UdelejObr("neterc");
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
public enum Speed
{
    rychly,
    pomaly,
    nic
}
public enum Shield
{
    vic,
    min,
    nic
}
public enum Vyditelnost
{
    vic,
    min,
    nic
}
