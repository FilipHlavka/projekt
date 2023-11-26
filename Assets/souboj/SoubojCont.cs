using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

public class SoubojCont : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer hracSprite; // fake hr·Ë
    [SerializeField]
    SpriteRenderer enemySprite;
    hracData hrac; // opravdu hr·Ë
    data Enemy;
    UnityEvent konec;
    bool bojujme = false;
    bool enemyNaTahu;
    GameObject cont;
    // Start is called before the first frame update
    void Awake()
    {
        if (konec == null)
            konec = new UnityEvent();
        cont = GameObject.FindGameObjectWithTag("GameController");
       
        konec.AddListener(cont.GetComponent<cont>().PrepniZpet);
    }

    // Update is called once per frame
    void Update()
    {
        if (bojujme)
            Check();

        if (bojujme)
        bojuj();

       
    }
    public void Check()
    {
        if (Enemy.zivoty <= 0 || hrac.zivoty <= 0)
        {
            bojujme = false;
            
            
            konec.Invoke();
        }
    }

    private void bojuj()
    {
        if (enemyNaTahu)
            TahEnemy();
        else
            HracNaTahu();
    }

    private void HracNaTahu()
    {
       enemyNaTahu = hrac.Bojuj(Enemy);
        
    }
    public void TahEnemy()
    {
        Enemy.Bojuj(hrac);
        enemyNaTahu = false;
        

    }

    public void Zacni(bool kdo, GameObject enemy)
    {
      
        // kdo = true -- ˙toËÌ enemy;
       
        Enemy = enemy.GetComponent<data>();
        enemySprite.sprite = enemy.GetComponent<SpriteRenderer>().sprite;

        
        hrac = GameObject.FindGameObjectWithTag("Player").GetComponent<hracData>();
       
        hracSprite.sprite = hrac.GetComponent<SpriteRenderer>().sprite;

        enemyNaTahu = kdo;
        bojujme = true;


        
    }
    
}
