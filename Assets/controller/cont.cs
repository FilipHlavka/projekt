using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class cont : MonoBehaviour
{
    GameObject hrac;

    UnityEvent stuj;
    UnityEvent funguj;
    [SerializeField]
    GameObject kamera;
    [SerializeField]                    // zkusit pøes static
    Camera kameraSouboje;

    [SerializeField]
    GameObject KontrolerSouboj;
    public UnityEvent<bool> zacniBojovat;
    List<GameObject> enemies;
   
    private void Start()
    {
        
        if (zacniBojovat == null)
            zacniBojovat = new UnityEvent<bool>();
      

    }
    public void Prepnuti(bool kdo,GameObject enemy)
    {

        
       // hrac = GameObject.FindGameObjectWithTag("Player");

        
        
        enemy.GetComponent<enemy>().bojuje = true;

        //zacniBojovat.AddListener(KontrolerSouboj.GetComponent<SoubojCont>().Zacni);

        if (kdo)
        {
            
            // zaútoèil enemy
            Debug.Log("útok enemy");
            //Debug.Log(enemy);
           // stuj.Invoke();

           
        }
        else
        {
         

            // zaútoèil hráè
            Debug.Log("útok hráè");
            //Debug.Log(enemy);
           // stuj.Invoke();

            
        }
        zacniBojovat.Invoke(kdo);
    }

    /*
    public void PrepniZpet()
    {
        kameraSouboje.enabled = false;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            hrac = GameObject.FindGameObjectWithTag("Player");

           

            funguj.AddListener(hrac.GetComponent<zakl>().Zastav);
            funguj.AddListener(hrac.GetComponent<pohybHrace>().Zastav);
        }
        funguj.AddListener(kamera.GetComponent<camPohyb>().Zastav);

        enemies = GameObject.FindGameObjectsWithTag("enemy");
       /* foreach (GameObject enemy in enemies)
        {
            funguj.AddListener(enemy.GetComponent<pohybEnemy>().Zastav);

        }
        //funguj.Invoke();
    }*/

}
