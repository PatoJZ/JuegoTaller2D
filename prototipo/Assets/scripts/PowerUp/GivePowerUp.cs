using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePowerUp : MonoBehaviour
{
    public enum Directions { SPEED,FORCE}
    public Directions powerUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PJ"))
        {
            switch (powerUp)
            {
                case Directions.SPEED:
                    
                    StartCoroutine(collision.GetComponent<PlayerControl>().MoreSpeed());
                    Debug.Log("Speed");
                    break;
                case Directions.FORCE:
                    StartCoroutine(collision.GetComponent<PlayerAttack>().MoreForce());
                    break;
                default:
                    break;
                
            }
            
        }
        
    }
}
