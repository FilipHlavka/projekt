using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    Dictionary<int, Casy> slovnik = new Dictionary<int, Casy>();
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
        foreach(var vec in casy.casy)
        {
           
           scoreObj obj = Instantiate(sc, Panel.transform);
            Debug.Log(vec.time + "   " + vec.tezky);
           obj.id = i;
           slovnik.Add(i,vec);
           i++;
        }
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
