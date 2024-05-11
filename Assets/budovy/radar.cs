using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radar : budova
{
    // Start is called before the first frame update
    
    void Start()
    {
        base.Start();
        pozice = transform.position;
        jmeno = "radar";

        
        

    }
    public override void akt()
    {
        // Instantiate(Resources.Load("budovy/kruh2"), gameObject.transform);

    }
}
