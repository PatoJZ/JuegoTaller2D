using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public GameObject puerta;
    public Vector2 cinematic;
    private bool inCinematic;
    private Material material;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!inCinematic)
        {
            if (puerta == null)
            {
                spriteRenderer.sortingOrder = 8;
                inCinematic = true;
                Time.timeScale = 0;
                FindObjectOfType<CameraFollow>().StartCinematic(cinematic,gameObject);
            }
            material.color = Color.white;
        }
        else
        {
            animator.speed = (Time.timeScale == 0) ? 0 : 1;
            material.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.unscaledTime * 2f, 1f));
        }
        
        
    }
}
