using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPoint : MonoBehaviour
{
    public static ControllerPoint instance;
    [SerializeField] public float point;
    // Start is called before the first frame update
    private void Awake()
    {
        if (ControllerPoint.instance==null)
        {
            ControllerPoint.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlusPoint(float plusPoint)
    {
        point += plusPoint;
    }
    public void InitialPoint(float initialPoint)
    {
        point = initialPoint;
    }
}
