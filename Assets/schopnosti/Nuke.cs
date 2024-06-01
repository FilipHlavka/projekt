using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
       
        pauza.funguj = false;
    }
    private void OnDestroy()
    {
        vyhra.prohra = false;
        cont.prvniInstance = true;

        ingamemanager.instance.PrepniNascenu("konecHry", true);
    }
}
