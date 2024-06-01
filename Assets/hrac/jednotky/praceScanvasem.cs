using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class praceScanvasem : MonoBehaviour
{
    public static UnityEvent<bool,int> nastavCanvasNaEnemy;

    // Start is called before the first frame update
    void Awake()
    {
        if (nastavCanvasNaEnemy == null)
            nastavCanvasNaEnemy = new UnityEvent<bool,int>();
    }


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("colliduju s " + collision.tag);
        if (collision.CompareTag("enemyCollider"))
            nastavCanvasNaEnemy.Invoke(true, collision.gameObject.GetComponent<ingameStatusy>().id);
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("PlayerCollider") || collision.CompareTag("enemyCollider"))
        nastavCanvasNaEnemy.Invoke(false, collision.gameObject.GetComponent<ingameStatusy>().id);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
