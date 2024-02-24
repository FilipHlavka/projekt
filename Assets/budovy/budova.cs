using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class budova : MonoBehaviour
{
    // Start is called before the first frame update
    public int zivoty;
    public int stity;
    public string jmeno;
    public Sprite sprite; // založení zastavením èasu a ukázáním nabídky pro nákup
    public PowerPointGenerator powerPointGenerator; // nièení enemy nebo èasovaèem
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
