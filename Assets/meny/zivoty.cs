using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zivoty : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;
    
    public static Zivoty instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        aktText();
    }
    public void aktText()
    {
        text.text = "Lives: " + vyhra.pocetZivotu;
    }
}
