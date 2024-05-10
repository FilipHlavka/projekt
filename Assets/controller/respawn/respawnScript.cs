using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class respawnScript : MonoBehaviour
{
    
    [SerializeField]
    private List<GameObject> jednotky;
    [SerializeField]
    private GameObject panel;

    RectTransform ptf;
    int k = 60;
    string jm;
    [SerializeField]
    GameObject spawnPoint;

    [SerializeField]
    camPohyb kamera;
    // Start is called before the first frame update
    private void Awake()
    {
       
        ptf = panel.GetComponent<RectTransform>();

    }
    void Start()
    {
    }

    private void Tlacitko(int odsazeni,string jm)
    {
      
        GameObject btObj = new GameObject("Button");
        btObj.transform.SetParent(ptf.transform);

       
        Image tlacObr = btObj.AddComponent<Image>();
        tlacObr.color = Color.white; 

        
        Button tlac = btObj.AddComponent<Button>();

        
        TextMeshProUGUI tlacText = new GameObject("Text").AddComponent<TextMeshProUGUI>();
        tlacText.transform.SetParent(btObj.transform);
        tlacText.text = vratTxt(jm);
        tlacText.color = Color.black;
        tlacText.rectTransform.sizeDelta = new Vector2(500f, 50f);
        tlacText.alignment = TextAlignmentOptions.Center;

        k += 63;

        RectTransform buttonRectTransform = btObj.GetComponent<RectTransform>();
        buttonRectTransform.sizeDelta = new Vector2(500f, 50f);
        buttonRectTransform.anchoredPosition = new Vector2(0, odsazeni-k);

       
        tlac.onClick.AddListener(() => Urci(jm));
    }
    private string vratTxt(string jm)
    {
            
        if(jm == "zakladniHrac")
        {
            jm = "Pìchota: 20";
        }
        if (jm == "tank")
        {
            jm = "Tank: 45";
        }
        if (jm == "spg")
        {
            jm = "Dìlostøelectvo: 60";
        }
        if (jm == "motor")
        {
            jm = "Motorizovaná jednotka: 45";
        }
        return jm;
    }
    private void Urci(string jm)
    {
        int cost = 0;
        if (jm == "zakladniHrac")
        {
            cost = 20;
        }
        if (jm == "tank")
        {
            cost = 45;
        }
        if (jm == "spg")
        {
            cost = 60;
        }
        if (jm == "motor")
        {
            cost = 45;
        }
        Debug.Log(jm);
        foreach (GameObject jed in jednotky)
        {
            if(jm == jed.GetComponent<zakl>().nameHr)
            {
                
                if (PowerPointGenerator.instance.mena - cost > 0)
                {
                    PowerPointGenerator.instance.ZmenText(PowerPointGenerator.instance.mena, PowerPointGenerator.instance.mena - cost);
                    PowerPointGenerator.instance.mena = PowerPointGenerator.instance.mena - cost;
                    PowerPointGenerator.instance.Max = PowerPointGenerator.instance.Max - cost;
                    Instantiate(jed, spawnPoint.transform.position, Quaternion.Euler(0, 0, 0));
                    panel.SetActive(false);
                    kamera.movex = spawnPoint.transform.position.x;
                    kamera.movey = spawnPoint.transform.position.y;
                    
                }
                else
                {
                    Debug.Log("Nedostatek penìz");
                }

            }
        }
        
    }

    public void DelejNeco()
    {
        int topPosition = Mathf.Abs((int)(ptf.anchoredPosition.y + ptf.sizeDelta.y / 2f));

        panel.SetActive(true);
        foreach (GameObject jed in jednotky)
        {
             jm = jed.GetComponent<zakl>().nameHr;
            Debug.Log(jm);
            Tlacitko(topPosition, jm);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
    }
}
