using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    
    bool perseguir=true; 
    public GameObject PlayerM;
    [SerializeField] private float speed;
    [SerializeField] private float health= 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //funcion para quitar vida al enemigo
    public void TakeEnemyDamage(float damage)
    {
        health -= damage;
        if (health<=0)
        {
            Dead();
        }
    }
    private void Dead()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        if (perseguir)
        {
            transform.position = Vector2.MoveTowards(transform.position,new Vector2(PlayerM.transform.position.x, PlayerM.transform.position.y) ,speed*Time.deltaTime);
        }
        
    }
}
