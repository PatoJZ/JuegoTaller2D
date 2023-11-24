using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerSave : MonoBehaviour
{
    public static ControllerSave instance;
    public enum Directions { HACHA, SHOVEL }
    public Directions weapon;
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
    public void KnowWeapon(int _weapon)
    {
        switch (_weapon)
        {
            case 1:
                weapon = Directions.HACHA;
                break;
            case 2:
                weapon = Directions.SHOVEL;
                break;
            case 3:

                break;
        }
    }
}
