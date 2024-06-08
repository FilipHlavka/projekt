using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZvukSouboj : MonoBehaviour
{
    public static ZvukSouboj instance;
    public AudioSource src;
    public AudioClip vystrel;
    public AudioClip def;
    public AudioClip buf;
    public AudioClip power;

    public void Awake()
    {
        instance = this;
    }
    public void Strel()
    {
        src.clip = vystrel;
        src.Play();
    }
    public void Buff()
    {
        src.clip = buf;
        src.Play();
    }
    public void Def()
    {
        src.clip = def;
        src.Play();
    }
    public void Power()
    {
        src.clip = power;
        src.Play();
    }
}
