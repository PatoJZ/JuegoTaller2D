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
    [Header("Cinematica")]
    public Vector2 cinematic;
    public float smoothing;
    public bool isCinematic;
    public float timerCinematic=0;
    public float timer=0;
    public GameObject arrow;
    public GameObject[] Npc;
    public Vector3[] npcPosition;
    private bool isTheSameZone=false;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(Target.transform.position.x,Target.transform.position.y,transform.position.z);
        if (!isCinematic)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition[i].x, maxPosition[i].x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition[i].y, maxPosition[i].y);

            transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        }
        else
        {
            timer += Time.unscaledDeltaTime;
            if (timer>=timerCinematic)
            {
                isCinematic = false;
                Time.timeScale = 1;
                Destroy(arrow);
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(cinematic.x, cinematic.y, transform.position.z),smoothing);

        }
    }
    public void StartCinematic(Vector2 cinema, GameObject a)
    {
        arrow = a;
        timer = 0;
        isCinematic = true;
        cinematic = cinema;
    }
    
    public void ChangeZone(int x)
    {
        
        if (i!=x)
        {
            isTheSameZone = false;
            for (int w = 0; w < Npc.Length; w++)
            {
                Npc[w].transform.position = npcPosition[w];
            }
        }
        else
        {
            isTheSameZone = true;
        }
        i = x;
        Target.GetComponent<PlayerAttack>().zone = i;
        int z = 0;
        foreach (int a in FindObjectOfType<ControllerHUD>().zone)
        {
            if (a == i)
            {
                FindObjectOfType<ControllerHUD>().indice = z;
            }
            z++;
        }
        if (FindObjectOfType<ControllerHUD>().roundsManager[FindObjectOfType<ControllerHUD>().indice] != null&& !isTheSameZone)
        {
            FindObjectOfType<ControllerHUD>().DefineRonda();
            FindObjectOfType<ControllerHUD>().ResetBarra();
        }
        
        
    }
}
