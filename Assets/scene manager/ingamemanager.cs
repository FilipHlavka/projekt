using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ingamemanager : MonoBehaviour
{
    soubojContainer controller;
    void Start()
    {
        
    }

    public void PrepniNascenu(string scena)
    {
        //Debug.Log(soubojContainer.enemy+" "+soubojContainer.hrac+" "+soubojContainer.enemyNaTahu);

        SceneManager.LoadScene(scena);
        
    }
    void Update()
    {
        
    }
}
