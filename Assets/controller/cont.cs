using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.U2D;
using Unity.VisualScripting;
using NUnit;
using FOVMapping;

public class cont : MonoBehaviour
{
    public static cont instance;
    [SerializeField]
    camPohyb kamera;
   
    [SerializeField]
    GameObject KontrolerSouboj;
    public UnityEvent<bool> zacniBojovat;
    [SerializeField]
    public Zabal zabal = new Zabal();
    #region pridelovani
    #endregion
    public static bool prvniInstance = true;

    public UnityEvent Respawn;
    public UnityEvent<bool,Zavin> aktBudovy;
    List<GameObject> listEnemaku;
    public nacteniSceny nacitani;

    Zavin strudl;
    [SerializeField]
    public string mapa;
    [SerializeField]
    hracScriptable hrci;
    [SerializeField]
    enemyScriptable enmci;
    
   // public int pocetZivotu;

    void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        //Debug.Log(prvniInstance);
    }

    private void Start()
    {
     
        Respawn.AddListener(respawnScript.instance.DelejNeco);

        nacitani = gameObject.GetComponent<nacteniSceny>();
        nacitani.eventNacteni.AddListener(AktivujNacteni);

        if (!prvniInstance)
        {
            prvniInstance = true;
            
            aktBudovy.Invoke(false,new Zavin());
            listEnemaku = GameObject.FindGameObjectsWithTag("enemy").ToList();
            vyhra.instance.stuj = true;
            foreach (GameObject obj in listEnemaku)
            {
                //listEnemaku.Remove(obj);
                Debug.Log("Destroying " + obj.name);
                Destroy(obj);

                //obj.tag = "svine";

            }
            RozdelAPanuj();
            ukazHrace();
            
            vyhra.instance.stuj = false;
            vytvarecBudov.muzePricist = true;
            Zivoty.instance.aktText();
            FOVManager.instance.FindAllFOVAgents();

        }

        if (zacniBojovat == null)
            zacniBojovat = new UnityEvent<bool>();
    }

    private void AktivujNacteni(Zavin strudl)
    {
        
        vyhra.instance.stuj = true;
        pocitadlo.instance.timer = strudl.timer;
        listEnemaku = GameObject.FindGameObjectsWithTag("enemy").ToList();
        foreach (GameObject obj in listEnemaku)
        {
            //listEnemaku.Remove(obj);
            Debug.Log("Destroying " + obj.name);
            Destroy(obj);
           
            //obj.tag = "svine";
           
        }
        //listEnemaku = GameObject.FindGameObjectsWithTag("enemy").ToList();

        //Debug.Log(listEnemaku[0] + "pocet kurwa");
        this.strudl = strudl;
        PowerPointGenerator.instance.ZmenText(0,strudl.powePoints);
        PowerPointGenerator.instance.mena = strudl.powePoints;
        NactiEnemaky();
        ukazHraceDoSceny(strudl);
        aktBudovy.Invoke(true,strudl);
        vyhra.instance.stuj = false;
        vytvarecBudov.muzePricist = true;

        Zivoty.instance.aktText();
        FOVManager.instance.FindAllFOVAgents();

    }



    #region hracDoSceny

    public void ukazHraceDoSceny(Zavin strudl)
    {

          zakl hrDt;
          UkladaniProHrac hrJednodusiData = new UkladaniProHrac();
          hrJednodusiData.atk = strudl.hrDt.atk;
          hrJednodusiData.def = strudl.hrDt.def;
          hrJednodusiData.nazev = strudl.hrDt.nazev;
          hrJednodusiData.zivoty = strudl.hrDt.zivoty;
          hrJednodusiData.pozice = new Vector3(strudl.hrDt.X,strudl.hrDt.Y, strudl.hrDt.Z);
          hrJednodusiData.rotace = new Vector3(strudl.hrDt.Xrotation,strudl.hrDt.Yrotation,strudl.hrDt.Zrotation);
          Debug.Log(hrJednodusiData.pozice);
         
       // UkladaniProHracMimo hrJednodusiData = strudl.hrDt;
        
        if (hrJednodusiData.zivoty > 0)
        {
            
            GameObject novyHrac = new GameObject();
            foreach (var hr in hrci.prefs)
            {
                if (hrJednodusiData.nazev == hr.hrac.nameHr)
                {
                    novyHrac = Instantiate(hr.hrac.gameObject, hrJednodusiData.pozice, Quaternion.Euler(hrJednodusiData.rotace));
                    Debug.Log(hrJednodusiData.rotace);
                    
                }
            }

            GameObject cam = GameObject.FindGameObjectWithTag("cameraHolder");
            cam.transform.position = new Vector3(hrJednodusiData.pozice.x, cam.transform.position.y, hrJednodusiData.pozice.z);
            hrDt = novyHrac.GetComponent<zakl>();
            hrDt.Zivoty = hrJednodusiData.zivoty;
            hrDt.Atk = hrJednodusiData.atk;
            if (((int)hrJednodusiData.def - hrDt.Def) > 0)
            {

                StartCoroutine(pockej(hrDt, hrJednodusiData));

            }
            else
            {

                hrDt.Def = hrDt.Def + (int)hrJednodusiData.def - hrDt.Def;
                Debug.Log(hrDt.aktDef);
            }
            GameObject hrac = GameObject.FindGameObjectWithTag("Player");
            Destroy(hrac);
           // hracObj.tag = "Player";

        }
        else
        {

            Debug.Log("divný");
            Respawn.Invoke();
        }

    } // pøi naètení

    public void ukazHrace()
    {
        string filePath = Application.dataPath + "/enemies.json";
        string jsonData = File.ReadAllText(filePath);
        zabal = JsonUtility.FromJson<Zabal>(jsonData);
        zakl hrDt;
        UkladaniProHrac hrJednodusiData = zabal.hrDt;

        PowerPointGenerator.instance.mena = zabal.pocetPointu;
        if (hrJednodusiData.zivoty > 0)
        {
           
            GameObject novyHrac = new GameObject();
            foreach (var hr in hrci.prefs)
            {
                if (hrJednodusiData.nazev == hr.hrac.nameHr)
                {
                    novyHrac = Instantiate(hr.hrac.gameObject, hrJednodusiData.pozice, Quaternion.Euler(hrJednodusiData.rotace));

                }
            }

            hrDt = novyHrac.GetComponent<zakl>();
            hrDt.Zivoty = hrJednodusiData.zivoty;
            hrDt.Atk = hrJednodusiData.atk;
            GameObject cam = GameObject.FindGameObjectWithTag("cameraHolder");
            cam.transform.position = new Vector3(hrJednodusiData.pozice.x, cam.transform.position.y, hrJednodusiData.pozice.z);
            if(((int)hrJednodusiData.def - hrDt.Def) > 0)
            {

                StartCoroutine(pockej(hrDt,hrJednodusiData));
                
            }
            else
            {
               
                hrDt.Def = hrDt.Def + (int)hrJednodusiData.def - hrDt.Def;
                Debug.Log(hrDt.aktDef);
            }
            
           
           
        }
        else
        {
            
            Debug.Log("divný");
            Respawn.Invoke();
        }
    } // pøi vrácení ze souboje

    IEnumerator pockej(zakl hrDt,UkladaniProHrac hrJednodusiData)
    {
        yield return new WaitForSeconds(0.1f);
        hrDt.aktDef = hrDt.Def + (int)hrJednodusiData.def - hrDt.Def;
        Debug.Log(hrDt.aktDef);
        Debug.Log("divný");
    }

    #endregion


    #region enemyDoSceny

    private void NactiEnemaky() // pøi naètení 
    {
       
        int enmPom = 0;
        Debug.Log("Uložených emeies: " + strudl.obj.Count);
       
        for (int i = 0; i < strudl.obj.Count ;i++)
        {
            if (strudl.obj[i].zivoty > 0)
            {
                Debug.Log("jsem tu");
                foreach (var enmb in enmci.enemies)
                {
                   // Debug.Log("òaf òaf" + enmb.enemak.nazev);
                    if (enmb.enemak.nazev == strudl.obj[i].nazev)
                    {
                        GameObject enm = Instantiate(enmb.enemak.gameObject, new Vector3(strudl.obj[i].X, strudl.obj[i].Y, strudl.obj[i].Z), Quaternion.Euler(strudl.obj[i].Xrotation, strudl.obj[i].Yrotation, strudl.obj[i].Zrotation));
                        
                        enm.name = "enemy" + enmPom;
                        enemy ll = enm.GetComponent<enemy>();
                        ll.zivoty = strudl.obj[i].zivoty;
                        enmPom++;
                    }
                }     
            }
        }
       
    }

    private void RozdelAPanuj() // pøi vrácení ze souboje
    {
        string filePath = Application.dataPath + "/enemies.json";

        string jsonData = File.ReadAllText(filePath);

        zabal = JsonUtility.FromJson<Zabal>(jsonData);
        vyhra.pocetZivotu = zabal.pocetZivotu;
        pocitadlo.instance.timer = zabal.timer;
        int enmPom = 0;
        foreach (UkladaniProEnemy enemy in zabal.obj)
        {
            if (enemy.zivoty > 0)
            {

                foreach (var enmb in enmci.enemies)
                {
                   
                    if (enmb.enemak.nazev == enemy.nazev)
                    {
                        GameObject enm = Instantiate(enmb.enemak.gameObject, enemy.pozice, Quaternion.Euler(enemy.rotace));
                        enemy ll = enm.GetComponent<enemy>();
                        ll.zivoty = enemy.zivoty;
                        enm.name = "enemy" + enmPom;
                        enmPom++;
                    }
                }
            }

        }

    }

    #endregion


    #region utok
    public void Prepnuti(bool kdo,GameObject enemy)
    {

        
        
        enemy.GetComponent<enemy>().bojuje = true;

        if (kdo)
        {
            Debug.Log("útok enemy");
         
        }
        else
        {
         
            Debug.Log("útok hráè");
            
        }
        zacniBojovat.Invoke(kdo);
    }
    #endregion
}
