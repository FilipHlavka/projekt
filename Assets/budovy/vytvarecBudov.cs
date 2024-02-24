using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vytvarecBudov : MonoBehaviour
{
    GameObject Generator;
    budova Budova;
    GameObject Controler;
    BudovaCont bdvCont;
    PowerPointGenerator powerPointGenerator;

    // Start is called before the first frame update
    private void Awake()
    {
        Controler = GameObject.FindGameObjectWithTag("GameController");
        bdvCont = Controler.GetComponent<BudovaCont>();
        Generator = GameObject.FindGameObjectWithTag("generator");


        powerPointGenerator = Generator.GetComponent<PowerPointGenerator>();
        bdvCont.stav.AddListener(postav);

    }
    void Start()
    {
        // bude ve funkci kt. bude urèovat podle vzdálenosti, zda lze zapnout shop
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Budova = gameObject.AddComponent<vrt>();
            Budova.powerPointGenerator = powerPointGenerator;                                                                          // Destroy(this);
            Budova.akt();
        }
    }

    public void postav(List<UkladaniProBudovu> listBdv)
    {
        

        foreach (var budova in listBdv)
        {
            
            if (Vector2.Distance(budova.pozice,(Vector2)transform.position) < 1)
            {
                Debug.Log("nice" + (Vector2)transform.position);
                if(budova.nazev == "dul")
                {
                 
                    Budova = gameObject.AddComponent<dul>();
                    Budova.powerPointGenerator = powerPointGenerator;
                    Budova.akt();
                }
                if (budova.nazev == "vrt")
                {

                    Budova = gameObject.AddComponent<vrt>();
                    Budova.powerPointGenerator = powerPointGenerator;
                    Budova.akt();
                }
                if (budova.nazev == "banka")
                {

                    Budova = gameObject.AddComponent<banka>();
                    Budova.powerPointGenerator = powerPointGenerator;
                    Budova.akt();
                }
            }
        }
        
    }
}
