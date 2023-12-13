using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public RoundManager roundManager;
    public GameObject entidadPrefab; // Prefab de la entidad que deseas generar.
    public GameObject Player;
    public float radioSpawn = 5f; // Radio en el cual se generará la entidad.
    public float tiempoEspera = 3f; // Tiempo de espera entre generaciones.
    public int zone;
    private bool jugadorDentroDelRadio = false;
    private float tiempoUltimaGeneracion = 0f;
    private Animator animator;

    // Update is called once per frame

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnDrawGizmos()
    {
        Vector3 jugadorPosition = GameObject.FindWithTag("PJ").transform.position;

        float distanciaAlJugador = Vector3.Distance(transform.position, jugadorPosition);

        if (distanciaAlJugador <= radioSpawn)
        {
           // Debug.Log("Está dentro");
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireSphere(transform.position, radioSpawn);
    }
    
    public IEnumerator GenerarEntidad()
    {
        yield return new WaitForSeconds(3f);
        Vector3 posicionSpawn = transform.position;
        Instantiate(entidadPrefab, new Vector3 (posicionSpawn.x, posicionSpawn.y+1, posicionSpawn.z), Quaternion.identity);
        //roundManager.EnemigoGenerado(); // Informa al RoundManager.
    }
    public void calculoRadioGeneracion()
    {
        // Obtén la posición actual del jugador en tiempo de ejecución.
        Vector3 jugadorPosition = GameObject.FindWithTag("PJ").transform.position;

        float distanciaAlJugador = Vector3.Distance(transform.position, jugadorPosition);

        if (distanciaAlJugador <= radioSpawn)
        {
            jugadorDentroDelRadio = true;
           // Debug.Log("Está dentro");
        }
        else
        {
            jugadorDentroDelRadio = false;
        }

        if (jugadorDentroDelRadio && Time.time - tiempoUltimaGeneracion >= tiempoEspera)
        {
            StartCoroutine(GenerarEntidad());
            roundManager.EnemigoGenerado();
            tiempoUltimaGeneracion = Time.time;
        }
    }
    public void animationDead()
    {
        animator.SetTrigger("Destroy");
    }
    public void Destruir()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyMove>().PlayerM = Player;
            Debug.Log("");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyMove>().PlayerM = Player;
            collision.gameObject.GetComponent<EnemyMove>().zone = zone;
            collision.gameObject.GetComponent<EnemyAttack>().roundManager = roundManager;
        }
    }

}
