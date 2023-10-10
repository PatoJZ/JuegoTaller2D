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
    // Update is called once per frame
    void Update()
    {
        //Poner Imputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput= new Vector2(moveX,moveY).normalized;
    }
    private void FixedUpdate()
    {
        //Arreglar fisicas
        playerRb.MovePosition(playerRb.position+moveInput*speed*Time.fixedDeltaTime);
    }
}
