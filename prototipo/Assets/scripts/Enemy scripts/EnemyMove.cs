using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    
    public bool chase=true;
    public GameObject PlayerM;
    private Rigidbody2D enemyRb;
    [SerializeField] private Vector2 speedBounce;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb=GetComponent<Rigidbody2D>();
    }

    //funcion para quitar vida al enemigo
    
    public void Bounce(Vector2 pointHit)
    {
        enemyRb.velocity = new Vector2(-speedBounce.x * pointHit.x, -speedBounce.y);
    }
    void Update()
    {
        if (chase)
        {
            enemyRb.velocity = new Vector2(0,0);
            transform.position = Vector2.MoveTowards(transform.position,new Vector2(PlayerM.transform.position.x, PlayerM.transform.position.y) ,speed*Time.deltaTime);
        }
        //Debug.Log(chase);
    }
}
