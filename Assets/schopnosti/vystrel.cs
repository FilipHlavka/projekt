using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vystrel : MonoBehaviour
{
    [SerializeField]
    ParticleSystem sys;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fire()
    {
        sys.Play(true);
    }
}
