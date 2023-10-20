using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PutTXT : MonoBehaviour
{
    public TMP_Text textPoint;
    // Start is called before the first frame update
    void Start()
    {
        textPoint.text = "Puntaje total=" + (ControllerPoint.instance.point);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
