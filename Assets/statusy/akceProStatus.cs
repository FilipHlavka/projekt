using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class akceProStatus : MonoBehaviour
{
    Staty st;
    public static UnityEvent<Staty,int> nastavStatus;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (nastavStatus == null)
            nastavStatus = new UnityEvent<Staty, int>();
        st = gameObject.GetComponent<Staty>();
        //Debug.Log("aaaaaaaaaaa");
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        Debug.Log(collision.name + "Triggered");
        if(collision.CompareTag("PlayerCollider") || collision.CompareTag("enemyCollider"))
        nastavStatus.Invoke(st,collision.gameObject.GetComponent<ingameStatusy>().id);
    }
    private void OnTriggerExit(Collider collision)
    {
        
        Staty sta = new Staty();
        sta.druh = Stat.nic;
        sta.druhSpeed = Speed.nic;
        sta.stit = Shield.nic;
        sta.range = Vyditelnost.nic;
        if (collision.CompareTag("PlayerCollider") || collision.CompareTag("enemyCollider"))
            nastavStatus.Invoke(sta, collision.gameObject.GetComponent<ingameStatusy>().id);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
