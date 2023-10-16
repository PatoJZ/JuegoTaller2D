using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entidadPrefab; // Prefab de la entidad que deseas generar.
    public Transform jugador; // Referencia al transform del jugador.
    public float radioSpawn = 10f; // Radio en el cual se generará la entidad.
    public float tiempoEspera = 3f; // Tiempo de espera entre generaciones.
    private bool jugadorDentroDelRadio = false;
    private float tiempoUltimaGeneracion = 0f;

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioSpawn);
    }
    private void Update()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= radioSpawn)
        {
            jugadorDentroDelRadio = true;
        }
        else
        {
            jugadorDentroDelRadio = false;
        }

        if (jugadorDentroDelRadio && Time.time - tiempoUltimaGeneracion >= tiempoEspera)
        {
            GenerarEntidad();
            tiempoUltimaGeneracion = Time.time;
        }
    }
    private void GenerarEntidad()
    {
        Vector3 posicionSpawn = transform.position;
        Instantiate(entidadPrefab, posicionSpawn, Quaternion.identity);
    }
}
