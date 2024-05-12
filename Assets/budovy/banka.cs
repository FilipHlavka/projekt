using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banka : budova
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        pozice = transform.position;
        jmeno = "nemocnice";
        //aktSprite();
    }
    
    public override void akt()
    {
        PowerPointGenerator.instance.bonusMax += 10;

    }
    private void OnDestroy()
    {
        PowerPointGenerator.instance.bonusMax -= 10;
    }
}
