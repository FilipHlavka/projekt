using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class akceProStatus : MonoBehaviour
{
    Staty st;
    public static UnityEvent<Staty,Collider2D> nastavStatus;
    // Start is called before the first frame update
    void Awake()
    {
        if (nastavStatus == null)
            nastavStatus = new UnityEvent<Staty,Collider2D>();
        st = gameObject.GetComponent<Staty>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name + "Triggered");
        nastavStatus.Invoke(st,collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Staty sta = new Staty();
        sta.druh = Stat.nic;
        sta.druhSpeed = Speed.nic;
        sta.stit = Shield.nic;
        sta.range = Vyditelnost.nic;
        nastavStatus.Invoke(sta, collision);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
