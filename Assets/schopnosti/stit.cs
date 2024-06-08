using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stit : MonoBehaviour
{
    public static stit instance;
    [SerializeField]
    public ParticleSystem sys;
    [SerializeField]
    pohybHrace phbHr;

    private void Awake()
    {
        instance = this;
    }

    public void NasadStit()
    {
        sys.Play();
        phbHr.agent.isStopped = true;
    }
}
