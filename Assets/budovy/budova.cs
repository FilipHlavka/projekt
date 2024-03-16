using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class budova : MonoBehaviour
{
    // Start is called before the first frame update
    public int zivoty;
    public int stity;
    public string jmeno;
    public Sprite sprite; // založení zastavením èasu a ukázáním nabídky pro nákup
    public PowerPointGenerator powerPointGenerator; // nièení enemy nebo èasovaèem
    public Vector2 pozice;
    public List<GameObject> enemies;
    Slider slider;
    SpriteRenderer spriteRenderer;
   
   // BudovaCont bdvCont;

   
    protected void Checkuj()
    {
        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, transform.position) < 11)
            {
                zivoty-= 2;
                slider.value = zivoty;
                Debug.Log(zivoty);
                if (zivoty <= 0 )
                {
                    
                    BudovaCont.poziceZnicenychBudov.Add((Vector2)transform.position);
                   // Debug.Log(BudovaCont.poziceZnicenychBudov.Count() + " to je ten list");
                    Destroy(gameObject);
                }
            }
        }
    }
    protected void aktSprite() 
    {
        zivoty = 10;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //Debug.Log(spriteRenderer);
        sprite = Resources.Load<Sprite>("budovy/" + jmeno);
        //Debug.Log(sprite);
        spriteRenderer.sprite = sprite;
        slider = gameObject.GetComponentInChildren<Slider>();
        slider.maxValue = zivoty;
        //Debug.Log(Application.dataPath);
        StartCoroutine(pockej());
        InvokeRepeating("Checkuj", 2, 2);
    }

    public virtual void akt() {  }

    IEnumerator pockej()
    {

        yield return new WaitForSeconds(1f);
        enemies = GameObject.FindGameObjectsWithTag("enemy").ToList();
       // Debug.Log(BudovaCont.poziceZnicenychBudov.Count() + "to je ten list");
    }
}
