using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Nuke : ZaklSchopnost
{
    private void Start()
    {
        base.Start();
        List<GameObject> list = GameObject.FindGameObjectsWithTag("enemy").ToList();
        foreach (GameObject go in list)
        {
            Destroy(go);
            EnemyRespawn.instance.spawnuj = false;
        }
        Invoke("Prehraj", 1);
        pauza.funguj = false;
    }
    private void OnDestroy()
    {
        vyhra.prohra = false;
        cont.prvniInstance = true;

        ingamemanager.instance.PrepniNascenu("konecHry", true, 1);
    }

    public override void Prehraj()
    {
       
            Hlasy.instance.Exploze();

        
    }
}
