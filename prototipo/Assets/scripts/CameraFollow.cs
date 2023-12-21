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
    public Vector2[] resolutionAdd;
    private int indiceresolucion=0;

    // Update is called once per frame
    void Update()
    {
        
        Vector3 targetPosition = new Vector3(Target.transform.position.x,Target.transform.position.y,transform.position.z);
        if (!isCinematic)
        {
            Resolutions();
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition[i].x-resolutionAdd[indiceresolucion].x, maxPosition[i].x + resolutionAdd[indiceresolucion].x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition[i].y - resolutionAdd[indiceresolucion].y, maxPosition[i].y + resolutionAdd[indiceresolucion].y);

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
    public void Resolutions()
    {
        if (Screen.width==640&&Screen.height==480)
        {
            indiceresolucion = 0;
        }
        else if(Screen.width == 720 && Screen.height == 480)
        {
            indiceresolucion = 1;
        }
        else if (Screen.width == 720 && Screen.height == 576)
        {
            indiceresolucion = 2;
        }
        else if (Screen.width == 1024 && Screen.height == 768)
        {
            indiceresolucion = 3;
        }
        else if (Screen.width == 1366 && Screen.height == 768)
        {
            indiceresolucion = 4;
        }
        else if (Screen.width == 1600 && Screen.height == 900)
        {
            indiceresolucion = 5;
        }
        else
        {
            indiceresolucion = 6;
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
            FindObjectOfType<ControllerHUD>().roundsManager[FindObjectOfType<ControllerHUD>().indice].GetComponent<RoundManager>().SoundStartRound();
            FindObjectOfType<ControllerHUD>().DefineRonda();
            FindObjectOfType<ControllerHUD>().ResetBarra();
        }
        
        
    }
}
