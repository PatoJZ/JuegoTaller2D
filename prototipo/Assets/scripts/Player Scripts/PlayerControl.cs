using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public float health = 3;
    [SerializeField] private float speed = 3f;
    [SerializeField] public float point;
    [SerializeField] private Vector2 speedBounce;
    public TMP_Text textHealth;
    public TMP_Text textPoint;
    public bool canMove = false;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    public Vector2 savePlace;
    private Vector2 moveInput;

    void Start()
    {
        ControllerPoint.instance.InitialPoint(0);
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
        Debug.Log("bounmce: "+(new Vector2(-speedBounce.x * pointHit.x, -speedBounce.y * pointHit.y)));
        
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
        textHealth.text = "Salud= "+health;
        textPoint.text = "Puntos= " +point;
        Debug.Log(canMove);
    }
    private void FixedUpdate()
    {
        //Arreglar fisicas
        if (canMove)
        {
            Debug.Log(speedBounce);
            playerRb.velocity = new Vector2(0,0);
            playerRb.MovePosition(playerRb.position+moveInput*speed*Time.fixedDeltaTime);
        }
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
