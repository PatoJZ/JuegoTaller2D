using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnowPoints : MonoBehaviour
{
    public TMP_Text Point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Point.text ="Puntaje: "+ControllerSave.instance.point;
    }
}
