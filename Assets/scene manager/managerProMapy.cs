using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class managerProMapy : MonoBehaviour
{
    public List<Button> list;

    // Start is called before the first frame update
    public string Mapa;
    void Start()
    {
        foreach (var tlac in list)
        {
            tlac.onClick.AddListener(() => { Mapa = tlac.name; });
        }
    }
    // ingamemanager.instance.PrepniNascenu( tlac.name ,true);
    // Update is called once per frame

    public void zacniHru()
    {
        if(Mapa != "")
          ingamemanager.instance.PrepniNascenu(Mapa, true);
    }
}
