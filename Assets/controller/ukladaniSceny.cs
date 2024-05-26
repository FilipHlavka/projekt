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
    public int pocetUlozeni = 0;
    void Start()
    {
       
    }

    public void Uloz()
    {
        pocetUlozeni++;
       
        BudovaDo();
        Prirazovani();
        strudl.sceneJm = SceneManager.GetActiveScene().name;
        strudl.obj = jablkoEnemy;
        strudl.bdv = jablkoBudova;
        strudl.hrDt = ukl;
        strudl.powePoints = PowerPointGenerator.instance.mena;
        strudl.pocetUlozeni = pocetUlozeni;
        strudl.pocetZivotu = vyhra.pocetZivotu;
        strudl.timer = pocitadlo.instance.timer;
        strudl.tezky = obtiznost.instance.tezka;
        BinaryFormatter formator = new BinaryFormatter();
        try
        {
            File.Delete(Application.dataPath + "/" + strudl.sceneJm + ".bin");
        }
        catch
        {
            Debug.Log("sakra");
        }
        FileStream stream = File.Create(Application.dataPath + "/" + strudl.sceneJm + ".bin");
        formator.Serialize(stream,strudl);
        stream.Close();

    }

    void Prirazovani()
    {
        GameObject hrac = GameObject.FindGameObjectWithTag("Player");
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag("enemy").ToList();
        Debug.Log("Ukládám: " + string.Join(", ", enemies.Select(e => e.name)));
        Debug.Log(enemies.Count + "pocet ulozenych enemaku");
        enemies.Add(hrac);


        enemyData.Clear();
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
          
            ukl.Y = hracData.transform.position.y;
            ukl.X = hracData.transform.position.x;
            ukl.Z = hracData.transform.position.z;

            ukl.Xrotation = hracData.transform.rotation.eulerAngles.x;
            ukl.Yrotation = hracData.transform.rotation.eulerAngles.y;
            ukl.Zrotation = hracData.transform.rotation.eulerAngles.z;
            
        }

        jablkoEnemy.Clear();
        foreach (enemy enemy in enemyData)
        {
            if (enemy != null)
            {
                UkladaniProEnemyMimo nvm = new UkladaniProEnemyMimo();
                nvm.nazev = enemy.nazev;
                nvm.zivoty = enemy.zivoty;
                nvm.atk = enemy.atk;
                nvm.def = enemy.aktDef;
             
                nvm.X = enemy.transform.position.x;
                nvm.Y = enemy.transform.position.y;
                nvm.Z = enemy.transform.position.z;
                //Debug.Log(enemy.name);
                nvm.Xrotation = enemy.transform.rotation.eulerAngles.x;
                nvm.Yrotation = enemy.transform.rotation.eulerAngles.y;
                nvm.Zrotation = enemy.transform.rotation.eulerAngles.z;

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
                uklProBdv.zivoty = budovaObjekt.zivoty;
                uklProBdv.Y = budovaObjekt.pozice.y;
                uklProBdv.X = budovaObjekt.pozice.x;
                uklProBdv.Z = budovaObjekt.pozice.z;
                uklProBdv.Xrotation = budovaObjekt.transform.rotation.eulerAngles.x;
                uklProBdv.Yrotation = budovaObjekt.transform.rotation.eulerAngles.y;
                uklProBdv.Zrotation = budovaObjekt.transform.rotation.eulerAngles.z;
                jablkoBudova.Add(uklProBdv);
                
            }

        }

    }
    
}
#region dataHoldery

[Serializable]
public struct UkladaniProEnemyMimo
{
    public int zivoty;
    public int atk;
    public float def;
    public string nazev;
    public float X;
    public float Y;
    public float Z;
    public float Xrotation;
    public float Yrotation;
    public float Zrotation;
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
    public float Z;
    public float Xrotation;
    public float Yrotation;
    public float Zrotation;
}
[Serializable]
public class UkladaniProBudovuMimo
{
    public string nazev;
    public float zivoty;
    public float X;
    public float Y;
    public float Z;
    public float Xrotation;
    public float Yrotation;
    public float Zrotation;
}
[Serializable]
public struct Zavin
{
    public List<UkladaniProBudovuMimo> bdv;
    public List<UkladaniProEnemyMimo> obj;
    public UkladaniProHracMimo hrDt;
    public string sceneJm;
    public int powePoints;
    public int pocetUlozeni;
    public int pocetZivotu;
    public float timer;
    public bool tezky;
}
#endregion