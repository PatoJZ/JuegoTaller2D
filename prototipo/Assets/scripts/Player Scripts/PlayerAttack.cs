using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField] private Transform controllerStroke;
    [SerializeField] private float radioStroke;
    [SerializeField] private float hitDamage;
    [SerializeField] private float timeattack;
    [SerializeField] private float prueba;
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
                collider.transform.GetComponent<EnemyAttack>().TakeEnemyDamage(hitDamage, collider.transform.position);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controllerStroke.position, radioStroke);
    }
}
