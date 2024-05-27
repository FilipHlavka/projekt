using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using static konec;
using static VyberSch;

public class scoreBoard : MonoBehaviour
{
    HvezdyASchWrapper wp;
    List<Casy> casList = new List<Casy>();

    CasyWrapper casy;
    [SerializeField]
    scoreObj sc;
    [SerializeField]
    GameObject Panel;
    [SerializeField]
    GameObject unseen;
    int pocetUnsean = 3;
    //s Dictionary<int, Casy> slovnik = new Dictionary<int, Casy>();
    // Start is called before the first frame update

    void Start()
    {
        Nacti();
    }

    // Update is called once per frame

    private void Nacti()
    {
        BinaryFormatter formator = new BinaryFormatter();


        if (!File.Exists(Application.dataPath + "/Casy.bin"))
            VytvorFile();

        FileStream streamCas = File.Open(Application.dataPath + "/Casy.bin", FileMode.Open);
        casy = (CasyWrapper)formator.Deserialize(streamCas);
        casList = casy.casy;
        int i = 0;
       
        foreach (var vec in casList)
        {

            scoreObj obj = Instantiate(sc, Panel.transform);
            Debug.Log(vec.time + "   " + vec.tezky);
            obj.id = i;
            vec.id = i;
            obj.eventOdstran.AddListener(odstran);
            aktText(vec, obj);
            i++;
        }
        for (int j = 0; j < Mathf.Abs(casList.Count - pocetUnsean - 1); j++)
        {
            Instantiate(unseen, Panel.transform);
        }
        streamCas.Close();
    }
    public void odstran(int id)
    {
        foreach (var vec in casList)
        {
               if(id == vec.id)
               {
                casList.Remove(vec);
                break;
               }
        }
    }
    public void Uloz()
    {
        
           // File.Delete(Application.dataPath + "/Casy.bin");
            BinaryFormatter formator = new BinaryFormatter();
            FileStream stream = File.Open(Application.dataPath + "/Casy.bin",FileMode.Open);
            CasyWrapper cs = new CasyWrapper(casList);
            formator.Serialize(stream, cs);
            stream.Close();
        
        
        
    }
    void aktText(Casy cas, scoreObj sc)
    {
        float minuty = Mathf.FloorToInt(cas.time / 60);
        float sec = Mathf.FloorToInt(cas.time % 60);
        string obtiznost;
        if (cas.tezky)
            obtiznost = " Hard ";
        else
            obtiznost = " Easy ";
        sc.mapa.text = cas.mapa;
        sc.obtiznost.text = obtiznost;
        sc.cas.text = minuty + ":" + sec;
    }

    void VytvorFile()
    {
        BinaryFormatter formator = new BinaryFormatter();
        Casy cas = new Casy();
        cas.mapa = "";
        cas.time = 0;
        cas.tezky = false;
        casList.Add(cas);
        CasyWrapper cs = new CasyWrapper(casList);
       
        FileStream stream = File.Create(Application.dataPath + "/Casy.bin");
        formator.Serialize(stream, cs);
        stream.Close();

    }
}
