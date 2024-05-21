using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class soubojSceneController : MonoBehaviour
{
    [SerializeField]
    public Zabal zabal;
    [SerializeField]
    public List<UkladaniProEnemy> listEnemies;
    public UkladaniProHrac hracDt;
    ingamemanager mngr;
    string sceneJm;
    bool neKonecEnemy = false;
    bool enemyVyhral = true;
    // Start is called before the first frame update
    void Start()
    {
        mngr = ingamemanager.instance;
        NactiJson();
       // Debug.Log(BudovaCont.poziceZnicenychBudov.Count() + " to je ten list");
    }
    public void NactiJson()
    {
        string filePath = Application.dataPath + "/enemies.json";

        if (File.Exists(filePath))
        {
           
            string jsonData = File.ReadAllText(filePath);

            
            zabal = JsonUtility.FromJson<Zabal>(jsonData);

           
            listEnemies = zabal.obj;
            hracDt = zabal.hrDt;
            sceneJm = zabal.sceneJm;
            //Debug.Log(listEnemies.Count);
        }
        
    }
    public void Uloz()
    {
        
        zabal.hrDt = hracDt;
        zabal.obj = listEnemies;

       
        Debug.Log(cont.prvniInstance);
        Kontrola();

        // zmenit senu
    }
    void Kontrola()
    {
       
       
        foreach(var enemy in listEnemies)
        {
            if(enemy.zivoty > 0)
                neKonecEnemy = true;
           Debug.Log(neKonecEnemy);
            Debug.Log(enemy.zivoty + "ziv");
            
        }
        if (hracDt.zivoty > 0)
            enemyVyhral = false;
        else
            zabal.pocetZivotu--;


        string jsonData = JsonUtility.ToJson(zabal);
        File.WriteAllText(Application.dataPath + "/enemies.json", jsonData);
        cont.prvniInstance = false;


        if (!neKonecEnemy)
        {
            vyhra.prohra = false;
            
            mngr.PrepniNascenu("konecHry", true);
        }else if (enemyVyhral && zabal.pocetZivotu == -1)
        {
            vyhra.prohra = true;
            //Debug.Log("wtf");
            mngr.PrepniNascenu("konecHry", true);

        }
        else
        {
            mngr.PrepniNascenu(sceneJm, false);

        }




    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(soubojContainer.enemy+" "+soubojContainer.hrac+" "+soubojContainer.enemyNaTahu);
    }
}
