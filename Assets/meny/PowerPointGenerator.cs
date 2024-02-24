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
    // Start is called before the first frame update
    void Start()
    {
        maxPP = 30; // llll
        InvokeRepeating("PridejPP", 5, 5);
    }
    public void PridejPP()
    {
        if(PP >= maxPP + bonusMaxPP)
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
