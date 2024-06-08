using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hlasy : MonoBehaviour
{
    public static Hlasy instance;
    public AudioSource zdroj;
    public AudioClip klik;
    public AudioClip exploze;
    public AudioClip plyn;
    public bool prvni = true;

    private void Awake()
    {
        
        instance = this;

        List<Hlasy> sm = FindObjectsOfType<Hlasy>().ToList();
        foreach (Hlasy h in sm)
        {
            if(h != instance)
            {
                Destroy(h.gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Prehraj()
    {
        zdroj.Stop();
        zdroj.clip = klik;
        zdroj.Play();
        
    }

    public void Exploze()
    {
       
        zdroj.clip = exploze;
        zdroj.Play();
    }

    public void Plyn()
    {

        zdroj.clip = plyn;
        zdroj.Play();
    }
}
