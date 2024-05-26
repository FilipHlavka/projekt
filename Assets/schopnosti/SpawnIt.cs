using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static VyberSch;

public class SpawnIt : MonoBehaviour
{
    public static SpawnIt instance;

    [SerializeField]
    schopnostScriptable particles;

    Camera cam;
    HvezdyASchWrapper wp;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       
        KontrolaANacteni();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!PowerPointGenerator.instance.stuj)
        {
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {


                foreach (var particle in particles.prefs)
                {
                    if (particle.schopnost.nazev == wp.aktSchopnost)
                    {
                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                        LayerMask layerMask = ~LayerMask.GetMask("player");
                        wp.aktSchopnost = "";
                        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                        {
                            Instantiate(particle.schopnost, hit.point, Quaternion.identity);
                            //pouzito = true;

                        }


                    }
                }


            }
        }



    }
    public void Uloz()
    {
        BinaryFormatter formator = new BinaryFormatter();
        try
        {
            File.Delete(Application.dataPath + "/Hvezdy.bin");
        }
        catch
        {
            Debug.Log("sakra");
        }
        //wp.hvezdy = 5; // smazat
        FileStream stream = File.Create(Application.dataPath + "/Hvezdy.bin");
        formator.Serialize(stream, wp);
        stream.Close();
    }

    void KontrolaANacteni()
    {
        if (!File.Exists(Application.dataPath + "/Hvezdy.bin"))
            VytvorFile();

        BinaryFormatter formator = new BinaryFormatter();

        FileStream stream = File.Open(Application.dataPath + "/Hvezdy.bin", FileMode.Open);

        wp = (HvezdyASchWrapper)formator.Deserialize(stream);
        stream.Close();
        
        
    }

    void VytvorFile()
    {
        BinaryFormatter formator = new BinaryFormatter();


        HvezdyASchWrapper hv = new HvezdyASchWrapper();
        hv.hvezdy = 5;
        hv.aktSchopnost = "";
        FileStream stream = File.Create(Application.dataPath + "/Hvezdy.bin");
        formator.Serialize(stream, hv);
        stream.Close();

    }
}
