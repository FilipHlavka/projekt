using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vrt : budova
{
    // Start is called before the first frame update
    void Start()
    {

        pozice = transform.position;
        jmeno = "vrt";
        aktSprite();
    }
    public override void akt()
    {
        powerPointGenerator.bonusPoKolika += 2;
       
    }
    private void OnDestroy()
    {
        powerPointGenerator.bonusPoKolika -= 2;
    }

}
