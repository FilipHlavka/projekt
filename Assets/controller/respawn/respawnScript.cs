using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FOVMapping;

public class respawnScript : MonoBehaviour
{
    public static respawnScript instance;
    [SerializeField]
    private List<GameObject> jednotky;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    hracScriptable hrci;
    [SerializeField]
    Button button;
    RectTransform ptf;
    //int k = 60;
    //string jm;
    [SerializeField]
    GameObject spawnPoint;

    [SerializeField]
    camPohyb kamera;
    
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        ptf = panel.GetComponent<RectTransform>();

    }
    void Start()
    {
    }

    
    public void DelejNeco()
    {
        /*
        int topPosition = Mathf.Abs((int)(ptf.anchoredPosition.y + ptf.sizeDelta.y / 2f));

        panel.SetActive(true);
        foreach (GameObject jed in jednotky)
        {
             jm = jed.GetComponent<zakl>().nameHr;
            Debug.Log(jm);
            Tlacitko(topPosition, jm);

        }*/

        VytvorTlacitko();
       
    }

    void VytvorTlacitko()
    {
        panel.SetActive(true);
        pauza.funguj = false;
        foreach (var hr in hrci.prefs)
        {
            Debug.Log(hr);
           Button tlac =  Instantiate(button,panel.transform);
            tlac.onClick.AddListener(() =>
            {
                if (PowerPointGenerator.instance.mena - hr.cenaOtaznik > 0)
                {
                    PowerPointGenerator.instance.ZmenText(PowerPointGenerator.instance.mena, PowerPointGenerator.instance.mena - hr.cenaOtaznik);
                    PowerPointGenerator.instance.mena = PowerPointGenerator.instance.mena - hr.cenaOtaznik;
                    PowerPointGenerator.instance.Max = PowerPointGenerator.instance.Max - hr.cenaOtaznik;
                    Instantiate(hr.hrac, spawnPoint.transform.position, Quaternion.Euler(0, 0, 0));
                    panel.SetActive(false);
                    FOVManager.instance.FindAllFOVAgents();

                    pauza.funguj = true;
                }
                
            });
            TMP_Text text = tlac.GetComponentInChildren<TMP_Text>();
            text.text = hr.hrac.nameHr + ": " + hr.cenaOtaznik;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
    }
}
