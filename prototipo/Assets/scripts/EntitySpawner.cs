using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public RoundManager roundManager;
    public GameObject entidadPrefab; // Prefab de la entidad que deseas generar.
    public GameObject Player;
    public float radioSpawn = 5f; // Radio en el cual se generar� la entidad.
    public float tiempoEspera = 3f; // Tiempo de espera entre generaciones.
    private bool jugadorDentroDelRadio = false;
    private float tiempoUltimaGeneracion = 0f;

    // Update is called once per frame
   
    public void OnDrawGizmos()
    {
        Vector3 jugadorPosition = GameObject.FindWithTag("PJ").transform.position;

        float distanciaAlJugador = Vector3.Distance(transform.position, jugadorPosition);

        if (distanciaAlJugador <= radioSpawn)
        {
           // Debug.Log("Est� dentro");
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
        Instantiate(entidadPrefab, posicionSpawn, Quaternion.identity);
        roundManager.EnemigoGenerado(); // Informa al RoundManager.
    }
    public void calculoRadioGeneracion()
    {
        // Obt�n la posici�n actual del jugador en tiempo de ejecuci�n.
        Vector3 jugadorPosition = GameObject.FindWithTag("PJ").transform.position;

        float distanciaAlJugador = Vector3.Distance(transform.position, jugadorPosition);

        if (distanciaAlJugador <= radioSpawn)
        {
            jugadorDentroDelRadio = true;
           // Debug.Log("Est� dentro");
        }
        else
        {
            jugadorDentroDelRadio = false;
        }

        if (jugadorDentroDelRadio && Time.time - tiempoUltimaGeneracion >= tiempoEspera)
        {
            StartCoroutine(GenerarEntidad());
            tiempoUltimaGeneracion = Time.time;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyMove>().PlayerM = Player;

        }
        
    }

}
