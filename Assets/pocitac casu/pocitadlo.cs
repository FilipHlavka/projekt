using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocitadlo : MonoBehaviour
{
    public static pocitadlo instance;
    public float timer;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
    }
}
