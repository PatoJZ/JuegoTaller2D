using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Tiempos")]
    public GameObject Target;
    [Header("Medidas de la sala")]
    public Vector2[] minPosition;
    public Vector2[] maxPosition;
    public int i = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(Target.transform.position.x,Target.transform.position.y,transform.position.z);

        targetPosition.x =Mathf.Clamp(targetPosition.x,minPosition[i].x,maxPosition[i].x);
        targetPosition.y =Mathf.Clamp(targetPosition.y,minPosition[i].y,maxPosition[i].y);
        
        transform.position = new Vector3(targetPosition.x,targetPosition.y,targetPosition.z);
    }
    public void ChangeZone(int x)
    {
        i = x;
        Target.GetComponent<PlayerAttack>().zone = i;
    }
}
