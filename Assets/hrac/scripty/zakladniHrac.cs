using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakladniHrac : zakl
{
    // dodìlat hráèe do scény 2
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
                /*Vector3 mousePosition = Input.mousePosition;
                Vector3 DoSveta = Camera.main.ScreenToWorldPoint(mousePosition);
                
                Instantiate(vybuch, new Vector3(DoSveta.x,DoSveta.y,0), Quaternion.Euler(0,0,0));*/
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
