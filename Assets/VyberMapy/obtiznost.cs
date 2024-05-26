using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class obtiznost : MonoBehaviour
{
    public static obtiznost instance;
    [SerializeField]
    public bool tezka = false;
    
    void Awake()
    {
       instance = this;
    }

    public void DejmiObtiznost(int n)
    {
        if(n == 0)
            tezka = false;
        else
            tezka = true;
    }
   
}
