using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;
using TMPro;

public class prirazeniDoBoje : MonoBehaviour
{
   /* [SerializeField]
    Dictionary<string,Sprite> slovnik = new Dictionary<string,Sprite>();*/

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

    [SerializeField]
    public List<model> modely;

    public UnityEvent zacniEvent;

    public actuallSouboj actSouboj;

    [SerializeField]
    TMP_Text text;
    [SerializeField]
    GameObject canvas;

    public UnityEvent zpet;

    void Start()
    {
        canvas.SetActive(false);
        if (zacniEvent == null)
            zacniEvent = new UnityEvent();  

        actSouboj = gameObject.GetComponent<actuallSouboj>();
        Souboj = gameObject.GetComponent<soubojSceneController>();
        zpet.AddListener(Souboj.Uloz);
        zacniEvent.AddListener(actSouboj.Pridej);

        priratEnemy();
        pridatHrace();
        zacniEvent.Invoke();
    }



    public void konecBoje(bool vyhraEnemy)
    {
       canvas.SetActive(true);
        
        if (vyhraEnemy)
        {
            text.text = "You lost";
        }
        else
        {
            text.text = "You won";
        }
        Souboj.hracDt = actSouboj.hracDt;
        listEnemies.Add(actSouboj.enemyVSouboji);
        Souboj.listEnemies = listEnemies;

        Invoke("ulozZpet",3);
    }
    public void ulozZpet()
    {
        zpet.Invoke();
    }
    #region prirazeniSpritu
    public void priratEnemy()
    {
       
       
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
       
        foreach(model m in modely)
        {
            if(m.nazev == enemyVSouboji.nazev)
            {
               model modelEnemy = Instantiate(m,enemyObjekt.transform);
                actSouboj.pal.AddListener(modelEnemy.Vystrel);
            }
        }
       
        //Debug.Log(listEnemies.Count);
    }

    public void pridatHrace()
    {
        int j = 0;
        hracDt = Souboj.hracDt;
        foreach (model m in modely)
        {
            if (m.nazev == hracDt.nazev)
            {
               model modelHrac = Instantiate(m, hracObjekt.transform);
                actSouboj.pal.AddListener(modelHrac.Vystrel);
            }
        }
        j = 0;
       
        
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
       
    }
}
