using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Container : MonoBehaviour
{
    soubojContainer container;
    public GameObject hrac;

    public List<GameObject> enemies;
    public List<enemy> enemyData;
    public zakl hracData;
    [SerializeField]
    public List<UkladaniProEnemy> UzFaktNevimEnemy = new List<UkladaniProEnemy>();
    public List<UkladaniProBudovu> UzFaktNevimBudova = new List<UkladaniProBudovu>();
    public static bool enemyNaTahu;
    int i = 0;
    UkladaniProHrac ukl = new UkladaniProHrac();
    [SerializeField]
    public string jsonData;
    void Start()
    {
        container = gameObject.GetComponent<soubojContainer>();
        container.uloz.AddListener(Uloz);
        i = 0;
    }

    private void Uloz(bool enemyNaRade)
    {
       
       
        
        enemyNaTahu = enemyNaRade;
        hrac = GameObject.FindGameObjectWithTag("Player");
        if(i == 0)
        DoJSNu();
    }

    private void DoJSNu()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy").ToList();
        enemies.Add(hrac);


        Scene currentScene = SceneManager.GetActiveScene();

        
        Zabal Whopper = new Zabal();
        Whopper.obj = UzFaktNevimEnemy;
        Whopper.hrDt = ukl;
        Whopper.sceneJm = currentScene.name;
        Whopper.bdv = UzFaktNevimBudova;

        enemyData = new List<enemy>(enemies.Count);

        foreach (GameObject enemyObject in enemies)
        {
            if (enemyObject.TryGetComponent<enemy>(out var enemyComponent))
            {
                enemyData.Add(enemyComponent);
            }
            else if(enemyObject.TryGetComponent<zakl>(out var hracComponent))
            {
                hracData = hracComponent;
            }
        }

        if(hracData != null)
        {
            
            ukl.zivoty = hracData.Zivoty;
            Debug.Log(ukl.zivoty);
            Debug.Log(hracData.Zivoty);
            ukl.atk = hracData.Atk;
            ukl.def = hracData.aktDef;
            ukl.nazev = hracData.nameHr;
            ukl.pozice = hracData.pozice;
        }
        


       
        
        i++;
        foreach (enemy enemy in enemyData)
        {
            if (enemy != null)
            {
                UkladaniProEnemy nvm = new UkladaniProEnemy();
                nvm.nazev = enemy.nazev;
                nvm.zivoty = enemy.zivoty;
                nvm.atk = enemy.atk;
                nvm.def = enemy.aktDef;
                nvm.bojuje = enemy.bojuje;
                nvm.pozice = enemy.pozice;
                nvm.jeNaTahu = enemyNaTahu;

                UzFaktNevimEnemy.Add(nvm);
            }
        }

        BudovaDoJSNu();

       
        jsonData = JsonUtility.ToJson(Whopper);
       /* Debug.Log(UzFaktNevimEnemy.Count);
        Debug.Log(jsonData);
        Debug.Log(UzFaktNevimEnemy);*/
        File.WriteAllText(Application.dataPath + "/enemies.json", jsonData);
       
        
       
    }

    void BudovaDoJSNu()
    {
        List<GameObject> budovy = new List<GameObject>();
        budovy = GameObject.FindGameObjectsWithTag("budova").ToList();

       

        foreach (GameObject budov in budovy )
        {
            if (budov.TryGetComponent<budova>(out var budovaObjekt))
            {
                UkladaniProBudovu uklProBdv = new UkladaniProBudovu();
                uklProBdv.nazev = budovaObjekt.jmeno;
                uklProBdv.pozice = budovaObjekt.pozice;
                uklProBdv.zivoty = budovaObjekt.zivoty;
                UzFaktNevimBudova.Add(uklProBdv);
            }

        }

    }


 
}
#region tridyProJSON
[Serializable]
public struct UkladaniProEnemy
{
    public int zivoty;
    public int atk;
    public float def;
    public string nazev;
    public bool bojuje;
    public Vector3 pozice;
   
    public bool jeNaTahu;
}
[Serializable]
public class UkladaniProHrac
{
    public int zivoty;
    public int atk;
    public float def;
    public string nazev;
    public Vector3 pozice;


}
[Serializable]
public struct UkladaniProBudovu
{
    public string nazev;
    public Vector3 pozice;
    public int zivoty;

}
[Serializable]
public struct Zabal
{
    public List<UkladaniProBudovu> bdv;
    public List<UkladaniProEnemy> obj;
    public UkladaniProHrac hrDt;
    public string sceneJm;
}
#endregion