using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private EnemyMove enemyMove;
    [Header("Elementos enemigos")]
    [SerializeField] public float health = 3;
    [SerializeField] public float damageAttack; 
    [Header("Tiempos")]
    [SerializeField] private float timeOutOfControl;
    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    } 
    // quitar vida del enemigo donde se pide el daño y la posicion de enpuje
    public void TakeEnemyDamage(float damage, Vector2 position)
    {
        health -= damage;
        if (health <= 0)
        {
            Dead();
        }
        else
        {
            StartCoroutine(OutOfControl());
            enemyMove.Bounce(position);
        }
    }
    //Eliminacion del gameObject
    private void Dead()
    {
        Destroy(gameObject);
        FindObjectOfType<RoundManager>().EnemigoEliminado();
    }
    //se cambia el booleano que deja avanzar al player 
    private IEnumerator OutOfControl()
    {
        enemyMove.chase = false;
        yield return new WaitForSeconds(timeOutOfControl);
        enemyMove.chase = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PJ"))
        {
            collision.gameObject.GetComponent<PlayerAttack>().TakeDamage(damageAttack,collision.GetContact(0).normal);
            ControllerSave.instance.KnowLife(collision.gameObject.GetComponent<PlayerControl>().health);
        }
    }
}
