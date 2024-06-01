using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ukazZarovku : MonoBehaviour
{
    public static ukazZarovku instance;

    public bool sviti;
    [SerializeField]
    Image zarovka;
    [SerializeField]
    public float timer;
    public float stopCas = 0.5f;

    private void Awake()
    {
        instance = this;
        zarovka.enabled = false;
    }

    private void Update()
    {
        if (timer > stopCas)
        {
            timer -= Time.deltaTime;
            
        }
        else
        {
            if(zarovka.enabled)
            zarovka.enabled = false;
        }
        if (sviti)
        {
            timer = 1;
            if(!zarovka.enabled)
                zarovka.enabled = true;
        }
    }
}
