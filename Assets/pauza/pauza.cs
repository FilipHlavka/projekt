using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pauza : MonoBehaviour
{
    bool pom = true;
    [SerializeField]
    GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pom)
            {
                Pauza();
            }
            else
            {
                Nepauza();
                
            }
        }
      
    }

    private void Pauza()
    {
        Time.timeScale = 0f;
        pohybHrace.nehejbat = true;
        PowerPointGenerator.instance.stuj = true;
        PowerPointGenerator.instance.pom = false;
        EnemyRespawn.instance.spawnuj = false;
        pom = !pom;
        panel.SetActive(true);

    }

    public void Nepauza()
    {
        pohybHrace.nehejbat = false;
        PowerPointGenerator.instance.stuj = false;
        PowerPointGenerator.instance.pom = true;
        EnemyRespawn.instance.spawnuj = true;
        Time.timeScale = 1f;
        pom = !pom;
        panel.SetActive(false);
    }
}
