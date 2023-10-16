using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyMove enemyMove;
    [SerializeField] private float health = 3;
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
    }
    private IEnumerator OutOfControl()
    {
        enemyMove.chase = false;
        yield return new WaitForSeconds(timeOutOfControl);
        enemyMove.chase = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
