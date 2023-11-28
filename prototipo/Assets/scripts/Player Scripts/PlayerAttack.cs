using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    [Header("Hitbox Ataque")]
    
    [SerializeField] private Transform controllerStroke;
    [SerializeField] private Transform controllerStrokeTwo;
    [SerializeField] private float radioStroke;
    [SerializeField] public Transform controllerLimit;
    [SerializeField] public float radioLimit;
    
    [Header("Da�o Ataque")]
    
    [SerializeField] public float hitDamageHacha;
    [SerializeField] public float hitDamageShovel;
    [SerializeField] public float hitDamageTools;
    [SerializeField] public float hitDamage;
    
    [Header("Tiempos")]
    
    public float timePowerUp;
    public float timeHealhRecuperate;
    [SerializeField] private float timeOutOfControl;
    [SerializeField] public float timeOutOfInvulnerability;
    
    [Header("PowerUp")]
    
    public float multiplyForce;
    public float multiplySpeed;
    public float multiplySpeedMove;
    public float multiplySpeedAttack;
    public enum Directions {HOE,SHOVEL,TOOLS}
    public Directions weapon;

    private List<string> nameItem = new List<string>();
    private List<Sprite>  imageItem = new List<Sprite>();
    private float maxHealth = 12;
    private PlayerControl playerControl;
    private Animator playerAnimator;
    private ControllerHUD controllerHUD;
    // Update is called once per frame
    private void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        playerAnimator = GetComponent<Animator>();
        controllerHUD = FindObjectOfType<ControllerHUD>();
        controllerHUD.UpdateItem(imageItem);
    }
    public void EndAttack()
    {
        playerAnimator.SetBool("Attack",false);
    }
    public void TakeDamage(float damage, Vector2 position)
    {
        animationIdle();
        playerControl.health -= damage;
        StopCoroutine(StartHealthRecovery());
        //StopAllCoroutines();
        if (playerControl.health <= 0)
        {
            Dead();
        }
        else
        {
            //se le quitara control del player y dara invulnerabilidad
            StartCoroutine(StartHealthRecovery());
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
            case Directions.HOE:
                playerAnimator.SetTrigger("Idle");
                break;
            case Directions.SHOVEL:
                playerAnimator.SetTrigger("Idle Shovel");
                break;
            case Directions.TOOLS:
                playerAnimator.SetTrigger("Idle Tools");
                break;
        }
    }
    //se cargara la escena
    private void Dead()
    {
        SceneManager.LoadScene(2);
    }
    private IEnumerator StartHealthRecovery()
    {
        yield return new WaitForSeconds(timeHealhRecuperate);
        StartCoroutine(HealthRecovery());
    }
    private IEnumerator HealthRecovery()
    {
        if (maxHealth>playerControl.health)
        {
            playerControl.health += 1;
            ControllerSave.instance.life = playerControl.health;
        }
        else
        {
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(HealthRecovery());
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
        playerControl.invulnerability = true;
        yield return new WaitForSeconds(timeOutOfControl*5);
        playerControl.invulnerability = false;
        Physics2D.IgnoreLayerCollision(7, 6,false);
    }
    private IEnumerator MoreForce()
    {
        hitDamage*=multiplyForce;
        controllerHUD.ActivateForce(timePowerUp);
        yield return new WaitForSeconds(timePowerUp);
        hitDamage/=multiplyForce;
    }
    private IEnumerator MoreSpeed()
    {
        playerAnimator.SetFloat("MultiplySpeedMove", multiplySpeedMove);
        playerControl.speed *= multiplySpeed;
        controllerHUD.ActivateVelocity(timePowerUp);
        yield return new WaitForSeconds(timePowerUp);
        playerControl.speed /= multiplySpeed;
        playerAnimator.SetFloat("MultiplySpeedMove", 1);
    }
    private IEnumerator MoreSpeedAttack()
    {
        playerAnimator.SetFloat("MultiplySpeedAttack", multiplySpeedAttack);
        controllerHUD.ActivateAttackVelocity(timePowerUp);
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
        controllerHUD.ChangeWeapon(this);
        switch (weapon)
        {
            case Directions.HOE:
                if (Input.GetKeyDown("n"))
                {
                    weapon = Directions.TOOLS;
                    animationIdle();
                }
                else if (Input.GetKeyDown("m"))
                {
                    weapon = Directions.SHOVEL;
                    animationIdle();
                }
            break;    
            case Directions.SHOVEL:
                if (Input.GetKeyDown("n"))
                {
                    weapon = Directions.HOE;
                    animationIdle();
                }
                else if (Input.GetKeyDown("m"))
                {
                    weapon = Directions.TOOLS;
                    animationIdle();
                }
                break;
            case Directions.TOOLS:
                if (Input.GetKeyDown("n"))
                {
                    weapon = Directions.SHOVEL;
                    animationIdle();
                }
                else if (Input.GetKeyDown("m"))
                {
                    weapon = Directions.HOE;
                    animationIdle();
                }
                break;

        }
        SetHitDamage();
    }
    public void SetHitDamage()
    {

        switch (weapon)
        {
            case Directions.HOE:
                hitDamage = hitDamageHacha;
                break;
            case Directions.SHOVEL:
                hitDamage = hitDamageShovel;
                break;
            case Directions.TOOLS:
                hitDamage = hitDamageTools;
                break;
        }
    }
    public void saveItem(string a, Sprite b)
    {
        nameItem.Add(a);
        imageItem.Add(b);
        controllerHUD.UpdateItem(imageItem);
    }
    public bool isItem(string name,string keyName,Sprite items)
    {
        int i=0;
        foreach(string a in nameItem)
        {
            if (a==name || keyName==a)
            {
                nameItem[i] = keyName;
                imageItem[i] = items;
                controllerHUD.UpdateItem(imageItem);
                return true;
            }
            i++;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controllerStroke.position, radioStroke);
        Gizmos.DrawWireSphere(controllerLimit.position, radioLimit);
        Gizmos.DrawWireSphere(controllerStrokeTwo.position, radioStroke);
    }
}
