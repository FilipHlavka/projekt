using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nastaveni : MonoBehaviour
{


    public void NastavGr(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void FullScreen(bool je)
    {
        Screen.fullScreen = je;
    }

}
