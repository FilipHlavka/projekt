using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEditor;

public class VyberSch : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    TMP_Text textPopis;
    [SerializeField]
    TMP_Text textJm;
    [SerializeField]
    schopnostScriptable sch;
    [SerializeField]
    TMP_Text cena;
    int schCislo = 0;
    HvezdyASchWrapper wp;
    int hvezdy;
    [SerializeField]
    TMP_Text textHvezdy;
    string aktSchopnost;
    [SerializeField]
    GameObject vyskakJizJe;
    [SerializeField]
    GameObject upozorneni;
    [SerializeField]
    TMP_Text textKoupe;
    [SerializeField]
    TMP_Text textNePenize;
    [SerializeField]
    TMP_Text volba;

    // Start is called before the first frame update
    void Start()
    {
        vyskakJizJe.SetActive(false);
        upozorneni.SetActive(false);
        nacti();
        image.sprite = Resources.Load<Sprite>("schopnosti/" + sch.prefs[0].schopnost.nazev);
        textPopis.text = sch.prefs[0].schopnost.popis;
        textJm.text = sch.prefs[0].schopnost.nazev;
        cena.text = "Cost: " + sch.prefs[0].schopnost.cena + " ";
    }
    
    public void Prepni(bool vpred)
    {
        if (vpred)
        {
            schCislo++;
            if (schCislo > sch.prefs.Count - 1)
                schCislo = 0;
            zmenSchopnost();

        }
        else
        {
            schCislo--;
            if (schCislo < 0)
                schCislo = sch.prefs.Count - 1;
            zmenSchopnost();
        }
    }
    void zmenSchopnost()
    {
        image.sprite = Resources.Load<Sprite>("schopnosti/" + sch.prefs[schCislo].schopnost.nazev);
        textPopis.text = sch.prefs[schCislo].schopnost.popis;
        textJm.text = sch.prefs[schCislo].schopnost.nazev;
        cena.text = "Cost: " + sch.prefs[schCislo].schopnost.cena + " ";


    }
    void nacti()
    {

        if (!File.Exists(Application.dataPath + "/Hvezdy.bin"))
            VytvorFile();


        BinaryFormatter formator = new BinaryFormatter();

        FileStream stream = File.Open(Application.dataPath + "/Hvezdy.bin", FileMode.Open);

        wp = (HvezdyASchWrapper)formator.Deserialize(stream);
        stream.Close();
        textHvezdy.text = wp.hvezdy + " ";
        hvezdy = wp.hvezdy;
        aktSchopnost = wp.aktSchopnost;
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
    public void Kup()
    {
        
        Debug.Log(aktSchopnost);
        if (aktSchopnost == "" || aktSchopnost == null)
        {
            KupFr();
            
        }
        else if (hvezdy - sch.prefs[schCislo].schopnost.cena < 0)
        {
            upozorneni.SetActive(true);
            textKoupe.enabled = false;
            textNePenize.enabled = true;
        }
        else
        {
            vyskakJizJe.SetActive(true);
            volba.text = "You already have an active " + aktSchopnost + ", do you wish to buy new product?";
        }

    }
    public void uloz()
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
        wp.hvezdy = hvezdy;
        wp.aktSchopnost = aktSchopnost;
        FileStream stream = File.Create(Application.dataPath + "/Hvezdy.bin");
        formator.Serialize(stream, wp);
        stream.Close();

    }
    public void KupFr()
    {
        if (hvezdy - sch.prefs[schCislo].schopnost.cena >= 0)
        {
            hvezdy -= sch.prefs[schCislo].schopnost.cena;
            aktSchopnost = sch.prefs[schCislo].schopnost.nazev;
            textHvezdy.text = hvezdy + " ";
            upozorneni.SetActive(true);
            textKoupe.enabled = true;
            textNePenize.enabled = false;
        }
        else
        {
           upozorneni.SetActive(true);
           textKoupe.enabled = false;
           textNePenize.enabled = true;
        }
    }
    public void ZavriVyskak()
    {
        vyskakJizJe.SetActive(false);
        upozorneni.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            hvezdy++;
            textHvezdy.text = hvezdy + " ";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            hvezdy--;
            textHvezdy.text = hvezdy + " ";
        }


    }
    [Serializable]
    public struct HvezdyASchWrapper
    {
        public int hvezdy;
        public string aktSchopnost;
    }
}
