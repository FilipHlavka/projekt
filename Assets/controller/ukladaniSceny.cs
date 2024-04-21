using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ukladaniSceny : MonoBehaviour
{
    // Start is called before the first frame update
    public List<UkladaniProEnemyMimo> jablkoEnemy = new List<UkladaniProEnemyMimo>();
    public List<UkladaniProBudovuMimo> jablkoBudova = new List<UkladaniProBudovuMimo>();
    public zakl hracData;
    Zavin strudl = new Zavin();
    public List<enemy> enemyData;
    UkladaniProHracMimo ukl = new UkladaniProHracMimo();
    void Start()
    {
       
    }

    public void Uloz()
    {
        
       
        BudovaDo();
        Prirazovani();
        strudl.sceneJm = SceneManager.GetActiveScene().name;
        strudl.obj = jablkoEnemy;
        strudl.bdv = jablkoBudova;
        strudl.hrDt = ukl;

        BinaryFormatter formator = new BinaryFormatter();

        FileStream stream = File.Create(Application.dataPath + "/" + strudl.sceneJm + ".bin");
        formator.Serialize(stream,strudl);
        stream.Close();

    }

    void Prirazovani()
    {
        GameObject hrac = GameObject.FindGameObjectWithTag("Player");
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag("enemy").ToList();
        enemies.Add(hrac);


      
        enemyData = new List<enemy>(enemies.Count);

        foreach (GameObject enemyObject in enemies)
        {
            if (enemyObject.TryGetComponent<enemy>(out var enemyComponent))
            {
                enemyData.Add(enemyComponent);
               
            }
            else if (enemyObject.TryGetComponent<zakl>(out var hracComponent))
            {
                hracData = hracComponent;
            }
        }

        if (hracData != null)
        {

            ukl.zivoty = hracData.Zivoty;
            
            ukl.atk = hracData.Atk;
            ukl.def = hracData.aktDef;
            ukl.nazev = hracData.nameHr;
          
            ukl.Y = hracData.pozice.y;
            ukl.X = hracData.pozice.x;
        }


        foreach (enemy enemy in enemyData)
        {
            if (enemy != null)
            {
                UkladaniProEnemyMimo nvm = new UkladaniProEnemyMimo();
                nvm.nazev = enemy.nazev;
                nvm.zivoty = enemy.zivoty;
                nvm.atk = enemy.atk;
                nvm.def = enemy.aktDef;
             
                nvm.X = enemy.pozice.x;
                nvm.Y = enemy.pozice.y;

                jablkoEnemy.Add(nvm);
            }
        }
    }

    void BudovaDo()
    {
        List<GameObject> budovy = new List<GameObject>();
        budovy = GameObject.FindGameObjectsWithTag("budova").ToList();



        foreach (GameObject budov in budovy)
        {
            if (budov.TryGetComponent<budova>(out var budovaObjekt))
            {
                UkladaniProBudovuMimo uklProBdv = new UkladaniProBudovuMimo();
                uklProBdv.nazev = budovaObjekt.jmeno;
                uklProBdv.Y = budovaObjekt.pozice.y;
                uklProBdv.X = budovaObjekt.pozice.x;
                jablkoBudova.Add(uklProBdv);
            }

        }

    }
    
}


[Serializable]
public struct UkladaniProEnemyMimo
{
    public int zivoty;
    public int atk;
    public float def;
    public string nazev;
    public float X;
    public float Y;
}
[Serializable]
public class UkladaniProHracMimo
{
    public int zivoty;
    public int atk;
    public float def;
    public string nazev;
    public float X;
    public float Y;

}
[Serializable]
public struct UkladaniProBudovuMimo
{
    public string nazev;
    public float X;
    public float Y;
}
[Serializable]
public struct Zavin
{
    public List<UkladaniProBudovuMimo> bdv;
    public List<UkladaniProEnemyMimo> obj;
    public UkladaniProHracMimo hrDt;
    public string sceneJm;
}
