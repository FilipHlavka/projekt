using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dul : budova
{
    // Start is called before the first frame update
    void Start()
    {
        pozice = transform.position;

        jmeno = "dul";
        aktSprite();
    }
    public override void akt()
    {
        powerPointGenerator.bonusPoKolika += 1;

    }
    private void OnDestroy()
    {
        powerPointGenerator.bonusPoKolika -= 1;
    }
}
