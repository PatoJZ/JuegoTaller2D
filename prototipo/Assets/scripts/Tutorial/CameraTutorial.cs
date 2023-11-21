using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTutorial : MonoBehaviour
{
    public Vector3[] positionCamera;
    public GameObject left;
    public GameObject right;
    public Vector3 saveposition;
    private Tutorial tutorial;    
    private int i=0;
    // Start is called before the first frame update
    void Start()
    {
        left.SetActive(false);
        transform.position = positionCamera[i];
        tutorial = FindObjectOfType<Tutorial>();
    }
    public void ChangeLeft()
    {
        if (i - 2 <= 0)
        {
            left.SetActive(false);
        }
        right.SetActive(true);
        i--;
        transform.position = positionCamera[i];
        tutorial.StopAllCoroutines();
        tutorial.StartCoroutine(tutorial.Down());
        tutorial.backGround.transform.position = saveposition;
        tutorial.Ax = 0;
        tutorial.Ay = (-1);
    }
    public void ChangeRight()
    {
        if (i + 2 >= positionCamera.Length)
        {
            right.SetActive(false);
        }
        left.SetActive(true);
        i++;
        transform.position = positionCamera[i];
        tutorial.StopAllCoroutines();
        tutorial.StartCoroutine(tutorial.HoeToShovel());
        tutorial.StartCoroutine(tutorial.SeeSignDown());
    }
}
