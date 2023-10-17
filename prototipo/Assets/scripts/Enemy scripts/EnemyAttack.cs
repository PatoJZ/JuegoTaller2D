using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyMove enemyMove;
    [SerializeField] public float health = 3;
    [SerializeField] private float timeOutOfControl;
    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }
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
    private void Dead()
    {
        Destroy(gameObject);
        FindObjectOfType<RoundManager>().EnemigoEliminado();
    }
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
            collision.gameObject.GetComponent<PlayerAttack>().TakeDamage(1,collision.GetContact(0).normal);
            Debug.Log(collision.GetContact(0).normal);
        }
    }
}
