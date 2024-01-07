using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerPointGenerator : MonoBehaviour
{
    [SerializeField]
    public static int maxPP;
    public static int PP;
    [SerializeField]
    int poKolika;
    public static UnityEvent akttext;
    // Start is called before the first frame update
    void Start()
    {
        maxPP = 100; // llll
        InvokeRepeating("PridejPP", 5, 5);
    }
    public void PridejPP()
    {
        if(PP >= maxPP)
        {
            PP = maxPP;
            CancelInvoke("PridejPP");
        }
        else
        {
            PP += poKolika;
        }
        PPtext.aktualizuj();
        //Debug.Log(PP);

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
