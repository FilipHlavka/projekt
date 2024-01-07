using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class ingameStatusy : MonoBehaviour
{
    [SerializeField]
    bool enemy;
    pohybEnemy enemyPohyb;
    enemy Enemy;
    Stat stav = Stat.nic;
    float zakRange;
    zakl hrac;
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
            }
            else
            {
                hracCont();
            }
        }
        
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

        } else if (stav == Stat.low)
        {
            hrac.dosah = hrac.dosah * 0.75f;
        }
        else
        {
            hrac.dosah = zakRange;
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
