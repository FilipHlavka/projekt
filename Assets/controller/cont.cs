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
            // za�to�il enemy
            Debug.Log("�tok enemy");
        }
        else
        {
            // za�to�il hr��
            Debug.Log("�tok hr��");
        }
    }
}
