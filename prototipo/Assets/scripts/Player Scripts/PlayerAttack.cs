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
    float timer=1;
    // Update is called once per frame
    void Update()
    {
        if (timer>0)
        {
            timer -= Time.deltaTime;
        }
        if (Input.GetKeyDown("space")&& timer<0)
        {
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
                collider.transform.GetComponent<EnemyMove>().TakeEnemyDamage(hitDamage);
            }
        }
    }
    private void changeFlipX()
    {
        
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            float sizeX = transform.position.x + (radioStroke*2);
            controllerStroke.transform.position = new Vector3( sizeX, controllerStroke.transform.position.y, controllerStroke.transform.position.z);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            float sizeX = transform.position.x - (radioStroke*2);
            controllerStroke.transform.position = new Vector3(sizeX, controllerStroke.transform.position.y, controllerStroke.transform.position.z);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controllerStroke.position, radioStroke);
    }
}
