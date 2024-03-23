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
    bool speedByl = false;
    bool rangeByl = false;
    bool shieldByl = false;
    bool vyditelnostByla = false;
    //bool pom = false;
    public int zaklRange;

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
            zaklRange = hrac.range;
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
                 PridejRangeBuff(st);
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
                speedByl = true;
                UdelejObr("speedBuff");

            }
            else if (st.druhSpeed == Speed.pomaly)
            {
                enemyPohyb.agent.speed = enemyPohyb.agent.speed * 0.6f;
                speedByl = true;
                UdelejObr("speedDeBuff");

            }
            else
            {
                enemyPohyb.agent.speed = Rychlost;
                OdstranObr(speedByl);
                speedByl = false;
            }
        }
        else
        {
            if (st.druhSpeed == Speed.rychly)
            {
                hrac.agent.speed = hrac.agent.speed * 1.5f;
                speedByl = true;
                UdelejObr("speedBuff");
            }
            else if (st.druhSpeed == Speed.pomaly)
            {
                hrac.agent.speed = hrac.agent.speed * 0.6f;
                speedByl = true;
                UdelejObr("speedDeBuff");
            }
            else
            {
                hrac.agent.speed = Rychlost + hrac.bonusRychlost;
                OdstranObr(speedByl);
                speedByl = false;
            }
        }
       
    }

    private void PridejShieldBuff(Staty st)
    {
        if (enemy)
        {
            if (st.stit == Shield.vic)
            {
                Enemy.aktDef = Enemy.aktDef * 1.25f;
                shieldByl = true;
                UdelejObr("shield");
            }
            else if (st.stit == Shield.min)
            {
                Enemy.aktDef = Enemy.aktDef * 0.5f;
                shieldByl = true;
                UdelejObr("antiShield");
            }
            else
            {
                Enemy.aktDef = Enemy.def;
                OdstranObr(shieldByl);
                shieldByl = false;
            }
        }
        else
        {
            if (st.stit == Shield.vic)
            {
                hrac.aktDef = hrac.aktDef * 1.25f;
                shieldByl = true;
                UdelejObr("shield");
            }
            else if (st.stit == Shield.min)
            {
                hrac.aktDef = hrac.aktDef * 0.5f;
                shieldByl = true;
                UdelejObr("antiShield");
            }
            else
            {
                hrac.aktDef = hrac.Def;
                OdstranObr(shieldByl);
                shieldByl = false;
            }
        }
    }

    private void PridejRangeBuff(Staty st)
    {
        if (st.range == Vyditelnost.vic)
        {
            hrac.maska.localScale = hrac.maska.localScale * 1.3f;
            vyditelnostByla = true;
            UdelejObr("oko");
        }
        else if (st.range == Vyditelnost.min)
        {
            hrac.maska.localScale = hrac.maska.localScale * 0.6f;
            vyditelnostByla = true;
            UdelejObr("neOko");
        }
        else
        {
            hrac.maska.localScale = new Vector3 (zaklRange, zaklRange, zaklRange);
            OdstranObr(vyditelnostByla);
            vyditelnostByla = false;
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
    private void OdstranObr(bool odstran)
    {
        if (odstran)
        {
            if (canvas != null)
            {
                foreach (GameObject stat in objekty)
                {
                    stat.transform.SetParent(null, false);
                }
                objekty.Clear();
            }
        }
        
    }

    private void PridejStatus(Staty st)
    {
        PridejSpeedBuff(st);
        PridejShieldBuff(st);
    }

   

    private void enemyCont()
    {
        if (stav == Stat.high)
        {
            enemyPohyb.AtkRange = enemyPohyb.AtkRange * 1.5f;
            UdelejObr("terc");
            rangeByl = true;
            Debug.Log(enemyPohyb.AtkRange);
        }
        else if (stav == Stat.low)
        {
            enemyPohyb.AtkRange = enemyPohyb.AtkRange * 0.75f;
            UdelejObr("neterc");
            rangeByl = true;
            Debug.Log(enemyPohyb.AtkRange);

        }
        else
        {
            enemyPohyb.AtkRange = zakRange;
            Debug.Log(enemyPohyb.AtkRange);
            OdstranObr(rangeByl);
            rangeByl = false;

        }
    }

    private void hracCont()
    {
        if (stav == Stat.high)
        {
            hrac.dosah = hrac.dosah * 1.5f;
            UdelejObr("terc");
            rangeByl = true;


        } else if (stav == Stat.low)
        {
            hrac.dosah = hrac.dosah * 0.75f;
            UdelejObr("neterc");
            rangeByl = true;
        }
        else
        {
            hrac.dosah = zakRange;
            OdstranObr(rangeByl);
            rangeByl = false;
        }
        Debug.Log(hrac.dosah);
    }
    
}
#region enumy
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
#endregion