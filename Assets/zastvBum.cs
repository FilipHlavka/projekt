using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zastvBum : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem sys;
    // Update is called once per frame
    private void Start()
    {
        Invoke("zastav",2);
    }
    void zastav()
    {
        Time.timeScale = 0;
    }
    public void odestav()
    {
        Time.timeScale = 1;

    }
}
