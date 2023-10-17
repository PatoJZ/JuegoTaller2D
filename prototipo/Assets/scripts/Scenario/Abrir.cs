using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrir : MonoBehaviour
{
    public GameObject player;
    public float pointNecesary;

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.GetComponent<PlayerControl>().point>=pointNecesary)
        {
            Destroy(gameObject);
        }
    }
}
