using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.UI;

public class vytvarecBudov : MonoBehaviour
{
    GameObject Generator;
    budova Budova;
    GameObject Controler;
    BudovaCont bdvCont;
    PowerPointGenerator powerPointGenerator;
    [SerializeField]
    Button vrtTlac;
    [SerializeField]
    Button dulTlac;
    [SerializeField]
    Button bankaTlac;
    Canvas op;
    GameObject player;
    bool pom = true;
    bool pom2= false;

    
    // Start is called before the first frame update
 
    private void Awake()
    {
        Controler = GameObject.FindGameObjectWithTag("GameController");
        bdvCont = Controler.GetComponent<BudovaCont>();
        Generator = GameObject.FindGameObjectWithTag("generator");
        op = vrtTlac.GetComponentInParent<Canvas>();
        powerPointGenerator = Generator.GetComponent<PowerPointGenerator>();
        bdvCont.stav.AddListener(postav);
       
    }
    void Start()
    {

        foreach (var pozice in BudovaCont.poziceZnicenychBudov)
        {
            if (Vector2.Distance(pozice, transform.position) < 7)
            {
                Destroy(gameObject);
            }
        }

        StartCoroutine(pockej());
        op.enabled = false;
        vrtTlac.onClick.AddListener(() => spawnVrt(true));
        dulTlac.onClick.AddListener(() => spawnDul(true));
        bankaTlac.onClick.AddListener(() => spawnBanka(true));
    }
    IEnumerator pockej()
    {
        // spustit pozdìjš
        yield return new WaitForSeconds(1.5f);
        pom2 = true;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            InvokeRepeating("DejHrace", 0, 1);
        }
    }
    void DejHrace()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CancelInvoke("DejHrace");
           
        }
    }
    #region spawny
    void spawnBanka(bool platim)
    {
        if (platim)
        {
            if(PowerPointGenerator.PP - 10 >= 0)
            {
                PowerPointGenerator.PP -= 10;
                PPtext.aktualizuj();
                Budova = gameObject.AddComponent<banka>();
                Budova.powerPointGenerator = powerPointGenerator;
                Budova.akt();
                Destroy(this);
            }
        }
        else
        {
            Budova = gameObject.AddComponent<banka>();
            Budova.powerPointGenerator = powerPointGenerator;
            Budova.akt();
            Destroy(this);
        }
      
    }

    void spawnDul(bool platim)
    {
        if (platim)
        {
            if (PowerPointGenerator.PP - 10 >= 0)
            {
                PowerPointGenerator.PP -= 10;
                PPtext.aktualizuj();

                Budova = gameObject.AddComponent<dul>();
                Budova.powerPointGenerator = powerPointGenerator;
                Budova.akt();
                Destroy(this);
            }
        }
        else
        {
            Budova = gameObject.AddComponent<dul>();
            Budova.powerPointGenerator = powerPointGenerator;
            Budova.akt();
            Destroy(this);
        }
    }

    void spawnVrt(bool platim)
    {

        if (platim)
        {
            if (PowerPointGenerator.PP - 15 >= 0)
            {
                PowerPointGenerator.PP -= 15;
                PPtext.aktualizuj();

                Budova = gameObject.AddComponent<vrt>();
                Budova.powerPointGenerator = powerPointGenerator;
                Budova.akt();
                Destroy(this);
            }
        }
        else
        {
            Budova = gameObject.AddComponent<vrt>();
            Budova.powerPointGenerator = powerPointGenerator;
            Budova.akt();
            Destroy(this);
        }
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        if (pom2)
        {
            
            if (player != null)
            {
                if (Vector2.Distance((Vector2)player.transform.position, transform.position) < 7)
                {
                    op.enabled = true;
                    pom = false;
                }
                else if (!pom)
                {
                    op.enabled = false;
                    pom = true;
                }
            }
           
            
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
                    spawnDul(false);

                }
                if (budova.nazev == "vrt")
                {

                    spawnVrt(false);
                }
                if (budova.nazev == "banka")
                {
                    spawnBanka(false);
                    
                }
            }
        }
        
    }


    private void OnDestroy()
    {
         op.enabled = false;
    }
}
