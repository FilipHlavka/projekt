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
    

    // Start is called before the first frame update
    void Start()
    {
        NactiJson();
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
           
            Debug.Log(listEnemies.Count);
        }
        
    }
    public void Uloz()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(soubojContainer.enemy+" "+soubojContainer.hrac+" "+soubojContainer.enemyNaTahu);
    }
}
