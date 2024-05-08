using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PowerPointGenerator : MonoBehaviour
{
    public static PowerPointGenerator instance;
    public static PowerPointGenerator Instance => instance;
    public int mena;
    public bool stuj;
    [SerializeField]
    public int Max;
    public int bonusMax;
    public int poKolika;
    public bool pom = true;
    public int bonusPoKolika;
    [SerializeField]
    TMP_Text Text;
    float timer;
    float duration = 0.2f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(PridejMenu());
        Text.text = "Mìna: " + mena;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (mena <= Max + bonusMax && !pom)
        {
            if (!stuj)
            {
                pom = true;
                StartCoroutine(PridejMenu());
            }
            
        }
    }
    IEnumerator PridejMenu()
    {
        while (pom)
        {
            yield return new WaitForSeconds(3f);
            ZmenText(mena, poKolika + mena + bonusPoKolika);
            Debug.Log(mena);
            if (mena >= Max + bonusMax)
            {
                pom = false;
            }
        }
    }
    public void ZmenText(int puvodni, int potom)
    {

        StartCoroutine(FungujUs(puvodni, potom));
        Text.text = "Mìna: " + potom;
        mena = potom;

    }
    private IEnumerator FungujUs(int puvodni, int potom)
    {
        timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float idk = Mathf.Clamp01(timer / duration);

            int pom = Mathf.RoundToInt(Mathf.Lerp(puvodni, potom, idk));
            Text.text = "Mìna: " + pom;

            yield return null;
        }
        mena = potom;

    }
}
