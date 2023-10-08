using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GeneradorEntidades : MonoBehaviour
{
    public Transform centroDelCirculo;
    public GameObject entidad1Prefab; // Entidad con 1 clic
    public GameObject entidad2Prefab; // Entidad con 2 clics
    public GameObject entidad3Prefab; // Entidad con 3 clics
    public Animator objetoDeReferenciaAnimator; // Asigna el Animator del objeto de referencia.
    public float radio = 5f;
    public float intervaloCreacion = 1f; // Intervalo entre la creación de entidades.
    public float velocidadMovimiento = 2f;

    private int contadorEntidadesGeneradas = 0;

    private void Start()
    {
        StartCoroutine(GenerarEntidadesConIntervalo());
    }

    private IEnumerator GenerarEntidadesConIntervalo()
    {
        yield return new WaitForSeconds(1f); // Espera inicial de 1 segundo antes de comenzar la generación.

        for (int i = 0; i < 3; i++) // Genera un total de 6 entidades (1 de 1 clic, 2 de 2 clics y 3 de 3 clics).
        {
            if (i == 0)
            {
                GenerarEntidad(entidad1Prefab, 1);
            }
            else if (i == 1 || i == 2)
            {
                GenerarEntidad(entidad2Prefab, 2);
            }
            else
            {
                GenerarEntidad(entidad3Prefab, 3);
            }

            contadorEntidadesGeneradas++;
            yield return new WaitForSeconds(intervaloCreacion);
        }
    }

    private void GenerarEntidad(GameObject entidadPrefab, int clics)
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

        StartCoroutine(MoverHaciaCentro(entidad.transform));
    }

    private IEnumerator MoverHaciaCentro(Transform entidadTransform)
    {
        Vector3 objetivo = centroDelCirculo.position;
        float distancia = Vector3.Distance(entidadTransform.position, objetivo);

        while (distancia > 0.1f)
        {
            entidadTransform.position = Vector3.MoveTowards(entidadTransform.position, objetivo, velocidadMovimiento * Time.deltaTime);
            distancia = Vector3.Distance(entidadTransform.position, objetivo);
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != centroDelCirculo.gameObject)
        {
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                        textoClics.RegistrarClic();
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
}
