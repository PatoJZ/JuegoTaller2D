using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoClics : MonoBehaviour
{
    public TextMeshPro textoMesh; // Asigna el componente TextMesh en el Inspector.
    public int clicsRestantes = 3; // Clics iniciales antes de destruir el objeto.

    private void Start()
    {
        ActualizarTexto();
    }
    public void Inicializar(int clics)
    {
        clicsRestantes = clics;
        ActualizarTexto();
    }
    public void RegistrarClic()
    {
        clicsRestantes--;

        if (clicsRestantes <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            ActualizarTexto();
        }
    }

    private void ActualizarTexto()
    {
        if (textoMesh != null)
        {
            textoMesh.text = clicsRestantes.ToString();
        }
    }

    // Actualiza la posición del TextMesh con respecto al objeto.
    private void Update()
    {
        if (textoMesh != null)
        {
            textoMesh.transform.position = transform.position + new Vector3(0f, 1f, 0f); // Ajusta la posición vertical según tus necesidades.
        }
    }
}
