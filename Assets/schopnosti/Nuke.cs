using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Nuke : ZaklSchopnost
{
    List<GameObject> list;
    private void Start()
    {
        base.Start();
        list = GameObject.FindGameObjectsWithTag("enemy").ToList();
       
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
        foreach (GameObject go in list)
        {
            Destroy(go);
            EnemyRespawn.instance.spawnuj = false;
        }
        Hlasy.instance.Exploze();
        if(stit.instance != null)
        stit.instance.NasadStit();
    }
}
