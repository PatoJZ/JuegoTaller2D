using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField] private Transform controllerStroke;
    [SerializeField] private float radioStroke;
    [SerializeField] private float hitDamage;
    [SerializeField] private float timeattack;
    [SerializeField] private float timeOutOfControl;
    private PlayerControl playerControl;
    private Animator playerAnimator;
    float timer=1;
    // Update is called once per frame
    private void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (timer>0)
        {
            timer -= Time.deltaTime;
            //playerAnimator.SetBool("Attack", false);
        }
        else
        {
            playerAnimator.SetBool("Attack", false);
        }
        if (Input.GetKeyDown("space")&& timer<0)
        {
            Debug.Log("hola");
            playerAnimator.SetBool("Attack",true);
            hit();
            timer = timeattack;
        }
        changeFlipX();
    }
    public void hit()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(controllerStroke.position,radioStroke);
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (collider.transform.GetComponent<EnemyAttack>().health-hitDamage<=0)
                {
                    playerControl.point += 10;
                    ControllerPoint.instance.PlusPoint(10);
                    Debug.Log(ControllerPoint.instance.point);
                }
                collider.transform.GetComponent<EnemyAttack>().TakeEnemyDamage(hitDamage, -playerControl.savePlace);
            }
        }
    }
    private void changeFlipX()
    {
        BoxCollider2D boxcollider = GetComponent<BoxCollider2D>();
        float sizeX = transform.position.x;
        float sizeY= transform.position.y-(boxcollider.size.y/4);
        if (playerControl.savePlace.x> 0)
        {
            sizeX = transform.position.x + (boxcollider.size.x/2)+ radioStroke;
        }else if (playerControl.savePlace.x < 0)
        {
            sizeX = transform.position.x - (boxcollider.size.x / 2)- radioStroke;
        }
        if (playerControl.savePlace.y > 0)
        {
            sizeY = transform.position.y + (radioStroke)+ (boxcollider.size.y / 4);
        }else if (playerControl.savePlace.y < 0)
        {
            sizeY = transform.position.y - boxcollider.size.y- (boxcollider.size.y / 4);
        }
        
        controllerStroke.transform.position = new Vector3(sizeX, sizeY, controllerStroke.transform.position.z);
        
    }
    public void TakeDamage(float damage, Vector2 position)
    {
        playerControl.health -= damage;
        if (playerControl.health <= 0)
        {
            Dead();
        }
        else
        {
            StartCoroutine(OutOfControl());
            playerControl.Bounce(playerControl.savePlace);
        }
    }
    private void Dead()
    {
        SceneManager.LoadScene(2);
    }
    private IEnumerator OutOfControl()
    {
        playerControl.canMove = false;
        yield return new WaitForSeconds(timeOutOfControl);
        playerControl.canMove = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controllerStroke.position, radioStroke);
    }
}
