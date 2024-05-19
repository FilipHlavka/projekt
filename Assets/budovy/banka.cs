using JetBrains.Annotations;
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
        if(vytvarecBudov.muzePricist)
        vyhra.pocetZivotu++;
        
        Zivoty.instance.aktText();

    }
    private void OnDestroy()
    {
        if(znicenEnemy)
        vyhra.pocetZivotu--;

        Zivoty.instance.aktText();
    }
}
