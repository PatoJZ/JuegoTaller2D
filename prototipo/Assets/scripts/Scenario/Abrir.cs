using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrir : MonoBehaviour
{
    public float pointNecesary;

    // Update is called once per frame
    void Update()
    {
        if (ControllerSave.instance.point>=pointNecesary)
        {
            Destroy(gameObject);
        }
    }
}
