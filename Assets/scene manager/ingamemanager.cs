using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ingamemanager : MonoBehaviour
{
    //soubojContainer controller;
    public static int l = 0;
    List<GameObject> entity = new List<GameObject>();
    [SerializeField]
    Image fakeScreen;
    public static ingamemanager instance;
    public static string dalsiScena;
    void Awake()
    {
            instance = this;
    }


    void Start()
    {
        if (fakeScreen != null)
            fakeScreen.enabled = false;

        if (vyhra.instance != null)
        vyhra.instance.konec.AddListener(stavKonce);
    }

    public void PrepniNascenu(string scena, bool loading)
    {
        // Zapnout fake loading screen!!!!!!!!!!!!!!!!!!!!! >w<
        if (fakeScreen != null)
            fakeScreen.enabled = true;
        
        dalsiScena = scena;
        ZnicTyZatracenyObjekty();
        if(!loading)
            SceneManager.LoadScene(scena);
        else
        {
            // Debug.Log("wtf");
            vyhra.pocetZivotu = 2;
            SceneManager.LoadScene("fakeLoadingScreen");

        }

    }
    public void PrepniNascenuJednodusi(string scena)
    {
            SceneManager.LoadScene(scena);
    }
    public void stavKonce()
    {
        PrepniNascenu("konecHry",true);
        
    }

    public void konec()
    {
        UnityEngine.Application.Quit();

    }
    private void ZnicTyZatracenyObjekty()
    {

        entity = GameObject.FindGameObjectsWithTag("enemy").ToList();
        entity.Add(GameObject.FindGameObjectWithTag("Player"));

        foreach (GameObject mrchaneznicitelna in entity)
        {
            Destroy(mrchaneznicitelna);
        }
    }
    void Update()
    {
        
    }
}
