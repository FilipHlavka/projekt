using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class obtiznost : MonoBehaviour
{
    private static obtiznost instance;
    [SerializeField]
    public bool tezka = false;
    public static obtiznost Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new obtiznost();
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void DejmiObtiznost(int n)
    {
        if(n == 0)
            tezka = false;
        else
            tezka = true;

        Debug.Log(tezka);
    }
   
}
