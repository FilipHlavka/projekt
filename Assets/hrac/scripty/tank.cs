using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank : zakl
{
    [SerializeField]
    public GameObject vybuch;
    int pocetVylepseni = 2;
    pohybHrace phb;
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
            if (pocetVylepseni > 0)
            {
                /*Vector3 mousePosition = Input.mousePosition;
                Vector3 DoSveta = Camera.main.ScreenToWorldPoint(mousePosition);

                Instantiate(vybuch, new Vector3(DoSveta.x,DoSveta.y,0), Quaternion.Euler(0,0,0));*/
                StartCoroutine(schopnost());
            }
        }

    }
    IEnumerator schopnost()
    {
        Debug.Log("aktivace schopnosti" );
        phb.agent.speed = Rychlost * 2;


        yield return new WaitForSeconds(3f);

        pocetVylepseni--;

        phb.agent.speed = Rychlost;


    }
}
