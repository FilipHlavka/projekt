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
        LineRenderer line = GameObject.FindGameObjectWithTag("line").GetComponent<LineRenderer>();
        line.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        pauza.funguj = false;
    }
    private void OnDestroy()
    {
        vyhra.prohra = false;
        cont.prvniInstance = true;

        ingamemanager.instance.PrepniNascenu("konecHry", true);
    }
}
