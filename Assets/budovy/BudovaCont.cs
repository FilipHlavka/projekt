using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

public class BudovaCont : MonoBehaviour
{
    public Zabal zabal = new Zabal();
    public List<UkladaniProBudovu> listBudov;
    cont Controller;
    public UnityEvent<List<UkladaniProBudovu>> stav;
    public static List<Vector2> poziceZnicenychBudov = new List<Vector2>();
    // Start is called before the first frame update
    void Awake()
    {
        Controller = gameObject.GetComponent<cont>();
        Controller.aktBudovy.AddListener(Pracuj);
    }
    public void Pracuj()
    {


        string filePath = Application.dataPath + "/enemies.json";

        string jsonData = File.ReadAllText(filePath);

        zabal = JsonUtility.FromJson<Zabal>(jsonData);

        listBudov = zabal.bdv;


        stav.Invoke(listBudov);
        
    }
}
