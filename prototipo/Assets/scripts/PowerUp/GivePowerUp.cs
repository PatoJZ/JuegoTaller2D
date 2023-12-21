using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePowerUp : MonoBehaviour
{
    public enum Directions { SPEED,FORCE,SPEEDFORCE}
    public Directions powerUp;
    public AudioClip itemRecolected;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PJ"))
        {
            switch (powerUp)
            {
                case Directions.SPEED:
                    
                    collision.GetComponent<PlayerAttack>().PutPowerUp("Speed");
                    break;
                case Directions.FORCE:
                    collision.GetComponent<PlayerAttack>().PutPowerUp("Force");
                    break;
                case Directions.SPEEDFORCE:
                    collision.GetComponent<PlayerAttack>().PutPowerUp("SpeedAttack");
                    break;
                default:
                    break;
                
            }
            ControllerSound.instance.ExecuteSound(itemRecolected);
            Destroy(gameObject);
        }
    }
}
