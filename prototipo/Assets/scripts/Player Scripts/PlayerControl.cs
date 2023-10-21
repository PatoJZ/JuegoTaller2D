using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    [Header("Movimientos")]
    [SerializeField] public float health = 3;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Vector2 speedBounce;
    public bool canMove = false;
    [Header("Posicion Jugador")]
    public Vector2 savePlace;
    private Vector2 moveInput;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;

    void Start()
    {
        ControllerSave.instance.KnowLife(health);
        ControllerSave.instance.InitialPoint(0);
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
        playerRb.velocity = new Vector2(-speedBounce.x*pointHit.x,-speedBounce.y*pointHit.y);
    }
    // Update is called once per frame
    void Update()
    {
        //Poner Imputs
        // el Input.GetAxisRaw("Horizontal") entrega entre un 1,-1 y 0 apretando las teclas a o d 
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput= new Vector2(moveX,moveY).normalized;
        if (moveX!=0&& canMove || moveY!=0 && canMove)
        {
            savePlace = new Vector2(moveX, moveY);
        }
        //se escoge la animacion
        Animation(savePlace.x,savePlace.y);
    }
    private void FixedUpdate()
    {
        //Arreglar fisicas
        if (canMove)
        {
            // se anula la velocidad de rebote
            playerRb.velocity = new Vector2(0,0);
            playerRb.MovePosition(playerRb.position+moveInput*speed*Time.fixedDeltaTime);
        }
        
        
    }

}
