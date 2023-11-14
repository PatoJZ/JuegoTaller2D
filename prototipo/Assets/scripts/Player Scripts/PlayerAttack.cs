using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    [Header("Hitbox Ataque")]
    [SerializeField] private Transform controllerStroke;
    [SerializeField] private float radioStroke;
    [SerializeField] public Transform controllerLimit;
    [SerializeField] public float radioLimit;
    [Header("Daño Ataque")]
    [SerializeField] public float hitDamageHacha;
    [SerializeField] public float hitDamageShovel;
    [SerializeField] public float hitDamage;
    [Header("Tiempos")]
    public float timePowerUp;
    [SerializeField] private float timeOutOfControl;
    [SerializeField] private float timeOutOfInvulnerability;
    [Header("PowerUp")]
    public float multiplyForce;
    public float multiplySpeed;
    public float multiplySpeedMove;
    public float multiplySpeedAttack;
    public enum Directions {HACHA,SHOVEL}
    public Directions weapon;
    
    private PlayerControl playerControl;
    private Animator playerAnimator;
    // Update is called once per frame
    private void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        playerAnimator = GetComponent<Animator>();
    }
    public void EndAttack()
    {
        playerAnimator.SetBool("Attack",false);
    }
    public void TakeDamage(float damage, Vector2 position)
    {
        animationIdle();
        playerControl.health -= damage;
        if (playerControl.health <= 0)
        {
            Dead();
        }
        else
        {
            //se le quitara control del player y dara invulnerabilidad
            StartCoroutine(OutOfControl());
            StartCoroutine(TimeOfInvulnerability());
            playerControl.Bounce(position);
            ControllerSave.instance.KnowLife(playerControl.health);
        }
    }
    private void animationIdle()
    {
        playerAnimator.SetBool("Attack", false);
        switch (weapon)
        {
            case Directions.HACHA:
                playerAnimator.SetTrigger("Idle");
                break;
            case Directions.SHOVEL:
                playerAnimator.SetTrigger("Idle Shovel");
                break;
        }
    }
    //se cargara la escena
    private void Dead()
    {
        SceneManager.LoadScene(2);
    }
    //se quitara el control del player
    private IEnumerator OutOfControl()
    {
        playerControl.canMove = false;
        yield return new WaitForSeconds(timeOutOfControl);
        playerControl.canMove = true;
    }
    //se le agregara invulnerabilidad del player
    private IEnumerator TimeOfInvulnerability()
    {
        Physics2D.IgnoreLayerCollision(7,6,true);
        yield return new WaitForSeconds(timeOutOfControl*5);
        Physics2D.IgnoreLayerCollision(7, 6,false);
    }
    private IEnumerator MoreForce()
    {
        hitDamage*=multiplyForce;
        yield return new WaitForSeconds(timePowerUp);
        hitDamage/=multiplyForce;
    }
    private IEnumerator MoreSpeed()
    {
        playerAnimator.SetFloat("MultiplySpeedMove", multiplySpeedMove);
        playerControl.speed *= multiplySpeed;
        yield return new WaitForSeconds(timePowerUp);
        playerControl.speed /= multiplySpeed;
        playerAnimator.SetFloat("MultiplySpeedMove", 1);
    }
    private IEnumerator MoreSpeedAttack()
    {
        playerAnimator.SetFloat("MultiplySpeedAttack", multiplySpeedAttack);
        yield return new WaitForSeconds(timePowerUp);
        playerAnimator.SetFloat("MultiplySpeedAttack", 1);
    }
    public void PutPowerUp(string powerUp)
    {

        if (powerUp=="Speed")
        {
            StartCoroutine(MoreSpeed());
        }else if (powerUp=="Force")
        {
            StartCoroutine(MoreForce());
        }else if (powerUp=="SpeedAttack")
        {
            StartCoroutine(MoreSpeedAttack());
        }
        
    }
    public void ChangeWeapon()
    {
        
        switch (weapon)
        {
            case Directions.HACHA:
                weapon = Directions.SHOVEL;
                playerAnimator.SetTrigger("Idle Shovel");
            break;    
            case Directions.SHOVEL:
                weapon = Directions.HACHA;
                playerAnimator.SetTrigger("Idle");
            break;
            default:
             break;

        }
        SetHitDamage();
    }
    public void SetHitDamage()
    {

        switch (weapon)
        {
            case Directions.HACHA:
                hitDamage = hitDamageHacha;
                break;
            case Directions.SHOVEL:
                hitDamage = hitDamageShovel;
                break;
            default:
                break;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controllerStroke.position, radioStroke);
        Gizmos.DrawWireSphere(controllerLimit.position, radioLimit);
    }
}
