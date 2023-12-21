using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    
    public enum Directions { CUERPO , DISPARADOR , EMBESTIDA , EXPLOCION };
    [Header("Tipo de Enemigo")]
    public Directions typeOfEnemy;
    [Header("No modificar")]
    public bool chase=true;
    public bool stun = false;
    public bool tackle = false;
    public GameObject PlayerM;
    [Header("Velocidades")]
    public float distanciaDeteccion;
    [SerializeField] private Vector2 speedBounce;
    [SerializeField] private float speed;
    public float changeWay = 1;
    public float rotationSpeed = 45.0f; // Grados por segundos
    public float timer=0;
    [Header("Sonidos General")]
    private AudioSource audioSource;
    public AudioClip walk;
    [Header("Sonidos Rabano")]
    public AudioClip roll;
    [Header("Sonidos Zanahoria")]
    public AudioClip spin;
    private bool limit=false;
    private float angle = 0.0f;
    public Vector3 direction;
    public int zone;
    private Rigidbody2D enemyRb;
    private EnemyAttack enemyAttack;
    private Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        enemyAttack=GetComponent<EnemyAttack>();
        enemyAnimator = GetComponent<Animator>();
        enemyRb=GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Bounce(Vector2 pointHit)
    {
        enemyRb.velocity = new Vector2(-speedBounce.x * pointHit.x, -speedBounce.y*pointHit.y);
    }
    public void RestartTime()
    {
        timer = 0;
    }
    void Update()
    {
        if (ControllerSave.instance.life<=0)
        {
            chase = false;
        }
        if (PlayerM.GetComponent<PlayerAttack>().zone!=zone)
        {
            enemyAttack.DeadForZone();
        }
        enemyAnimator.SetBool("Chase",chase);
        if (stun&& !enemyAttack.isDead)
        {
            timer += Time.deltaTime;
            if (timer >= enemyAttack.timeOfStun)
            {
                timer = 0;
                
                chase = true;
                enemyAttack.AnimationIdleRadish();
                enemyAnimator.SetBool("AttackAnimation", false);
            }
        }
        if (chase && GetComponent<EnemyAttack>().health>0)
        {
            switch (typeOfEnemy)
            {
                case Directions.CUERPO:
                    enemyRb.velocity = new Vector2(0, 0);
                    FollowPlayer(true);
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = walk;
                        audioSource.Play();
                    }
                    if (PlayerM.transform.position.x >= transform.position.x)
                    {
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    float distanciaAlJugador = Vector3.Distance(transform.position, PlayerM.transform.position);
                    
                    if (distanciaAlJugador < distanciaDeteccion && !enemyAnimator.GetBool("Attacks"))
                    {
                        
                        // El jugador está lo suficientemente cerca, realiza las acciones necesarias.
                        enemyAnimator.SetBool("Attacks", true);
                        enemyAnimator.SetTrigger("Attack");

                    }
                    break;
                case Directions.DISPARADOR:
                    enemyRb.velocity = new Vector2(0, 0);
                    
                    if (PlayerM.transform.position.x >= transform.position.x)
                    {
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    if (!limit)
                    {
                        FollowPlayer(true);
                    }
                    else
                    {
                        timer += Time.deltaTime;
                        if (timer <= 2f)
                        {
                            angle += rotationSpeed * Time.deltaTime*changeWay;
                        }
                        else if (timer <= 2.5f)
                        {
                            enemyAnimator.SetTrigger("StartSpinning");
                        }
                        //x=circulo.position.x + radio del circulo*cos(angulo*rad);
                        float x = PlayerM.GetComponent<PlayerAttack>().controllerLimit.position.x+ 0.2f + PlayerM.GetComponent<PlayerAttack>().radioLimit * Mathf.Cos(angle * Mathf.Deg2Rad);
                        float y = PlayerM.GetComponent<PlayerAttack>().controllerLimit.position.y + 0.2f + PlayerM.GetComponent<PlayerAttack>().radioLimit * Mathf.Sin(angle * Mathf.Deg2Rad);
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(x, y), speed * Time.deltaTime);
                    }
                    break;
                case Directions.EMBESTIDA:
                    enemyRb.velocity = Vector2.zero;
                    if (!tackle && !audioSource.isPlaying)
                    {

                        audioSource.clip = walk;
                        audioSource.Play();
                    }
                    if (!tackle&& !stun)
                    {
                            
                            FollowPlayer(true);
                        
                        
                        if (PlayerM.transform.position.x >= transform.position.x)
                        {
                            gameObject.GetComponent<SpriteRenderer>().flipX = false;
                        }
                        else
                        {
                            gameObject.GetComponent<SpriteRenderer>().flipX = true;
                        }
                        
                    }
                    else
                    {
                        timer += Time.deltaTime;
                        // Mueve al enemigo en la dirección del jugador.
                        transform.position += direction * speed*2 * Time.deltaTime;
                        if (timer>=enemyAttack.timeOfCharge)
                        {
                            timer = 0;
                            enemyAnimator.SetBool("Rolling", false);
                            chase = false;
                            enemyAnimator.SetBool("AttackAnimation", false);
                        }
                    }
                    
                    break;
                case Directions.EXPLOCION:
                    enemyRb.velocity = Vector2.zero;
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = walk;
                        audioSource.Play();
                    }
                    if (PlayerM.transform.position.x >= transform.position.x)
                    {
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    FollowPlayer(true);

                    break;
            }
        }
        //sonidos
        
        switch (typeOfEnemy)
        {
            case Directions.CUERPO:
                if ((audioSource.isPlaying && !chase) || (audioSource.isPlaying && Time.timeScale == 0))
                {
                    audioSource.Stop();
                }
                break;
            case Directions.DISPARADOR:
                if (!enemyAnimator.GetBool("Spin"))
                {
                    audioSource.clip = walk;
                }
                else
                {
                    audioSource.clip = spin;
                }
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                if ((audioSource.isPlaying && !chase) || (audioSource.isPlaying && Time.timeScale == 0) )
                {
                    audioSource.Stop();
                }
                break;
            case Directions.EMBESTIDA:
                if (!tackle)
                {
                    audioSource.clip = walk;
                }
                else
                {

                    audioSource.clip = roll;
                    
                }
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                if ((audioSource.isPlaying && !chase) || (audioSource.isPlaying && Time.timeScale == 0)|| (audioSource.isPlaying && stun))
                {
                    audioSource.Stop();
                }
                break;
            case Directions.EXPLOCION:
                if ((audioSource.isPlaying && !chase) || (audioSource.isPlaying && Time.timeScale == 0))
                {
                    audioSource.Stop();
                }
                break;
        }
        
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (typeOfEnemy)
        {
            case Directions.CUERPO:
                if (!collision.gameObject.CompareTag("PJ")&& !collision.gameObject.CompareTag("Enemy"))
                {
                    EscapeObstacle(collision);
                }
                break;
            case Directions.DISPARADOR:
                if (!collision.gameObject.CompareTag("PJ")&& !collision.gameObject.CompareTag("Enemy"))
                {
                    EscapeObstacle(collision);
                }
                break;
            case Directions.EMBESTIDA:
                if (!collision.gameObject.CompareTag("PJ")&& !enemyAnimator.GetBool("AttackAnimation")&& !collision.gameObject.CompareTag("Enemy"))
                {
                    EscapeObstacle(collision);
                }
                break;
            case Directions.EXPLOCION:
                if (!collision.gameObject.CompareTag("PJ") && !enemyAnimator.GetBool("AttackAnimation")&& !collision.gameObject.CompareTag("Enemy"))
                {
                    EscapeObstacle(collision);
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (typeOfEnemy)
        {
            case Directions.CUERPO:
                
                break;
            case Directions.DISPARADOR:
                if (collision.CompareTag("Limit"))
                {
                    limit = true;
                    Vector2 relativeVector = transform.position - collision.transform.position;
                    angle = Mathf.Atan2(relativeVector.y, relativeVector.x) * Mathf.Rad2Deg;
                }
                break;
            case Directions.EMBESTIDA:
                if (collision.CompareTag("Limit")&& !enemyAnimator.GetBool("AttackAnimation")&& !stun)
                {
                    chase = false;
                    enemyAnimator.SetBool("AttackAnimation",true);
                    enemyAnimator.SetTrigger("StartRolling");
                }
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Limit"))
        {
            switch (typeOfEnemy)
            {
                case Directions.DISPARADOR:
                    limit = false;
                    break;
            }
        }
        
    }
    private void FollowPlayer(bool i)
    {
        if (i)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(PlayerM.transform.position.x, PlayerM.transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(PlayerM.transform.position.x, PlayerM.transform.position.y), -speed * Time.deltaTime);
        }
    }
    private void EscapeObstacle(Collision2D collision)
    {
        if (collision.gameObject.transform.position.x <= PlayerM.transform.position.x)
        {
            transform.position = new Vector3(transform.position.x + ((speed * Time.deltaTime) ), transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - ((speed * Time.deltaTime) ), transform.position.y, transform.position.z);
        }

        if (collision.gameObject.transform.position.y <= PlayerM.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + ((speed * Time.deltaTime) ), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - ((speed * Time.deltaTime) ), transform.position.z);
        }
    }
}
