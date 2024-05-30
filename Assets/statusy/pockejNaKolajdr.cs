using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pockejNaKolajdr : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Collider kolajdr;
    private void Awake()
    {
        StartCoroutine(pockej());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator pockej()
    {
        kolajdr.enabled = false;

        yield return new WaitForSeconds(0.2f);
        kolajdr.enabled = true;
    }
}
