using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerHUD : MonoBehaviour
{
    public TMP_Text textPoint;
    public TMP_Text textHealth;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //Cambiar texto
        if (textPoint!=null)
        {
            textPoint.text = "Puntaje total=" + (ControllerSave.instance.point);
        }
        if (textHealth!=null)
        {
            textHealth.text = "Vidas=" + (ControllerSave.instance.life);
        }
        
    }
}
