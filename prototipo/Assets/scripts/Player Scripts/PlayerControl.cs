using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private Vector2 speedBounce;
    public bool canMove = true;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    public Vector2 savePlace;
    private Vector2 moveInput;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        savePlace=new Vector2(0,-1);
    }
    //cambiar de lado la escala
    void Animation(float x,float y)
    {
        playerAnimator.SetFloat("Horizontal", x);
        playerAnimator.SetFloat("Vertical", y);
    }
    public void Bounce(Vector2 pointHit)
    {
        playerRb.velocity = new Vector2(-speedBounce.x*pointHit.x,-speedBounce.y);
    }
    // Update is called once per frame
    void Update()
    {
        //Poner Imputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput= new Vector2(moveX,moveY).normalized;
        if (moveX!=0 || moveY!=0)
        {
            savePlace = new Vector2(moveX, moveY);
        }
        Animation(savePlace.x,savePlace.y);
    }
    private void FixedUpdate()
    {
        //Arreglar fisicas
        if (canMove)
        {
            playerRb.MovePosition(playerRb.position+moveInput*speed*Time.fixedDeltaTime);
        }
        
    }
    
}
