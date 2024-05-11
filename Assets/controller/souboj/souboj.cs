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
    // Start is called before the first frame update
    void Start()
    {
        mngr = ingamemanager.Instance;
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
        Debug.Log("jsem tu");
        zabal.hrDt = hracDt;
        zabal.obj = listEnemies;

        string jsonData = JsonUtility.ToJson(zabal);
        File.WriteAllText(Application.dataPath + "/enemies.json", jsonData);
        cont.prvniInstance = false;
        Debug.Log(cont.prvniInstance);
        // zmenit senu
        mngr.PrepniNascenu(sceneJm);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(soubojContainer.enemy+" "+soubojContainer.hrac+" "+soubojContainer.enemyNaTahu);
    }
}
