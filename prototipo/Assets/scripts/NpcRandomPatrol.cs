using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcRandomPatrol : MonoBehaviour
{
    public float speed;
    public float minPatrolTime;
    public float maxPatrolTime;
    public float minWaitTime;
    public float maxWaitTime;

    Animator animator;
    Rigidbody2D rigidBody;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        direction = RandomDirection();
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        ContinueBehavior();
    }
    IEnumerator Patrol()
    {
        direction = RandomDirection();
        Animation();
        yield return new WaitForSeconds(Random.Range(minPatrolTime,maxPatrolTime));
        
        direction = Vector2.zero;
        Animation();
        yield return new WaitForSeconds(Random.Range(minPatrolTime,maxPatrolTime));

        StartCoroutine(Patrol());
    }
    private Vector2 RandomDirection()
    {
        int x = Random.Range(0,8);
        return x switch
        {
            0 => Vector2.up,
            1 => Vector2.down,
            2 => Vector2.left,
            3 => Vector2.right,
            4 => new Vector2(1,1),
            5 => new Vector2(1,-1),
            6 => new Vector2(-1,1),
            _ => new Vector2(-1,-1),
        };
    }
    private void Animation()
    {
        if (direction.magnitude != 0)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        else animator.Play("Idle");

        rigidBody.velocity = direction.normalized * speed;
    }
    public void StopBehavior()
    {
        StopAllCoroutines();
        direction = Vector2.zero;
        Animation();
    }
    public void ContinueBehavior()
    {
        StartCoroutine(Patrol());
    }
    public void FacePlayer(Vector2 playerFace)
    {
        animator.SetFloat("Horizontal", -playerFace.x);
        animator.SetFloat("Vertical", -playerFace.y);
        direction = Vector2.zero;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PJ"))
        {
            rigidBody.velocity = direction.normalized * speed;
            collision.rigidbody.velocity =Vector2.zero;
        }
    }
}
