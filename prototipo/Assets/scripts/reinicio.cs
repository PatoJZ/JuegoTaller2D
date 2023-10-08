using UnityEngine;
using TMPro;

public class PantallaReinicio : MonoBehaviour
{
    public TextMeshPro tiempoReinicioText;

    private void Start()
    {
        float tiempoGuardado = PlayerPrefs.GetFloat("TiempoTranscurrido", 0f);
        tiempoReinicioText.text = "Tiempo logrado: " + tiempoGuardado.ToString("F2"); // F2 para mostrar dos decimales.
    }

    // Resto del código de la pantalla de reinicio...
}