using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyn : ZaklSchopnost
{
    public override void Prehraj()
    {
        Hlasy.instance.Plyn();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        base.Start();
        Invoke("Prehraj",1);
    }
}
