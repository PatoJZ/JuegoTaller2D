using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerSave : MonoBehaviour
{
    public static ControllerSave instance;
    [SerializeField] public float point;
    [SerializeField] public float life;
    // Start is called before the first frame update
    private void Awake()
    {
        if (ControllerSave.instance==null)
        {
            ControllerSave.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //saber la Vida
    public void KnowLife(float _life)
    {
        life =_life;
    }
    // agregar puntaje
    public void PlusPoint(float plusPoint)
    {
        point += plusPoint;
    }
    public void InitialPoint(float initialPoint)
    {
        point = initialPoint;
    }
}
