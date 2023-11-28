using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMission : MonoBehaviour
{
    public string name;
    public Sprite item;
    // Start is called before the first frame update
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PJ"))
        {
            collision.gameObject.GetComponent<PlayerAttack>().saveItem(name,item);
            Destroy(gameObject);
        }
    }
}
