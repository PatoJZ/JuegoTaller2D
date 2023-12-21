using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class RoundManager : MonoBehaviour
{
    public EntitySpawner[] spawners; // Array de objetos spawner en el mapa.
    public Transform jugador;

    public int rondaActual = 0; // Número de ronda actual.
    public int rondaFinal = 2;
    public int enemigosPorRonda = 2; // Cantidad inicial de enemigos por ronda.
    public int totalEnemigosRondaActual = 0; // Total de enemigos en la ronda actual.
    public int enemigosGenerados = 1; // Cantidad de enemigos generados en la ronda actual.
    public int enemigosEliminados = 0; // Cantidad de enemigos eliminados en la ronda actual.

    public float duracionParpadeo = 3f; // Duración total del parpadeo.
    public float intervaloParpadeo = 0.5f; // Intervalo entre cambios de color del parpadeo.

    public bool Generacion;
    public bool eliminar = false;
    public ControllerHUD controllerHUD;
    [Header("puertas")]
    public GameObject[] doorBlock;
    public int zone;
    [Header("Sonidos")]
    public AudioClip startRound;
    public AudioClip endRound;


    public void Start()
    {
        controllerHUD = FindObjectOfType<ControllerHUD>();
        Generacion = true;
        ComenzarNuevaRonda();
    }
    public void Update()
    {
        if (zone== FindObjectOfType<PlayerAttack>().zone)
        {
            foreach (GameObject a in doorBlock)
            {

                a.SetActive(true);
            }
            //Debug.Log(enemigosGenerados);
        }
        if (Generacion)
        {
            GenerarEnemigos();
        }
        if (enemigosGenerados >= enemigosPorRonda)
        {
            Generacion = false;
        }
        if (rondaActual>rondaFinal)
        {

            controllerHUD.DeleteRoundManagers();
        }
    }
    public void SoundStartRound()
    {
        ControllerSound.instance.ExecuteSound(startRound);
    }
    public void ComenzarNuevaRonda()
    {
        Generacion = true;
        rondaActual++;
        enemigosGenerados = 0;
        totalEnemigosRondaActual = enemigosPorRonda;
        enemigosEliminados = 0;
        GenerarEnemigos();
        if(rondaActual <= rondaFinal&&zone==FindObjectOfType<PlayerAttack>().zone)
        SoundStartRound();
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

        if (enemigosEliminados >= totalEnemigosRondaActual)
        {
            enemigosPorRonda += 2;
            ComenzarNuevaRonda();
        }
    }
    public void EnemigoEliminadoPorZona()
    {
        enemigosGenerados--;
        Generacion = true;
    }
    public void RemoverSpawner()
    {
        int i=0;
        ControllerSound.instance.ExecuteSound(endRound);
        foreach (EntitySpawner a in spawners)
        {
            spawners[i] = null;
            a.animationDead();
            i++;
        }
        Destroy(gameObject);
    }
}

