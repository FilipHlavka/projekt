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
   
    void Awake()
    {
            instance = this;
    }


    void Start()
    {
        if (fakeScreen != null)
            fakeScreen.enabled = false;

        vyhra.instance.konec.AddListener(stavKonce);
    }

    public void PrepniNascenu(string scena)
    {
        // Zapnout fake loading screen!!!!!!!!!!!!!!!!!!!!! >w<
        if (fakeScreen != null)
            fakeScreen.enabled = true;
        
        ZnicTyZatracenyObjekty();
        SceneManager.LoadScene(scena);

        
    }
    public void stavKonce()
    {
        PrepniNascenu("konecHry");
        
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
