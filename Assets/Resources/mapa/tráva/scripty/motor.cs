using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motor : zakl
{
    [SerializeField]
    public GameObject vybuch;
    int pocetVylepseni = 2;
    pohybHrace phb;
    bool CRunning = false;
    public override void Start()
    {
        base.Start();
        phb = gameObject.GetComponent<pohybHrace>();
    }
    public override void Update()
    {
        base.Update();

        CheckForPower();
    }

    private void CheckForPower()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (pocetVylepseni > 0 && !CRunning)
            {
               
                CRunning = true;
                StartCoroutine(schopnost());
            }
        }

    }
    IEnumerator schopnost()
    {
        Debug.Log("aktivace schopnosti");
        bonusRychlost = zaklRychlost;
        phb.agent.speed = Rychlost + bonusRychlost;


        yield return new WaitForSeconds(3f);

        pocetVylepseni--;
        CRunning = false;
        bonusRychlost = 0;
        phb.agent.speed = Rychlost;


    }
}
