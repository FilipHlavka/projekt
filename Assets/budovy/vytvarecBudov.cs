using FOVMapping;
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
    public static bool muzePricist = true;

   
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
        vrtTlac.onClick.AddListener(() => spawn(true, bdv.bdv[1], bdv.bdv[1].prefab.zivoty));
        dulTlac.onClick.AddListener(() => {
            spawn(true, bdv.bdv[0], bdv.bdv[0].prefab.zivoty);

        });
        bankaTlac.onClick.AddListener(() => spawn(true, bdv.bdv[2], bdv.bdv[2].prefab.zivoty));
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


                budova gm = Instantiate(budova.prefab, new Vector3(transform.position.x,transform.position.y-0.6f,transform.position.z), transform.rotation);
                gm.transform.SetParent(gameObject.transform);
                gm.powerPointGenerator = powerPointGenerator;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gm.slider.value = zivoty;
                gm.akt();
                Destroy(this);

            }
        }
        else
        {
            budova gm = Instantiate(budova.prefab, new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z), transform.rotation);
            gm.transform.SetParent(gameObject.transform);
            gm.powerPointGenerator = powerPointGenerator;
            gm.zivoty = zivoty;
            gm.slider.value = zivoty;
            gm.akt();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(this);

        }
       FOVManager.instance.FindAllFOVAgents();
      
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

        muzePricist = false;

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
                    spawn(false, bdv.bdv[1], budova.zivoty);
                }
                if (budova.nazev == "nemocnice")
                {
                    spawn(false, bdv.bdv[2], budova.zivoty);

                }
            }
        }
        
    }


    private void OnDestroy()
    {
         op.enabled = false;
    }
}
