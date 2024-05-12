using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class nacteniSceny : MonoBehaviour
{
    string scena;
    Zavin strudl;
    public UnityEvent<Zavin> eventNacteni;
    private void Start()
    {
        if (eventNacteni == null)
            eventNacteni = new UnityEvent<Zavin>();
    }
    public void Nacti()
    {
        scena = SceneManager.GetActiveScene().name; ;
        BinaryFormatter formator = new BinaryFormatter();

        FileStream stream = File.Open(Application.dataPath + "/" + scena + ".bin", FileMode.Open);

        strudl = (Zavin)formator.Deserialize(stream);
        stream.Close();
       
       eventNacteni.Invoke(strudl);
    }
   
}
