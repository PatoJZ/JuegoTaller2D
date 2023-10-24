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
    [Header("Power Up")]
    public float timePowerUp;
    public float multiplySpeed;
    private Vector2 moveInput;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    BasicInteraction basicInteraction;
    void Start()
    {
        //ControllerSave.instance.KnowLife(health);
        //ControllerSave.instance.InitialPoint(0);
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        savePlace=new Vector2(0,-1);
    }
    //cambiar de lado la escala
    void Animation(float x,float y,float moveX,float moveY)
    {
        playerAnimator.SetFloat("Horizontal", x);
        playerAnimator.SetFloat("Vertical", y);
        if (moveX != 0 || moveY != 0)
        {
            playerAnimator.SetBool("Move", true);
        }
        else
        {
            playerAnimator.SetBool("Move", false);
        }
    }
    public IEnumerator MoreSpeed()
    {
        Debug.Log("funcion");
        speed *= multiplySpeed;
        yield return new WaitForSeconds(timePowerUp);
        speed /= multiplySpeed;
    }
    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
        if (moveX != 0 && canMove || moveY != 0 && canMove)
        {
            savePlace = new Vector2(moveX, moveY);
        }
        if (Input.GetKeyDown("e"))
        {
            if (basicInteraction!=null)
            {
                basicInteraction.Interact(savePlace,transform.position);
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
            Animation(savePlace.x, savePlace.y, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
