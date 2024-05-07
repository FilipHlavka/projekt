using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerPointGenerator : MonoBehaviour
{
    [SerializeField]
    public static int maxPP;
   

    public int bonusMaxPP = 0;

    public static int PP;
    [SerializeField]
    int poKolika;
    public int bonusPoKolika;
    public static UnityEvent akttext;
    public static bool stuj = false;
    // Start is called before the first frame update
    void Start()
    {
        maxPP = 500; // llll
        PP = 200;
        InvokeRepeating("PridejPP", 5, 5);
        //PPtext.aktualizuj();
    }
    public void PridejPP()
    {
        if (!stuj) {
            PPtext.aktualizuj();
            if (PP >= maxPP + bonusMaxPP)
            {
                PP = maxPP + bonusMaxPP;

            }
            else
            {
                PP += poKolika + bonusPoKolika;
            }
            PPtext.aktualizuj();
            //Debug.Log(PP);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
