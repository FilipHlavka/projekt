using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZaklSchopnost : MonoBehaviour
{
    [SerializeField]
    public string nazev;
    [SerializeField]
    public float dobaTrvani;
    [SerializeField]
    public int dmg;
   
    public virtual void Start()
    {
        StartCoroutine(Znic());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("enemyCollider"))
        {
            other.gameObject.GetComponentInParent<enemy>().TakeDamage(dmg);
        }
        if (other.CompareTag("PlayerCollider"))
        {
            other.gameObject.GetComponentInParent<zakl>().TakeDamage(dmg);
        }
    }

    void Update()
    {
        
    }

    public IEnumerator Znic()
    {
        yield return new WaitForSeconds(dobaTrvani);
        Destroy(gameObject);
    }
}
