using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banka : budova
{
    // Start is called before the first frame update
    void Start()
    {
        
        pozice = transform.position;
        jmeno = "banka";
        aktSprite();
    }
    
    public override void akt()
    {
        powerPointGenerator.bonusMaxPP += 10;

    }
    private void OnDestroy()
    {
        powerPointGenerator.bonusMaxPP -= 10;
    }
}
