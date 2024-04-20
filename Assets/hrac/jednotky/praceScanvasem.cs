using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class praceScanvasem : MonoBehaviour
{
    public static UnityEvent<bool,Collider2D> nastavCanvasNaEnemy;

    // Start is called before the first frame update
    void Start()
    {
        if (nastavCanvasNaEnemy == null)
            nastavCanvasNaEnemy = new UnityEvent<bool,Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("halo halo");
        nastavCanvasNaEnemy.Invoke(true,collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        nastavCanvasNaEnemy.Invoke(false,collision);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
