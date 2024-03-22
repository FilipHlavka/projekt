using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ingamemanager : MonoBehaviour
{
    soubojContainer controller;
    public static int l = 0;
    List<GameObject> entity = new List<GameObject>();
    [SerializeField]
    Image fakeScreen;
    void Start()
    {
        if (fakeScreen != null)
            fakeScreen.enabled = false;   
    }

    public void PrepniNascenu(string scena)
    {
        // Zapnout fake loading screen!!!!!!!!!!!!!!!!!!!!! >w<
        if (fakeScreen != null)
            fakeScreen.enabled = true;
        ZnicTyZatracenyObjekty();
        SceneManager.LoadScene(scena);

        
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
