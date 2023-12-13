using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EnemyAttack : MonoBehaviour
{
    public enum Directions { CUERPO, DISPARADOR, EMBESTIDA, EXPLOCION};
    public Directions typeOfEnemy;
    private EnemyMove enemyMove;
    [Header("Elementos enemigos")]
    [SerializeField] public float health = 3;
    [SerializeField] public float damageAttack;
    [Header("Tiempos")]
    [SerializeField] public float timeOutOfControl;
    [SerializeField] public float timeOfCharge;
    public RoundManager roundManager;
    private bool isDead=false;
    private Animator enemyAnimator;
    private Rigidbody2D rb;
    public GameObject bullet;
    public Light2D light;
    [Header("Sonidos")]
    public AudioClip damageRecive;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        
        if (light!=null)
        {
            light.intensity = 0;
        }
        
    }
    // quitar vida del enemigo donde se pide el daño y la posicion de enpuje
    public void TakeEnemyDamage(float damage, Vector2 position)
    {
            health -= damage;

            StartCoroutine(OutOfControl());
            enemyMove.Bounce(position);
            if (health <= 0)
            {
                isDead = true;
                ControllerSave.instance.point += 10;
                animationDead();
            }
    }
    public void animationDead()
    {
        enemyAnimator.SetTrigger("Dead");
        roundManager.EnemigoEliminado();
        FindObjectOfType<ControllerHUD>().enemyPoints++;
    }
    //Eliminacion del gameObject
    private void Dead()
    {
        Destroy(gameObject);
    }
    public void DeadForZone()
    {
        roundManager.EnemigoEliminadoPorZona();
        Destroy(gameObject);
    }
    //se cambia el booleano que deja avanzar al player 
    private IEnumerator OutOfControl()
    {
        switch (typeOfEnemy)
        {
            case Directions.EMBESTIDA:
                yield return new WaitForSeconds(timeOutOfControl);
                rb.velocity = Vector2.zero;
                break;
            default:
                enemyMove.chase = false;
                yield return new WaitForSeconds(timeOutOfControl);
                enemyMove.chase = true;
                rb.velocity = Vector2.zero;
                break;
        }
    }
    ///////******************Papa*************************///////
    public void AnimationIdle()
    {
        enemyAnimator.SetBool("Attacks",false);
        enemyAnimator.SetTrigger("Idle");
    }
    public void Emerge()
    {
        enemyAnimator.SetTrigger("Idle");
        enemyMove.chase = true;
    }
    private void AnimationDamagePotato()
    {
        enemyAnimator.SetTrigger("Damage");
    }
    ///////******************Rabano**********************///////
    public void StartRolling()
    {
        enemyMove.timer = 0;
        enemyMove.direction = new Vector3(enemyMove.PlayerM.transform.position.x - transform.position.x, enemyMove.PlayerM.transform.position.y - transform.position.y, transform.position.z);
        enemyMove.direction.Normalize();
        enemyMove.chase = true;
        enemyMove.tackle = true;
        enemyAnimator.SetBool("Rolling", true);
        //StartCoroutine(MoveRolling());
    }
    public void EndRolling()
    {
        enemyAnimator.SetBool("AttackAnimation", false);
        enemyMove.tackle = false;
        enemyMove.chase = true;
        enemyAnimator.SetTrigger("Idle");
    }
    public void Shock()
    {
        enemyAnimator.SetBool("Rolling", false);
        enemyMove.tackle = false;
        enemyAnimator.SetBool("Stun",true);
        StartCoroutine(EnemyStun());
    }
    private IEnumerator EnemyStun()
    {
        yield return new WaitForSeconds(2);
        enemyAnimator.SetBool("Stun", false);
        enemyMove.chase = true;
        enemyAnimator.SetBool("AttackAnimation", false);
        enemyAnimator.SetTrigger("Idle");
    }
    private void AnimationDamageRadish()
    {
        enemyAnimator.SetTrigger("Damage");
        StopAllCoroutines();
    }
    public void AnimationIdleRadish()
    {
        enemyAnimator.SetBool("Stun", false);
        enemyMove.chase = true;
        enemyMove.tackle = false;
        enemyAnimator.SetBool("AttackAnimation", false);
        enemyAnimator.SetTrigger("Idle");
    }
    ///////******************Zanahoria**********************///////
    public void StarSpinning()
    {
        enemyAnimator.SetBool("Spin",true);
        StartCoroutine(StopSpinning());
    }
    public IEnumerator StopSpinning()
    {
        yield return new WaitForSeconds(1);
        enemyAnimator.SetBool("Spin", false);       
    }
    public void Shoot()
    {
        Vector3 direction = new Vector3(enemyMove.PlayerM.transform.position.x - transform.position.x, enemyMove.PlayerM.transform.position.y - transform.position.y, transform.position.z);
        GameObject bulle= Instantiate(bullet,transform.position,transform.rotation);
        Rigidbody2D rbProyectil = bulle.GetComponent<Rigidbody2D>();
        rbProyectil.velocity = direction * 2f;
    }
    public void AnimationStun()
    {
        StartCoroutine(Stun());
    }
    public IEnumerator Stun()
    {
        enemyMove.chase = false;
        enemyAnimator.SetTrigger("Stun");
        yield return new WaitForSeconds(2);
        AnimationIdleCarrot();
        enemyMove.RestartTime();
        enemyMove.chase = true;
    }
    public void AnimationIdleCarrot()
    {
        enemyAnimator.SetTrigger("Idle");
    }
    private void AnimationDamageCarrot()
    {
        enemyAnimator.SetTrigger("Damage");
        StopAllCoroutines();
    }
    ///////******************Coliflor**********************///////
    public void AnimationIdleCauliflower()
    {
        enemyAnimator.SetTrigger("Idle");
        enemyMove.chase = true;
    }
    private void AnimationAttackCauliflower()
    {
        enemyAnimator.SetTrigger("Attack");
    }
    public void IncrimateLight()
    {
        light.intensity = 0.2f;
        light.shapeLightFalloffSize += 0.5f;
    }
    public void IncrimateLight2()
    {
        light.intensity += 0.2f;
        light.shapeLightFalloffSize += 0.5f;
    }
    private void AnimationDamageCauliflower()
    {
        light.intensity = 0;
        light.shapeLightFalloffSize = 0.5f;
        enemyAnimator.SetTrigger("Damage");
    }
    public void Explotion()
    {
        Instantiate(bullet,transform.position,transform.rotation);
        roundManager.EnemigoEliminado();
        FindObjectOfType<ControllerHUD>().enemyPoints++;
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        switch (typeOfEnemy)
        {
            default:
                if (collision.gameObject.CompareTag("PJ"))
                {
                    
                    collision.gameObject.GetComponent<PlayerAttack>().TakeDamage(damageAttack, collision.GetContact(0).normal);
                    ControllerSound.instance.ExecuteSound(damageRecive);
                }

                break;
            case Directions.EXPLOCION:
                if (collision.gameObject.CompareTag("PJ"))
                {
                    enemyMove.chase = false;
                    AnimationAttackCauliflower();
                }
                
                break;
        }
        switch (typeOfEnemy)
        {
            case Directions.EMBESTIDA:
                
                if (enemyAnimator.GetBool("AttackAnimation")&& !collision.gameObject.CompareTag("Enemy") && !isDead)
                {
                    enemyMove.timer = 0;
                    enemyMove.chase = false;
                    enemyAnimator.SetTrigger("Shock");
                }
                
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch (typeOfEnemy)
        {
            case Directions.CUERPO:
                if (collision.gameObject.CompareTag("Weapon") && !isDead)
                {
                    AnimationDamagePotato();
                    ControllerSound.instance.ExecuteSound(FindObjectOfType<PlayerAttack>().weaponSound);
                    TakeEnemyDamage(FindObjectOfType<PlayerAttack>().hitDamage, -FindObjectOfType<PlayerControl>().savePlace);
                    
                }
                break;
            case Directions.DISPARADOR:
                if (collision.gameObject.CompareTag("Weapon") && !isDead)
                {
                    AnimationDamageCarrot();
                    ControllerSound.instance.ExecuteSound(FindObjectOfType<PlayerAttack>().weaponSound);
                    TakeEnemyDamage(FindObjectOfType<PlayerAttack>().hitDamage, -FindObjectOfType<PlayerControl>().savePlace);
                    
                }
                break;
                
            case Directions.EMBESTIDA:
                if (collision.gameObject.CompareTag("Weapon")&& (enemyAnimator.GetBool("Stun"))&&!isDead)
                {
                    AnimationDamageRadish();
                    ControllerSound.instance.ExecuteSound(FindObjectOfType<PlayerAttack>().weaponSound);
                    TakeEnemyDamage(FindObjectOfType<PlayerAttack>().hitDamage, -FindObjectOfType<PlayerControl>().savePlace);
                    
                }
                break;
            case Directions.EXPLOCION:
                if (collision.gameObject.CompareTag("Weapon") && !isDead)
                {
                    AnimationDamageCauliflower();
                    ControllerSound.instance.ExecuteSound(FindObjectOfType<PlayerAttack>().weaponSound);
                    TakeEnemyDamage(FindObjectOfType<PlayerAttack>().hitDamage, -FindObjectOfType<PlayerControl>().savePlace);
                    
                }
                break;
        }
    }
}
