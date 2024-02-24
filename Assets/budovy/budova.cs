using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class budova : MonoBehaviour
{
    // Start is called before the first frame update
    public int zivoty;
    public int stity;
    public string jmeno;
    public Sprite sprite; // zalo�en� zastaven�m �asu a uk�z�n�m nab�dky pro n�kup
    public PowerPointGenerator powerPointGenerator; // ni�en� enemy nebo �asova�em
    public Vector2 pozice;
    SpriteRenderer spriteRenderer;

    protected void aktSprite() 
    { 
      
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        Debug.Log(Application.dataPath);
    }
    
    public virtual void akt() {  }
    
}
