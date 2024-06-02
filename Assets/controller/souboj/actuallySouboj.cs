using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class actuallSouboj : MonoBehaviour
{
    [SerializeField]
    public UkladaniProEnemy enemyVSouboji;
    [SerializeField]
    public UkladaniProHrac hracDt;
    prirazeniDoBoje boj;
    float enemyAtkMulti = 1;
    public UnityEvent<bool> vyhraEnemyOtaznik;
    public bool vyhraEnemy;
    bool konec = false;
    float hracAtkMulti = 1;
    bool aktSchopnost = false;
    int schopnostCislo;
    int doba = 0;
    bool muzu = false;
    int maxZivoty;
    int maxDef;

    [SerializeField]
    TMP_Text HrcHp;
    [SerializeField]
    Slider HrcHpSlider;
    [SerializeField]
    TMP_Text HrcSH;
    [SerializeField]
    Slider HrcSHSlider;
    [SerializeField]
    TMP_Text EnmHp;
    [SerializeField]
    Slider EnmHpSlider;
    [SerializeField]
    TMP_Text EnmSh;
    [SerializeField]
    Slider EnmShSlider;
    [SerializeField]
    TMP_Text Log;
    string zpoznenejText;
    bool jeZpozden= false;
    public UnityEvent<bool> pal;
    bool uzKonci = false;

    // Start is called before the first frame update
    void Awake()
    {
        if(vyhraEnemyOtaznik == null) 
            vyhraEnemyOtaznik = new UnityEvent<bool>();

        if (pal == null)
            pal = new UnityEvent<bool>();


        boj = gameObject.GetComponent<prirazeniDoBoje>();
        vyhraEnemyOtaznik.AddListener(boj.konecBoje);
        VylosovaniSchopnosti();
        Debug.Log(schopnostCislo + "schopnost");
       if(schopnostCislo == 0)
        {
            Log.text = "Your power is Bleeding";
           
        }else if(schopnostCislo == 1)
        {
            Log.text = "Your power is Healing";
        }
        zpoznenejText = Log.text;
   



    }
    public void Pridej()
    {
      
        hracDt = boj.hracDt;
        hracDt.def = (int)Mathf.Round(boj.hracDt.def);
        hracDt.zivoty = (int)Mathf.Round(boj.hracDt.zivoty);
        enemyVSouboji = boj.enemyVSouboji;
        enemyVSouboji.zivoty = (int)Mathf.Round(boj.enemyVSouboji.zivoty);
        enemyVSouboji.def = (int)Mathf.Round(boj.enemyVSouboji.def);
        maxZivoty = hracDt.zivoty;
        maxDef = (int)hracDt.def;
        HrcHpSlider.maxValue = maxZivoty;
        HrcSHSlider.maxValue = maxDef;
        EnmHpSlider.maxValue = enemyVSouboji.zivoty;
        EnmShSlider.maxValue = enemyVSouboji.def;
        aktUI();
        
        if (enemyVSouboji.jeNaTahu)
        {
            enemyVSouboji.jeNaTahu = false;
            Invoke("EnemyBoj",2);
        }

    }
    void aktUI()
    {
        HrcHp.text = hracDt.zivoty + " HP ";
        HrcSH.text = hracDt.def + " SH";
        EnmHp.text = enemyVSouboji.zivoty + " HP ";
        EnmSh.text = enemyVSouboji.def + " SH";

        HrcHpSlider.value = hracDt.zivoty;
        HrcSHSlider.value = hracDt.def;
        EnmHpSlider.value = enemyVSouboji.zivoty;
        EnmShSlider.value = enemyVSouboji.def;

    }


    // Update is called once per frame
    void Update()
    {
        if (!konec) {
            if (!enemyVSouboji.jeNaTahu && hracDt.zivoty > 0 && enemyVSouboji.zivoty > 0)
            {

                Bojuj();


            }
            else if (aktSchopnost && muzu)
            {
                Rozdelovac();
                muzu = false;
            }
            else if (hracDt.zivoty > 0 && enemyVSouboji.zivoty > 0)
            {
                if(!jeZpozden)
                StartCoroutine(Enmy());
            }
            else
            {
                if (enemyVSouboji.zivoty <= 0)
                {
                    vyhraEnemy = false;
                    enemyVSouboji.zivoty = 0;
                    aktUI();
                }
                else {
                    hracDt.zivoty = 0;
                    aktUI();
                    vyhraEnemy = true;
                } 

                konec = true;
                if(!uzKonci)
                Invoke("zkonci",2);
                uzKonci = true;
            }
        }
    }

    public void zkonci()
    {
        Log.text = "";
        vyhraEnemyOtaznik.Invoke(vyhraEnemy);
    }

    IEnumerator Enmy()
    {
        Debug.Log("aktivace schopnosti");
        jeZpozden = true;


        yield return new WaitForSeconds(2f);
        EnemyBoj(); // pøidat zpoždìní

        jeZpozden = false;


    }
    #region schopnostiABuffyDebuffy


    private void VylosovaniSchopnosti()
    {
        schopnostCislo = Random.Range(0,2);
    }

    private void Bleeding()
    {
        if (enemyVSouboji.def - (hracDt.atk/3 * hracAtkMulti) <= 0)
        {
            enemyVSouboji.zivoty += (int)(enemyVSouboji.def - (hracDt.atk/3 * hracAtkMulti));
            enemyVSouboji.def = 0;
        }
        else
            enemyVSouboji.def -= (hracDt.atk/3 * hracAtkMulti);

        aktUI();
        doba--;
        if(doba <= 0)
        {
            aktSchopnost = false;
        }
    }
    private void Rozdelovac()
    {
        if(schopnostCislo == 0)
        {
            Bleeding();
        }
        if (schopnostCislo == 1)
        {
            Healing();
        }
    }

   /* private void Debuff()
    {
        
    }*/

    private void Healing()
    {
        bool idk = true;
        if(hracDt.zivoty == maxZivoty)
        {
            if(hracDt.def == maxDef)
            {
                doba--;
                if (doba <= 0)
                {
                    aktSchopnost = false;
                }
                idk = false;
               
            }
            else
            {
            Debug.Log(hracDt.zivoty + " " + hracDt.def + "hraè");

                hracDt.def += Mathf.FloorToInt(maxDef / 10);
                if(hracDt.def >= maxDef)
                {
                    hracDt.def = maxDef;
                }
            }
        }


        hracDt.zivoty += Mathf.FloorToInt(maxZivoty / 10);
        Debug.Log(hracDt.zivoty + " " + hracDt.def + "hraè");

        if (maxZivoty <= hracDt.zivoty)
            hracDt.zivoty = maxZivoty;

        aktUI();

        if (idk)
        {
            doba--;

            if (doba <= 0)
            {
                aktSchopnost = false;
            }
        }
        
    }

    #endregion
    // ---------------------------------------------
    #region hrac

    private void Bojuj()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Utoc();
            Debug.Log("útok hráè");
            Log.text = "Player attack" + "\n" + zpoznenejText;
            zpoznenejText = "Player attack";
            Debug.Log(enemyVSouboji.zivoty + " " + enemyVSouboji.def + "enemy");
            aktUI();
            pal.Invoke(false);
            muzu = true;

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            hracDt.zivoty = 0;
            muzu = true;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            BranSe();
            Debug.Log("Defense hráè");
            Log.text = "Player defense" + "\n" + zpoznenejText;
            zpoznenejText = "Player defense";
            muzu = true;

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ZvisAtk();
            Debug.Log("buff hráè");
            Log.text = "Player buff" + "\n" + zpoznenejText;
            zpoznenejText = "Player buff";
            muzu = true;

        }
        if (Input.GetKeyDown(KeyCode.Space) && !aktSchopnost)
        {
            
           
           
            
            aktSchopnost = true;
            doba = Random.Range(1, 4);
            Debug.Log("POWER" + doba);
            Log.text = "Power for " +doba+" round(s)"+ "\n" + zpoznenejText;
            zpoznenejText = "Power for " + doba + " round(s)";
            muzu = true;

            enemyVSouboji.jeNaTahu = true;

        }


        

    }
    

    private void Utoc()
    {
        if (enemyVSouboji.def - (hracDt.atk * hracAtkMulti) <= 0)
        {
            enemyVSouboji.zivoty += (int)(enemyVSouboji.def - (hracDt.atk * hracAtkMulti));
            enemyVSouboji.def = 0;
        }
        else
            enemyVSouboji.def -= (hracDt.atk * hracAtkMulti);

        enemyVSouboji.jeNaTahu = true;

    }

    public void BranSe()
    {
        hracDt.def += 5;
        HrcSHSlider.maxValue += 5;
        enemyVSouboji.jeNaTahu = true;
        Debug.Log("hryc defence" + hracDt.def);
        aktUI();

    }
    public void ZvisAtk()
    {
        hracAtkMulti += (float)0.5;
        enemyVSouboji.jeNaTahu = true;

    }

    #endregion

    //---------------------------------------------
    #region enemy
    public void EnemyBoj()
    {
        int rnd = UnityEngine.Random.Range(0, 101);
        if (rnd <= 20)
        {
            BranSeEnemy();
            Debug.Log("štíty");
            Log.text = "Enemy defense" + "\n" + zpoznenejText;
            zpoznenejText = "Enemy defense";
        }
        if (rnd > 20 && rnd < 50)
        {
            ZvisAtkEnemy();
            Debug.Log("buff");
            Log.text = "Enemy buff" + "\n" + zpoznenejText;
            zpoznenejText = "Enemy buff";
        }
        if (rnd > 49)
        {
            UtocEnemy();
            Debug.Log(hracDt.zivoty + " " + hracDt.def + "hraè");
            aktUI();
            pal.Invoke(true);
            Debug.Log("útok");
            Log.text = "Enemy attack" + "\n" + zpoznenejText;
            zpoznenejText = "Enemy attack";
        }
        enemyVSouboji.jeNaTahu = false;
    }

    private void UtocEnemy()
    {
        if (hracDt.def - (enemyVSouboji.atk * enemyAtkMulti) <= 0)
        {
            hracDt.zivoty += (int)(hracDt.def - (enemyVSouboji.atk * enemyAtkMulti));
            hracDt.def = 0;
        }
        else
            hracDt.def -= (enemyVSouboji.atk * enemyAtkMulti);
    }

    public void BranSeEnemy()
    {
        enemyVSouboji.def += 5;
        EnmShSlider.maxValue += 5;
        aktUI();


    }
    public void ZvisAtkEnemy()
    {
        enemyAtkMulti += (float)0.5;

    }
    #endregion
}
