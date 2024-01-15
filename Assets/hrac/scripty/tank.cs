using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank : zakl
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
        phb.agent.speed = Rychlost * 2;


        yield return new WaitForSeconds(3f);

        pocetVylepseni--;
        CRunning = false;
        phb.agent.speed = Rychlost;


    }
}
