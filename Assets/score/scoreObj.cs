using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class scoreObj : MonoBehaviour
{
    [SerializeField]
    Button kos;
    [SerializeField]
    public TMP_Text mapa;
    [SerializeField]
    public TMP_Text obtiznost;
    [SerializeField]
    public TMP_Text cas;
    public int id;
    public UnityEvent<int> eventOdstran;
    [SerializeField]
    GameObject unseen;
    // Start is called before the first frame update
    void Start()
    {
        kos.onClick.AddListener(() =>
        {
            eventOdstran.Invoke(id);
            Instantiate(unseen, gameObject.transform.parent);
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
