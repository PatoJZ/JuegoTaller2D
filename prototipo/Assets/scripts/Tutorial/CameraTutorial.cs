using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraTutorial : MonoBehaviour
{
    public Vector3[] positionCamera;
    public GameObject left;
    public GameObject right;
    public GameObject start;
    public Vector3 saveposition;
    public TMP_Text txtLeft;
    public TMP_Text txtRight;
    public TMP_Text txtObjetive;
    private Tutorial tutorial;    
    private int i=0;
    // Start is called before the first frame update
    void Start()
    {
        left.SetActive(false);
        transform.position = positionCamera[i];
        tutorial = FindObjectOfType<Tutorial>();
        txtObjetive.enabled = false;
    }
    public void ChangeLeft()
    {
        if (i - 1 <= 0)
        {
            left.SetActive(false);
        }
        start.SetActive(false);
        right.SetActive(true);
        i--;
        transform.position = positionCamera[i];
        tutorial.StopAllCoroutines();
        switch (i)
        {
            case 0:
                txtLeft.text = "Moverse";
                txtRight.text = "       Atacar";
                tutorial.StartCoroutine(tutorial.Down());
                tutorial.backGround.transform.position = saveposition;
                tutorial.Ax = 0;
                tutorial.Ay = (-1);
                break;
            case 1:
                txtRight.enabled = true;
                txtObjetive.enabled = false;
                txtLeft.text = " Cambiar Arma";
                txtRight.text = "Leer Señal";
                tutorial.StartCoroutine(tutorial.HoeToShovel());
                tutorial.StartCoroutine(tutorial.SeeSignDown());
                break;
        }
    }
    public void ChangeRight()
    {
        if (i + 2 >= positionCamera.Length)
        {
            right.SetActive(false);
        }
        if (i + 2 == positionCamera.Length)
        {
            start.SetActive(true);
        }
        left.SetActive(true);
        i++;
        transform.position = positionCamera[i];
        tutorial.StopAllCoroutines();
        switch (i)
        {
            case 1:
                txtLeft.text = " Cambiar Arma";
                txtRight.text = "Leer Señal";
                tutorial.StartCoroutine(tutorial.HoeToShovel());
                tutorial.StartCoroutine(tutorial.SeeSignDown());
                break;
            case 2:
                txtLeft.text = " Pausar Juego";
                txtRight.enabled = false;
                txtObjetive.enabled = true;
                tutorial.pause.SetActive(false);
                tutorial.StartCoroutine(tutorial.PressEscActivate());
                break;
        }
    }
}
