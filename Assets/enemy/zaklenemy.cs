using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zaklenemy : MonoBehaviour
{
    [SerializeField]
    protected int zivoty;
    [SerializeField]
    protected int atk;
    [SerializeField]
    protected float def;
    [SerializeField]
    public float rychlost;
    [SerializeField]
    public float range;
    [SerializeField]
    public float dosahDetekce;
    public float defenseMulti;
   
    public float atkMulti;
    // Start is called before the first frame update
    public void Awake()
    {
        gameObject.GetComponent<data>().zivoty = zivoty;
        gameObject.GetComponent<data>().atk = atk;
        gameObject.GetComponent<data>().def = def;
        gameObject.GetComponent<data>().rychlost = rychlost;
        gameObject.GetComponent<data>().range = range;
        gameObject.GetComponent<data>().dosahDetekce = dosahDetekce;
    }

    
}
