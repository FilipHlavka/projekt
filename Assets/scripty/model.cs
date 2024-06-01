using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class model : MonoBehaviour
{
    public string nazev;
    [SerializeField]
    public vystrel vystrel;
    public bool jeEnemy;

    public void Vystrel(bool enemy)
    {
        if(enemy == jeEnemy)
        vystrel.Fire();
    }
}
