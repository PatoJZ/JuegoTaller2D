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
    public bool tackle = false;
    public GameObject PlayerM;
    [Header("Velocidades")]
    public float distanciaDeteccion;
    [SerializeField] private Vector2 speedBounce;
    [SerializeField] private float speed;
    public float rotationSpeed = 45.0f; // Grados por segundos
    private float timer=0;
    private bool limit=false;
    private float angle = 0.0f;
    public Vector3 direction;
    private Rigidbody2D enemyRb;
    private Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRb=GetComponent<Rigidbody2D>();
        StartCoroutine(Desactivated_colision());
    }

    public void Bounce(Vector2 pointHit)
    {
        enemyRb.velocity = new Vector2(-speedBounce.x * pointHit.x, -speedBounce.y*pointHit.y);
    }
    public IEnumerator Desactivated_colision()
    {
        Physics2D.IgnoreLayerCollision(8,6,false);
        yield return new WaitForSeconds(0.1f);
        Physics2D.IgnoreLayerCollision(8, 6, true);

    }
    public void RestartTime()
    {
        timer = 0;
    }
    void Update()
    {
        enemyAnimator.SetBool("Chase", chase);
        if (chase)
        {
            switch (typeOfEnemy)
            {
                case Directions.CUERPO:
                    enemyRb.velocity = new Vector2(0, 0);
                    FollowPlayer(true);

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
                        // Puedes llamar a funciones, activar comportamientos, etc.
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
                            angle += rotationSpeed * Time.deltaTime;
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

                    if (!tackle)
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
                        // Mueve al enemigo en la dirección del jugador.
                        transform.position += direction * speed*2 * Time.deltaTime;
                    }
                    break;
                case Directions.EXPLOCION:
                    enemyRb.velocity = Vector2.zero;
                    FollowPlayer(true);

                    break;
            }
            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (typeOfEnemy)
        {
            case Directions.CUERPO:
                if (!collision.gameObject.CompareTag("PJ"))
                {
                    EscapeObstacle(collision);
                }
                break;
            case Directions.DISPARADOR:
                if (!collision.gameObject.CompareTag("PJ"))
                {
                    EscapeObstacle(collision);
                }
                break;
            case Directions.EMBESTIDA:
                if (!collision.gameObject.CompareTag("PJ")&& !enemyAnimator.GetBool("AttackAnimation"))
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
                    Debug.Log("enter " + angle);
                }
                break;
            case Directions.EMBESTIDA:
                if (collision.CompareTag("Limit")&& !enemyAnimator.GetBool("AttackAnimation"))
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
        switch (typeOfEnemy)
        {
            case Directions.CUERPO:

                break;
            case Directions.DISPARADOR:
                if (collision.CompareTag("Limit"))
                {
                    limit = false;
                }
                break;
            case Directions.EMBESTIDA:

                break;
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
            transform.position = new Vector3(transform.position.x + ((speed * Time.deltaTime) / 2), transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - ((speed * Time.deltaTime) / 2), transform.position.y, transform.position.z);
        }

        if (collision.gameObject.transform.position.y <= PlayerM.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + ((speed * Time.deltaTime) / 2), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - ((speed * Time.deltaTime) / 2), transform.position.z);
        }
    }
}
