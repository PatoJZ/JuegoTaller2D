using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class RoundManager : MonoBehaviour
{
    public EntitySpawner[] spawners; // Array de objetos spawner en el mapa.
    public Transform jugador;
    public TMP_Text rondasText; // TextMeshPro para mostrar el número de rondas.
    public TMP_Text enemigosText; // TextMeshPro para mostrar la cantidad de enemigos.

    public int rondaActual = 0; // Número de ronda actual.
    public int enemigosPorRonda = 2; // Cantidad inicial de enemigos por ronda.
    public int totalEnemigosRondaActual = 0; // Total de enemigos en la ronda actual.
    public static int enemigosGenerados = 1; // Cantidad de enemigos generados en la ronda actual.
    private int enemigosEliminados = 0; // Cantidad de enemigos eliminados en la ronda actual.

    public float duracionParpadeo = 3f; // Duración total del parpadeo.
    public float intervaloParpadeo = 0.5f; // Intervalo entre cambios de color del parpadeo.
    private Color colorOriginal;

    public bool Generacion;

    public void Start()
    {
        Generacion = true;
        colorOriginal = rondasText.color;
        ActualizarTextos();
        ComenzarNuevaRonda();
    }
    public void Update()
    {
        if (Generacion)
        {
            GenerarEnemigos();
        }
        Debug.Log(enemigosGenerados);
        if (enemigosGenerados >= enemigosPorRonda)
        {
            Generacion = false;
        }
    }
    public void ComenzarNuevaRonda()
    {
        StartCoroutine(ParpadearTextoRondaCompleta());
        Generacion = true;
        rondaActual++;
        enemigosGenerados = 0;
        totalEnemigosRondaActual = enemigosPorRonda;
        enemigosEliminados = 0;
        ActualizarTextos();
        GenerarEnemigos();
        Debug.Log("enemigo por ronda = " + (enemigosPorRonda - 1).ToString());
    }

    public void GenerarEnemigos()
    {
        if (enemigosGenerados < enemigosPorRonda)
        {
            foreach (var spawner in spawners)
            {
                spawner.calculoRadioGeneracion();
            }
        }
    }
    public void EnemigoGenerado()
    {
        enemigosGenerados++;
        Debug.Log("enemigo generado1 = " + enemigosGenerados.ToString());
        
    }

    public void EnemigoEliminado()
    {
        enemigosEliminados++;
        ActualizarTextos();

        if (enemigosEliminados >= totalEnemigosRondaActual)
        {
            enemigosPorRonda += 4;
            ComenzarNuevaRonda();
        }
    }
    public void EnemigoEliminadoPorZona()
    {
        enemigosGenerados--;
        Generacion = true;
    }

    private void ActualizarTextos()
    {
        rondasText.text = "Ronda: " + rondaActual.ToString();
        enemigosText.text = "Enemigos: " + enemigosEliminados + " / " + totalEnemigosRondaActual;
    }
    private IEnumerator ParpadearTextoRondaCompleta()
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionParpadeo)
        {
            rondasText.color = (rondasText.color == colorOriginal) ? Color.red : colorOriginal;
            tiempoTranscurrido += intervaloParpadeo;
            yield return new WaitForSeconds(intervaloParpadeo);
        }

        // Restaura el color original y avanza a la siguiente ronda.
        rondasText.color = colorOriginal;


    }
}

