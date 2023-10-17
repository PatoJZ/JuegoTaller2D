using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Target;
    [SerializeField] Vector2 minPosition;
    [SerializeField] Vector2 maxPosition;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(Target.transform.position.x,Target.transform.position.y,transform.position.z);

        targetPosition.x =Mathf.Clamp(targetPosition.x,minPosition.x,maxPosition.x);
        targetPosition.y =Mathf.Clamp(targetPosition.y,minPosition.y,maxPosition.y);
        
        transform.position = new Vector3(targetPosition.x,targetPosition.y,targetPosition.z);
    }
}
