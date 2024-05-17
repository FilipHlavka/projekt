using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class budova : MonoBehaviour
{
    // Start is called before the first frame update
    public int zivoty;
   
    public string jmeno;
   
    public PowerPointGenerator powerPointGenerator; // nièení enemy nebo èasovaèem
    public Vector3 pozice;
    public List<GameObject> enemies;
    [SerializeField]
    public Slider slider;
    bool inProgress = false;


    // BudovaCont bdvCont;

    public virtual void Start()
    {
        StartCoroutine(pockej());
        InvokeRepeating("Checkuj", 2, 2);
        slider = gameObject.GetComponentInChildren<Slider>();
        //StartCoroutine(pockejJeste());
        //slider.maxValue = zivoty;
    }

    protected void Checkuj()
    {
        Debug.Log("halo halo");
        if(enemies.Count == 0 && !inProgress)
        {
            enemies.Clear();
            StartCoroutine(pockej());
            return;
        }
        foreach (var enemy in enemies)
        {
            if(enemy != null)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) < 11)
                {
                    zivoty -= 2;
                    slider.value = zivoty;
                    //Debug.Log(slider.value + "slider.value");

                    Debug.Log(zivoty);
                    if (zivoty <= 0)
                    {
                        BudovaCont.poziceZnicenychBudov.Add(transform.position);
                        Debug.Log(BudovaCont.poziceZnicenychBudov.Count() + " to je ten list");
                        Destroy(gameObject);
                    }
                }
            }
            else if (!inProgress)
            {
                enemies.Clear();
                StartCoroutine(pockej());
                return;
            }

        }
    }
    
    public virtual void akt() {  }

    IEnumerator pockej()
    {
        inProgress = true;
        yield return new WaitForSeconds(1f);
        enemies = GameObject.FindGameObjectsWithTag("enemy").ToList();
        Debug.Log(enemies.Count + "pocet enemy v listu");
        inProgress = false;
        //Debug.Log(BudovaCont.poziceZnicenychBudov.Count() + "to je ten list");
    }

 
}
