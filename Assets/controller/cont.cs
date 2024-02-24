using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.U2D;

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
    public UnityEvent aktBudovy;
    //enemyTypy typy;

    private void Start()
    {
        res = gameObject.GetComponent<respawnScript>();
        Respawn.AddListener(res.DelejNeco);

        if (!prvniInstance)
        {
            prvniInstance = true;
            
            aktBudovy.Invoke();
            RozdelAPanuj();
            ukazHrace();
        }
        
        if (zacniBojovat == null)
            zacniBojovat = new UnityEvent<bool>();

       

        //typy = gameObject.GetComponent<enemyTypy>();
      
        //slovnik = typy.slovnik;
    }

    #region hracDoSceny



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
            hrDt.Def = (int)hrJednodusiData.def;
            hracObj.tag = "Player";

        }
        else
        {
            
            Debug.Log("divný");
            Respawn.Invoke();
        }
    }



    #endregion


    #region enemyDoSceny

    private void RozdelAPanuj()
    {
        for (int i = 0; i < enemaci.Count; i++)
        {
            slovnik.Add(enemyNames[i], enemaci[i]);
        }


        string filePath = Application.dataPath + "/enemies.json";

        string jsonData = File.ReadAllText(filePath);

        zabal = JsonUtility.FromJson<Zabal>(jsonData);

        foreach (UkladaniProEnemy enemy in zabal.obj)
        {
            if (enemy.zivoty > 0)
            {
                slovnik.TryGetValue(enemy.nazev, out GameObject enemyObj);
                Instantiate(enemyObj, enemy.pozice, Quaternion.Euler(0, 0, 0));

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
