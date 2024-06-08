using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zvukHrac : MonoBehaviour
{
    public static zvukHrac instance;
    public AudioSource zdroj;
    public AudioClip zvuk;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        zdroj.loop = true;
        zdroj.clip = zvuk;
       
       
    }

    public void Harj(bool stoji)
    {
        if(!stoji)
            zdroj.Play();
        else
            zdroj.Stop();
    }
}
