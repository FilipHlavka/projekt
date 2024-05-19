using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class konec : MonoBehaviour
{
    [SerializeField]
    Canvas prohraCan;
    [SerializeField]
    Canvas vyhraCan;
    
    // Start is called before the first frame update
    void Start()
    {
        prohraCan.enabled = false;
        vyhraCan.enabled = false;
        if (vyhra.prohra)
            prohraCan.enabled = true;
        else
            vyhraCan.enabled = true;

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
