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
    [SerializeField]
    budovyScriptable bdv;
    
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
            if (Vector3.Distance(pozice, transform.position) < 7)
            {
                Destroy(gameObject);
            }
        }

        StartCoroutine(pockej());
        op.enabled = false;
        vrtTlac.onClick.AddListener(() => spawnVrt(true));
        dulTlac.onClick.AddListener(() => {
            spawn(true, bdv.bdv[0], bdv.bdv[0].prefab.zivoty);

        });
        bankaTlac.onClick.AddListener(() => spawn(true,null,4));
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

    void spawn(bool platim, budovyHolder budova, int zivoty)
    {
        
        if (platim)
        {
            if (PowerPointGenerator.instance.mena - budova.cena >= 0)
            {
                PowerPointGenerator.instance.ZmenText(PowerPointGenerator.instance.mena, PowerPointGenerator.instance.mena - budova.cena);
                PowerPointGenerator.instance.mena -= budova.cena;


                budova gm = Instantiate(budova.prefab, transform.position, transform.rotation);
                gm.transform.SetParent(gameObject.transform);
                gm.powerPointGenerator = powerPointGenerator;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
               /* gm.zivoty = zivoty;
                gm.slider.value = zivoty;
               */
                Destroy(this);

            }
        }
        else
        {
            budova gm = Instantiate(budova.prefab, transform.position, transform.rotation);
            gm.transform.SetParent(gameObject.transform);
            gm.powerPointGenerator = powerPointGenerator;
            gm.zivoty = zivoty;
            gm.slider.value = zivoty;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(this);

        }
       
      
    }
 

    void spawnDul(bool platim) // radar !!!!!!!!!!!!!!!!!
    {
        if (platim)
        {
            if (PowerPointGenerator.instance.mena - 10 >= 0)
            {
                PowerPointGenerator.instance.ZmenText(PowerPointGenerator.instance.mena, PowerPointGenerator.instance.mena - 10);
                PowerPointGenerator.instance.mena -= 10;
               

                Budova = gameObject.AddComponent<radar>();
                Budova.powerPointGenerator = powerPointGenerator;
                Budova.akt();
                Destroy(this);
            }
        }
        else
        {
            Budova = gameObject.AddComponent<radar>();
            Budova.powerPointGenerator = powerPointGenerator;
            Budova.akt();
            Destroy(this);
        }
    }

    void spawnVrt(bool platim)
    {

        if (platim)
        {
            if (PowerPointGenerator.instance.mena - 15 >= 0)
            {
                PowerPointGenerator.instance.ZmenText(PowerPointGenerator.instance.mena, PowerPointGenerator.instance.mena - 15);
                PowerPointGenerator.instance.mena -= 15;
                

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
              

                if (Vector3.Distance(player.transform.position, transform.position) < 7)
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
            else
            {
                pom2 = false;   
                StartCoroutine(pockej());
            }
           
            
        }
        
        
    }

    public void postav(List<UkladaniProBudovu> listBdv)
    {
        
        

        foreach (var budova in listBdv)
        {
            Debug.Log("no nìco" + budova.zivoty);
            if (Vector3.Distance(budova.pozice,transform.position) < 1)
            {
                Debug.Log("nice" + budova.nazev);

                if(budova.nazev == "radar")
                {
                    spawn(false, bdv.bdv[0], budova.zivoty); //radar !!!!!!!!!!!

                }
                if (budova.nazev == "vrt")
                {

                    spawnVrt(false);
                }
                if (budova.nazev == "banka")
                {
                    spawn(true, null, 4);


                }
            }
        }
        
    }


    private void OnDestroy()
    {
         op.enabled = false;
    }
}
