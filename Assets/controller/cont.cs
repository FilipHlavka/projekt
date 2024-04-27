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

public class cont : MonoBehaviour
{
    [SerializeField]
    camPohyb kamera;
    /*[SerializeField]                    // zkusit pøes static
    Camera kameraSouboje;*/
    public static bool prvniInstance = true;
    [SerializeField]
    GameObject KontrolerSouboj;
    public UnityEvent<bool> zacniBojovat;
    [SerializeField]
    public Zabal zabal = new Zabal();
    //List<GameObject> enemiesToApear;
    respawnScript res;

    #region pridelovani
    [SerializeField]
    public List<GameObject> enemaci, hraci;
    [SerializeField]
    public List<string> enemyNames, playerNames;

    [SerializeField]
    public Dictionary<string, GameObject> slovnik = new Dictionary<string, GameObject>();
    #endregion

    public UnityEvent Respawn;
    public UnityEvent<bool,Zavin> aktBudovy;
    List<GameObject> listEnemaku;
    public nacteniSceny nacitani;

    Zavin strudl;
    //enemyTypy typy;

    private void Start()
    {
        res = gameObject.GetComponent<respawnScript>();
        Respawn.AddListener(res.DelejNeco);

        nacitani = gameObject.GetComponent<nacteniSceny>();
        nacitani.eventNacteni.AddListener(AktivujNacteni);

        if (!prvniInstance)
        {
            prvniInstance = true;
            
            aktBudovy.Invoke(false,new Zavin());
            RozdelAPanuj();
            ukazHrace();
        }
        
        if (zacniBojovat == null)
            zacniBojovat = new UnityEvent<bool>();

       

        //typy = gameObject.GetComponent<enemyTypy>();
      
        //slovnik = typy.slovnik;
    }

    private void AktivujNacteni(Zavin strudl)
    {
        slovnik.Clear();
        vyhra.stuj = true;
        
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
        PowerPointGenerator.PP = strudl.powePoints;
        NactiEnemaky();
        ukazHraceDoSceny(strudl);
        aktBudovy.Invoke(true,strudl);
        vyhra.stuj = false;
        
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
        hrJednodusiData.pozice = new Vector2(strudl.hrDt.X,strudl.hrDt.Y);
        Debug.Log(hrJednodusiData.pozice);
        int j = 0;

        for (int i = enemaci.Count; i < enemaci.Count + hraci.Count; i++)
        {
            slovnik.Add(playerNames[j], hraci[j]);
            j++;
        }
        j = 0;

        if (hrJednodusiData.zivoty > 0)
        {
            kamera.movex = hrJednodusiData.pozice.x;
            kamera.movey = hrJednodusiData.pozice.y;
            slovnik.TryGetValue(hrJednodusiData.nazev, out GameObject hracObj);

            GameObject novyHrac = Instantiate(hracObj, hrJednodusiData.pozice, Quaternion.Euler(0, 0, 0));

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
            hracObj.tag = "Player";

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

        int j = 0;
       
        for (int i = enemaci.Count; i < enemaci.Count + hraci.Count; i++)
        {
            slovnik.Add(playerNames[j], hraci[j]);
            j++;
        }
        j = 0;

        if (hrJednodusiData.zivoty > 0)
        {
            kamera.movex = hrJednodusiData.pozice.x;
            kamera.movey = hrJednodusiData.pozice.y;
            slovnik.TryGetValue(hrJednodusiData.nazev, out GameObject hracObj);
            
            GameObject novyHrac =  Instantiate(hracObj, hrJednodusiData.pozice, Quaternion.Euler(0, 0, 0));

            hrDt = novyHrac.GetComponent<zakl>();
            hrDt.Zivoty = hrJednodusiData.zivoty;
            hrDt.Atk = hrJednodusiData.atk;
            if(((int)hrJednodusiData.def - hrDt.Def) > 0)
            {

                StartCoroutine(pockej(hrDt,hrJednodusiData));
                
            }
            else
            {
               
                hrDt.Def = hrDt.Def + (int)hrJednodusiData.def - hrDt.Def;
                Debug.Log(hrDt.aktDef);
            }
            
            hracObj.tag = "Player";

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
        
        for (int i = 0; i < enemaci.Count; i++)
        {
            slovnik.Add(enemyNames[i], enemaci[i]);
        }
        int enmPom = 0;
        Debug.Log("Uložených emeies: " + strudl.obj.Count);
       
        for (int i = 0; i < strudl.obj.Count ;i++)
        {
            if (strudl.obj[i].zivoty > 0)
            {
               
                
                
                    slovnik.TryGetValue(strudl.obj[i].nazev, out GameObject enemyObj);
                    GameObject enm = Instantiate(enemyObj, new Vector2(strudl.obj[i].X, strudl.obj[i].Y), Quaternion.Euler(0, 0, 0));

                    enm.name = "enemy" + enmPom;
                    enemy ll = enm.GetComponent<enemy>();
                    ll.zivoty = strudl.obj[i].zivoty;
                    enmPom++;
                   
              
               
            }
        }
       
    }

    private void RozdelAPanuj() // pøi vrácení ze souboje
    {
        for (int i = 0; i < enemaci.Count; i++)
        {
            slovnik.Add(enemyNames[i], enemaci[i]);
        }


        string filePath = Application.dataPath + "/enemies.json";

        string jsonData = File.ReadAllText(filePath);

        zabal = JsonUtility.FromJson<Zabal>(jsonData);

        int enmPom = 0;
        foreach (UkladaniProEnemy enemy in zabal.obj)
        {
            if (enemy.zivoty > 0)
            {
                slovnik.TryGetValue(enemy.nazev, out GameObject enemyObj);
                GameObject enm = Instantiate(enemyObj, enemy.pozice, Quaternion.Euler(0, 0, 0));
                enemy ll = enm.GetComponent<enemy>();
                ll.zivoty = enemy.zivoty;
                enm.name = "enemy" + enmPom;
                enmPom++;
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
