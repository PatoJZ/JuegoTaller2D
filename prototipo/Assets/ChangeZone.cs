using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    private CameraFollow camera;
    public int zone;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<CameraFollow>();
    }

    // Update is called once per frame
    
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PJ"))
        {

            camera.ChangeZone(zone);
        }

    }
}
