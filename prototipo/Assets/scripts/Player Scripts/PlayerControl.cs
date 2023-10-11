using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    //cambiar de lado la escala
    void changeFlipX()
    {
        if (Input.GetAxisRaw("Horizontal")>0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetAxisRaw("Horizontal")<0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Poner Imputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput= new Vector2(moveX,moveY).normalized;
        changeFlipX();
        

    }
    private void FixedUpdate()
    {
        //Arreglar fisicas
        playerRb.MovePosition(playerRb.position+moveInput*speed*Time.fixedDeltaTime);
    }
    
}
