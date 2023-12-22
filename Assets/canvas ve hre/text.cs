using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PPtext : MonoBehaviour
{
    // Start is called before the first frame update
    static TMP_Text napis;
    void Start()
    {
        napis = gameObject.GetComponent<TMP_Text>();
       
    }
    public static void aktualizuj()
    {
        napis.text = "Power Points: "+PowerPointGenerator.PP;
    }
    // Update is called once per frame
    
}
