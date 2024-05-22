using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIt : MonoBehaviour
{
   
    [SerializeField]
    schopnostScriptable particles;
    bool pouzito = false;
    public string aktSchopnost = "gas";
    Camera cam;

 
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PowerPointGenerator.instance.stuj)
        {
            if (Input.GetKeyDown(KeyCode.Mouse2) && !pouzito)
            {


                foreach (var particle in particles.prefs)
                {
                    if (particle.schopnost.nazev == aktSchopnost)
                    {
                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                        LayerMask layerMask = ~LayerMask.GetMask("player");

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
}
