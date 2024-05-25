using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class managerProMapy : MonoBehaviour
{
    public List<Button> list;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var tlac in list)
        {
            tlac.onClick.AddListener(() => { ingamemanager.instance.PrepniNascenu( tlac.name ,true); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
