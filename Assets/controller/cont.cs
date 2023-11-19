using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class cont : MonoBehaviour
{
    public void ZK()
    {
        Debug.Log("funguje");
    }
    public void Prepnuti(bool kdo)
    {
        if (kdo)
        {
            // zaútoèil enemy
            Debug.Log("útok enemy");
        }
        else
        {
            // zaútoèil hráè
            Debug.Log("útok hráè");
        }
    }
}
