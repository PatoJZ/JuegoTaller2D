using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    [Header("Movimientos")]
    [SerializeField] public float health = 3;
    private float maxHealt = 12;
    [SerializeField] public float speed = 3f;
    [SerializeField] private Vector2 speedBounce;
    public bool canMove = false;
    public bool canAttack = true;
    public bool invulnerability = false;
    [Header("Posicion Jugador")]
    public Vector2 savePlace;
    public Vector2 angulo_base;

    [Header("Codigo")]

    private AudioSource audioSource;
    public AudioClip Walk;

    [Header("Codigo")]
    public AudioClip sonidoNuevo;
    private List<KeyCode> codigoSecreto = new List<KeyCode> {
        KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };
    private List<KeyCode> entradasJugador = new List<KeyCode>();
    /// <summary>
    /// ///////////
    /// </summary>
    private Vector2 moveInput;
    private PlayerAttack playerAttack;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private Material material;
    private float timer;
    public BasicInteraction basicInteraction;
    void Start()
    {
        health = Mathf.Clamp(health, 2, 12);
        ControllerSave.instance.KnowLife(health);
        ControllerSave.instance.InitialPoint(0);
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        //Setear cosas
        playerAttack.SetHitDamage();
        savePlace=new Vector2(0,-1);
        material = GetComponent<Renderer>().material;
        audioSource = GetComponent<AudioSource>();

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
        if (!playerAnimator.GetBool("Attack") && Time.timeScale!=0)
        {
            if (moveX != 0 && canMove || moveY != 0 && canMove)
            {
                savePlace = new Vector2(moveX, moveY);
            }
        }
        if (canAttack && Time.timeScale != 0)
        {
            
            //primero se cambiara arma y despues se atacara
            if (Input.GetKeyDown("n") || Input.GetKeyDown("m"))
            {
                playerAttack.ChangeWeapon();
            }else if (Input.GetKeyDown("space")&& !playerAnimator.GetBool("Attack"))
            {
                canAttack = false;
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
    public void Regeneratelife()
    {
        if (maxHealt!=health)
        {
            timer += Time.deltaTime;
            if (timer>=playerAttack.timeHealhRecuperate)
            {
                health += 1;
                ControllerSave.instance.life = health;
                timer = playerAttack.timeHealhRecuperate-1;
            }
        } 
    }
    public void ResetTime()
    {
        timer = 0;
    } 
    public void CanAttack()
    {
        playerAnimator.SetBool("Attack", false);
        canAttack = true;
        
    }
    public void Bounce(Vector2 pointHit)
    {
        playerRb.velocity = new Vector2(-speedBounce.x*pointHit.x,-speedBounce.y*pointHit.y);
    }
    public void HitAnimation()
    {
        if (invulnerability)
        {
            // Reduce el temporizador de invulnerabilidad

            // Cambia el color del material mientras está en estado de invulnerabilidad
            material.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time * 5f, 1f));
        }
        else
        {
            material.color = Color.white;
        }
    }
    bool VerificationSequence()
    {
        if (entradasJugador.Count > codigoSecreto.Count)
        {
            // Elimina la primera entrada si la lista es más larga que la secuencia secreta
            entradasJugador.RemoveAt(0);
        }

        // Compara las listas
        for (int i = 0; i < entradasJugador.Count; i++)
        {
            if (entradasJugador[i] != codigoSecreto[i])
            {
                return false;
            }
        }

        // Si llega a este punto, la secuencia coincide
        if (entradasJugador.Count == codigoSecreto.Count)
        {
            // Realiza la acción secreta aquí
            ActivateSecretAction();

            // Limpia la lista de entradas para futuros intentos
            entradasJugador.Clear();
        }

        return true;
    }
    void ActivateSecretAction()
    {
        Debug.Log("activado");
        playerAttack.weaponSounds[1] = sonidoNuevo;
        playerAttack.weaponSound = playerAttack.weaponSounds[1];
        // Agrega aquí la lógica que deseas ejecutar al activar el código secreto
    }
    KeyCode GetLastKeyPressed()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.anyKeyDown)
            {
                entradasJugador.Add(GetLastKeyPressed());
            }

            // Verifica si la secuencia coincide hasta el momento
            if (!VerificationSequence())
            {
                // Restablece la lista si la secuencia es incorrecta
                entradasJugador.Clear();
            }
        }
        HitAnimation();
        Inputs();
        if (Time.timeScale!=0)
        {
            Animation(savePlace.x, savePlace.y);
        }
        angulo_base = new Vector2(transform.position.x-playerAttack.radioLimit, transform.position.y);
        Regeneratelife();
        // vector base = new vector2(miposicion.x - limitecirculo,x, lo mismo en y);
        // vector enemigo = new Vector2(miposicion.x - posicionenemiga.x, lo mismo en y);
        //Debug.Log("atacar es : "+canAttack);
        if (canMove && !audioSource.isPlaying && moveInput.magnitude > 0)
        {
            audioSource.clip = Walk;
            audioSource.Play();
        }
        if (!canMove || moveInput.magnitude == 0 || Time.timeScale == 0)
        {
            audioSource.Stop();
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
        if (canMove&& !audioSource.isPlaying&& moveInput.magnitude > 0)
        {
            audioSource.clip = Walk;
            audioSource.Play();
        }
        if (!canMove || moveInput.magnitude==0 || Time.timeScale==0)
        {
            audioSource.Stop();
        }
        if (Time.timeScale == 0)
        {
            Debug.Log("Tiempo detenido");
            Debug.Log(Time.timeScale);
        }

    }

}
