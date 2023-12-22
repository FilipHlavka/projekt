using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;


public class prirazeniDoBoje : MonoBehaviour
{
    [SerializeField]
    Dictionary<string,Sprite> slovnik = new Dictionary<string,Sprite>();

    [SerializeField]
    List<Sprite> spriteList, hracspriteList;
    [SerializeField]
    List<string> nazevList , hracNazev;
    soubojSceneController Souboj;
    public List<UkladaniProEnemy> listEnemies;
    public UkladaniProEnemy enemyVSouboji;
    public UkladaniProHrac hracDt;

    [SerializeField]
    GameObject enemyObjekt;
    [SerializeField]
    GameObject hracObjekt;

    public UnityEvent zacniEvent;

    public actuallSouboj actSouboj;

    void Start()
    {
      
        if(zacniEvent == null)
            zacniEvent = new UnityEvent();  

        actSouboj = gameObject.GetComponent<actuallSouboj>();
        Souboj = gameObject.GetComponent<soubojSceneController>();

        zacniEvent.AddListener(actSouboj.Pridej);

        priratEnemy();
        pridatHrace();
        zacniEvent.Invoke();
    }
    public void priratEnemy()
    {
        for (int i = 0; i < spriteList.Count; i++)
        {
            slovnik.Add(nazevList[i], spriteList[i]);
        }
       
        foreach (UkladaniProEnemy enemy in Souboj.listEnemies)
        {
            listEnemies.Add(enemy);
        }
        for (int i = 0; i < listEnemies.Count; i++)
        {
            if (listEnemies[i].bojuje)
            {
                enemyVSouboji = listEnemies[i];
                listEnemies.Remove(listEnemies[i]);
            }
        }
        Sprite sprite = null;
        slovnik.TryGetValue(enemyVSouboji.nazev, out sprite);
        enemyObjekt.GetComponent<SpriteRenderer>().sprite = sprite;
        Debug.Log(listEnemies.Count);
    }

    public void pridatHrace()
    {
        int j = 0;
        hracDt = Souboj.hracDt;
        for (int i = spriteList.Count; i < spriteList.Count + hracspriteList.Count; i++)
        {
            slovnik.Add(hracNazev[j], hracspriteList[j]);
            j++;
        }
        j = 0;
        Sprite sprite = null;
        slovnik.TryGetValue(hracDt.nazev, out sprite);
        hracObjekt.GetComponent<SpriteRenderer>().sprite = sprite;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
