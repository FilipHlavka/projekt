using System.Collections;
using System.Collections.Generic;
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
    UnityEvent<bool,GameObject> zacniBojovat;
    GameObject[] enemies;
    public void ZK()
    {
        
        Debug.Log("funguje");
    }
    private void Start()
    {
        kameraSouboje.enabled = false;
        if (stuj == null)
            stuj = new UnityEvent();
        if (zacniBojovat == null)
            zacniBojovat = new UnityEvent<bool,GameObject>();
        if (funguj == null)
            funguj = new UnityEvent();
        stuj.AddListener(kamera.GetComponent<camPohyb>().Zastav);
    }
    public void Prepnuti(bool kdo, GameObject enemy)
    {

        kameraSouboje.enabled = true;
        hrac = GameObject.FindGameObjectWithTag("Player");

        stuj.AddListener(hrac.GetComponent<zakl>().Zastav);
        stuj.AddListener(hrac.GetComponent<pohybHrace>().Zastav);

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enem in enemies)
        {
            stuj.AddListener(enem.GetComponent<pohybEnemy>().Zastav);

        }


        zacniBojovat.AddListener(KontrolerSouboj.GetComponent<SoubojCont>().Zacni);

        if (kdo)
        {
            
            // zaútoèil enemy
            Debug.Log("útok enemy");
            Debug.Log(enemy);
            stuj.Invoke();

           
        }
        else
        {
         

            // zaútoèil hráè
            Debug.Log("útok hráè");
            Debug.Log(enemy);
            stuj.Invoke();

            
        }
        zacniBojovat.Invoke(kdo, enemy);
    }


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
        foreach (GameObject enemy in enemies)
        {
            funguj.AddListener(enemy.GetComponent<pohybEnemy>().Zastav);

        }
        funguj.Invoke();
    }
}
