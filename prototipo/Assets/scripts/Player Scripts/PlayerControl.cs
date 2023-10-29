using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    [Header("Movimientos")]
    [SerializeField] public float health = 3;
    [SerializeField] public float speed = 3f;
    [SerializeField] private Vector2 speedBounce;
    public bool canMove = false;
    [Header("Posicion Jugador")]
    public Vector2 savePlace;
    private Vector2 moveInput;
    private PlayerAttack playerAttack;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    BasicInteraction basicInteraction;
    void Start()
    {
        //ControllerSave.instance.KnowLife(health);
        //ControllerSave.instance.InitialPoint(0);
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        //Setear cosas
        playerAttack.SetHitDamage();
        savePlace=new Vector2(0,-1);
    }
    //cambiar de lado la escala
    void Animation(float x,float y)
    {
        if (!playerAnimator.GetBool("Attack"))
        {
            playerAnimator.SetFloat("Horizontal", x);
            playerAnimator.SetFloat("Vertical", y);
        }
        else
        {
            playerAnimator.SetFloat("Horizontal", savePlace.x);
            playerAnimator.SetFloat("Vertical", savePlace.y);
        }
        
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            playerAnimator.SetBool("Move", true);
        }
        else
        {
            playerAnimator.SetBool("Move", false);
        }
    }
    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if (!playerAnimator.GetBool("Attack"))
        {
            moveInput = new Vector2(moveX, moveY).normalized;
        }
        else
        {
            moveInput = Vector2.zero;
        }
        if (!playerAnimator.GetBool("Attack"))
        {
            if (moveX != 0 && canMove || moveY != 0 && canMove)
            {
                savePlace = new Vector2(moveX, moveY);
            }
        }
        if (!playerAnimator.GetBool("Attack") && Time.timeScale != 0)
        {
            //primero se cambiara arma y despues se atacara
            if (Input.GetKeyDown("c"))
            {
                playerAttack.ChangeWeapon();
            }else if (Input.GetKeyDown("space"))
            {
                playerAnimator.SetBool("Attack", true);
            }
        }
        //para interectuar con dialogos o carteles
        if (Input.GetKeyDown("e"))
        {
            if (basicInteraction != null)
            {
                basicInteraction.Interact(savePlace, transform.position);
            }
        }

    }
    public void Bounce(Vector2 pointHit)
    {
        playerRb.velocity = new Vector2(-speedBounce.x*pointHit.x,-speedBounce.y*pointHit.y);
    }
    // Update is called once per frame
    void Update()
    {
        Inputs();
        if (Time.timeScale!=0)
        {
            Animation(savePlace.x, savePlace.y);
        }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            basicInteraction = collision.GetComponent<BasicInteraction>();
            Debug.Log("hola");
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            basicInteraction = null;
        }
    }

}
