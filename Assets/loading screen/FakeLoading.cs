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
    
    public float dobaTrvani = 5f;
    bool cekej;

    private void Start()
    {
        StartCoroutine(FakeLoadingCoroutine());
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
                if (Random.Range(0, 301) % 300 == 0)
                    StartCoroutine(pockej());
            }
            
             yield return null;
        }

       
       //SceneManager.LoadScene("NextScene"); 
    }

    private IEnumerator pockej()
    {
            cekej = true;
            yield return new WaitForSeconds(Random.Range(1,3));
            cekej = false;
    }
}
