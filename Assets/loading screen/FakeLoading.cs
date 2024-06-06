using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FakeLoading : MonoBehaviour
{
    [SerializeField]
    public TMP_Text text;
    [SerializeField]
    public Slider sl;
    [SerializeField]
    public float dobaTrvani = 1f;
    //public float 
    bool cekej;

    private void Start()
    {
        if (pauza.funguj != null)
        {
            pauza.funguj = true;

        }
        nastavCas();
        Time.timeScale = 1f;
        StartCoroutine(FakeLoadingCoroutine());
      
    }
    public void nastavCas()
    {
        dobaTrvani = ingamemanager.cas;
    }
    private void Update()
    {
      //  Debug.Log(cekej);
    }
    private IEnumerator FakeLoadingCoroutine()
    {
        float doba = 0f;

        while (doba < dobaTrvani)
        {
           
            if (!cekej)
            {
                

                doba += Time.deltaTime;
                text.text = "Loading... " + Mathf.FloorToInt((doba / dobaTrvani) * 100) + "%";
                sl.value = Mathf.FloorToInt((doba / dobaTrvani) * 100);
                if (Random.Range(0, 601) % 600 == 0)
                {
                    cekej = true;

                    StartCoroutine(pockej());

                }
            }
            
             yield return null;
        }

       
       SceneManager.LoadScene(ingamemanager.dalsiScena); 
    }


    private IEnumerator pockej()
    {
        Debug.Log("co se to dìje");
        yield return new WaitForSeconds(Random.Range(1,3));
           
            cekej = false;
    }
}
