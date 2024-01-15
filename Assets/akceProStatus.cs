using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class akceProStatus : MonoBehaviour
{
    [SerializeField]
    Stat druh;
    public static UnityEvent<Stat,Collider2D> nastavStatus;
    // Start is called before the first frame update
    void Awake()
    {
        if (nastavStatus == null)
            nastavStatus = new UnityEvent<Stat, Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name + "Triggered");
        nastavStatus.Invoke(druh,collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        nastavStatus.Invoke(Stat.nic, collision);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
