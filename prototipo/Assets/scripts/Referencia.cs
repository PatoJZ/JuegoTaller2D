using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GeneradorEntidades : MonoBehaviour
{
    public Transform centroDelCirculo;
    public GameObject entidad1Prefab; // Entidad con 1 clic
    public GameObject entidad2Prefab; // Entidad con 2 clics
    public GameObject entidad3Prefab; // Entidad con 3 clics
    public Animator objetoDeReferenciaAnimator; // Asigna el Animator del objeto de referencia.
    public float radio = 5f;
    public float velocidadMovimiento = 2f;
    public TextMeshPro vidaText;
    public TextMeshPro tiempoText;

    public Material colorOriginalMaterial; // Asigna el material original del objeto de referencia en el Inspector.
    public Color colorGolpe = Color.red; // Color rojizo para el golpe.
    public float tiempoColorGolpe = 0.2f;
    private Renderer objetoDeReferenciaRenderer;
    private Color colorActual;

    private float intervaloCreacionInicial = 3f;
    private float velocidadMovimientoInicial = 2f;
    private int vidas = 3;
    private float tiempoEntreFases = 5f;
    private bool generacionEnProgreso = false;
    private float tiempoTranscurrido = 0f;



    private void Start()
    {
        StartCoroutine(GenerarEntidadesPorFases());

        objetoDeReferenciaRenderer = GetComponent<Renderer>();
        colorActual = objetoDeReferenciaRenderer.material.color;
    }


    private IEnumerator GenerarEntidadesPorFases()
    {
        while (vidas > 0)
        {
            yield return new WaitForSeconds(tiempoEntreFases);

            StartCoroutine(GenerarEntidadesPorFase(entidad1Prefab, 1, intervaloCreacionInicial, velocidadMovimientoInicial));
            yield return new WaitForSeconds(tiempoEntreFases);

            StartCoroutine(GenerarEntidadesPorFase(entidad2Prefab, 2, intervaloCreacionInicial * 0.75f, velocidadMovimientoInicial * 1.25f));
            yield return new WaitForSeconds(tiempoEntreFases);

            StartCoroutine(GenerarEntidadesPorFase(entidad1Prefab, 1, intervaloCreacionInicial * 0.5f, velocidadMovimientoInicial * 1.5f));
            StartCoroutine(GenerarEntidadesPorFase(entidad2Prefab, 2, intervaloCreacionInicial * 0.5f, velocidadMovimientoInicial * 1.5f));
            yield return new WaitForSeconds(tiempoEntreFases);

            StartCoroutine(GenerarEntidadesPorFase(entidad3Prefab, 3, intervaloCreacionInicial * 0.25f, velocidadMovimientoInicial * 2f));
        }
    }

    private IEnumerator GenerarEntidadesPorFase(GameObject entidadPrefab, int clics, float intervalo, float velocidad)
    {
        while (vidas > 0)
        {
            yield return new WaitForSeconds(intervalo);

            if (!generacionEnProgreso)
            {
                generacionEnProgreso = true;
                StartCoroutine(GenerarEnemigosSecuencialmente(entidadPrefab, clics, velocidad));
            }
        }
    }

    private IEnumerator GenerarEnemigosSecuencialmente(GameObject entidadPrefab, int clics, float velocidad)
    {
        // Número de enemigos a generar en esta fase.
        int cantidadEnemigos = Random.Range(3, 6); // Puedes ajustar el rango como desees.

        for (int i = 0; i < cantidadEnemigos; i++)
        {
            GenerarEntidad(entidadPrefab, clics, velocidad);
            yield return new WaitForSeconds(1f); // Intervalo entre la generación de enemigos.
        }

        generacionEnProgreso = false; // Marcar que la generación ha terminado.
    }
    private void GenerarEntidad(GameObject entidadPrefab, int clics, float velocidad)
    {
        float angulo = Random.Range(0f, 360f);
        Vector3 posicionInicial = centroDelCirculo.position + new Vector3(Mathf.Cos(angulo) * radio, Mathf.Sin(angulo) * radio, 0f);
        GameObject entidad = Instantiate(entidadPrefab, posicionInicial, Quaternion.identity);

        // Configura el componente "TextoClics" con la cantidad de clics.
        TextoClics textoClics = entidad.GetComponent<TextoClics>();
        if (textoClics != null)
        {
            textoClics.Inicializar(clics);
        }

        StartCoroutine(MoverHaciaCentro(entidad.transform, velocidad));
    }

    private IEnumerator MoverHaciaCentro(Transform entidadTransform, float velocidad)
    {
        Vector3 objetivo = centroDelCirculo.position;
        float distancia = Vector3.Distance(entidadTransform.position, objetivo);

        while (distancia > 0.1f)
        {
            entidadTransform.position = Vector3.MoveTowards(entidadTransform.position, objetivo, velocidad * Time.deltaTime);
            distancia = Vector3.Distance(entidadTransform.position, objetivo);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != centroDelCirculo.gameObject)
        {
            Destroy(other.gameObject);
            vidas--;
            objetoDeReferenciaRenderer.material.color = colorGolpe;
            StartCoroutine(RevertirColorDespuesDeTiempo(tiempoColorGolpe));
        }
    }

    private void Update()
    {
        if (vidas > 0)
        {
            tiempoTranscurrido += Time.deltaTime;
        }
        vidaText.text = "Vidas: " + vidas.ToString();
        if (vidas <= 0)
        {
            vidas = 0;
            AnimacionMuerte();
            StartCoroutine(LoadNewScene());

        }
        if (tiempoText != null)
        {
            tiempoText.text = "Tiempo: " + tiempoTranscurrido.ToString("F2"); // F2 para mostrar dos decimales.
        }
        if (Input.GetMouseButtonDown(0) && vidas > 0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPosition = new Vector2(mousePosition.x, mousePosition.y);

            Collider2D hitCollider = Physics2D.OverlapPoint(clickPosition);

            if (hitCollider != null)
            {
                TextoClics textoClics = hitCollider.gameObject.GetComponent<TextoClics>();

                if (textoClics != null)
                {
                    textoClics.RegistrarClic();
                    // Llama a la animación "Disparar" en el objeto de referencia.
                    if (textoClics != null)
                    {
                        DispararAnimacionDeReferencia(); // Activa la animación del objeto de referencia.
                    }
                }
                
            }

        }
    }

    private void DispararAnimacionDeReferencia()
    {
        if (objetoDeReferenciaAnimator != null)
        {
            objetoDeReferenciaAnimator.SetTrigger("shoot"); // Asume que el nombre del trigger de la animación de disparo es "Disparar".
            
        }
    }
    private void AnimacionMuerte()
    {
        if (objetoDeReferenciaAnimator != null)
        {
            objetoDeReferenciaAnimator.SetTrigger("death");
        }
      
    }
    public IEnumerator LoadNewScene()
    {
        PlayerPrefs.SetFloat("TiempoTranscurrido", tiempoTranscurrido); // Guarda el tiempo en PlayerPrefs.
        yield return new WaitForSeconds(5f); // Espera 3 segundos (ajusta según tus necesidades).
        SceneManager.LoadScene("final");
    }
    private IEnumerator RevertirColorDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);

        // Vuelve al color original.
        objetoDeReferenciaRenderer.material.color = colorActual;
    }
}
