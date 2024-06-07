using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class DejNaTlacitka : MonoBehaviour
{
    void Start()
    {
       
        Button[] buttons = FindObjectsOfType<Button>();

        
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(Zavolej);
        }
    }

    public void Zavolej()
    {
        Hlasy.instance.Prehraj();
    }
}
