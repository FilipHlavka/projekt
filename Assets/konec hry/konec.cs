using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static VyberSch;

public class konec : MonoBehaviour
{
    [SerializeField]
    Canvas prohraCan;
    [SerializeField]
    Canvas vyhraCan;
    HvezdyASchWrapper wp;
    List<Casy> casList = new List<Casy>();

    CasyWrapper casy;
    // Start is called before the first frame update
    void Start()
    {
        prohraCan.enabled = false;
        vyhraCan.enabled = false;
        
        if (vyhra.prohra)
            prohraCan.enabled = true;
        else
        {
            vyhraCan.enabled = true;
            NactiAUloz();
            
        }


    }


    private void NactiAUloz()
    {
        BinaryFormatter formator = new BinaryFormatter();

        FileStream stream = File.Open(Application.dataPath + "/Hvezdy.bin", FileMode.Open);

        wp = (HvezdyASchWrapper)formator.Deserialize(stream);
        stream.Close();

        try
        {
            File.Delete(Application.dataPath + "/Hvezdy.bin");
        }
        catch
        {
            Debug.Log("sakra");
        }
        wp.hvezdy += 1; 
        stream = File.Create(Application.dataPath + "/Hvezdy.bin");
        formator.Serialize(stream, wp);
        stream.Close();

        if (!File.Exists(Application.dataPath + "/Casy.bin"))
            VytvorFile();

        FileStream streamCas = File.Open(Application.dataPath + "/Casy.bin", FileMode.Open);
        casy = (CasyWrapper)formator.Deserialize(streamCas);
        streamCas.Close();

        // casy.casy = casList;
        foreach (var i in casy.casy)
        {
            if(!i.fake)
            casList.Add(i);
        }
        Casy cas = new Casy();
       
         cas.tezky = obtiznost.instance.tezka;
         cas.time = pocitadlo.instance.timer;
         cas.mapa = cont.instance.mapa;
         casList.Add(cas);
        CasyWrapper cs = new CasyWrapper(casList);
        FileStream streamCasDva = File.Open(Application.dataPath + "/Casy.bin", FileMode.Open);
        formator.Serialize(streamCasDva, cs);
        Debug.Log(casy.casy);
        streamCasDva.Close();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void VytvorFile()
    { 
        BinaryFormatter formator = new BinaryFormatter();
        Casy cas = new Casy();
        cas.mapa = "";
        cas.time = 0;
        cas.tezky = false;
        cas.fake = true;
        casList.Add(cas);
        CasyWrapper cs = new CasyWrapper(casList);
        
        FileStream stream = File.Create(Application.dataPath + "/Casy.bin");
        formator.Serialize(stream, cs);
        stream.Close();
       

    }


    [Serializable]
    public class Casy
    {
        public bool fake = false;
        public float time;
        public bool tezky;
        public string mapa;
        public int id;
    }

    [Serializable]
    public class CasyWrapper
    {
        public List<Casy> casy;
        public CasyWrapper(List<Casy> list)
        {
            casy = list;
        }
    }
}
