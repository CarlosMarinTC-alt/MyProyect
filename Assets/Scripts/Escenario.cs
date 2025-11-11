using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenario : MonoBehaviour
{
    [Header("Configuración de cambio de escena")]
    [SerializeField] private int indiceEscenario;
    public static int ultimoIndiceEscenario = 99; 

    public KeyCode teclaInteraccion = KeyCode.Space;

    [Header("Indicador de interacción")]
    public Sprite iconoSprite;
    public Vector3 offset = new Vector3(0f, 3.5f, 0f);
    public Vector2 Posicion_pasillo = new Vector2(8.92f, 0.55f);
    public Vector2 Posicion_habitacion = new Vector2(0.27f, 0.52f);
    private bool puedeCambiar = false;
    private Transform protaTransform;
    private GameObject iconoInstanciado;

    void Update()
    {
        if (puedeCambiar && protaTransform != null)
        {
            if (iconoInstanciado != null)
                iconoInstanciado.transform.position = protaTransform.position + offset;

            if (Input.GetKeyDown(teclaInteraccion))
            {
                ultimoIndiceEscenario = indiceEscenario;

                CambiarEscenario(indiceEscenario);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeCambiar = true;
            protaTransform = collision.transform;

            if (iconoSprite != null && iconoInstanciado == null)
            {
                iconoInstanciado = new GameObject("IconoInteraccion");
                var sr = iconoInstanciado.AddComponent<SpriteRenderer>();
                sr.sprite = iconoSprite;
                sr.sortingOrder = 10;
                iconoInstanciado.transform.position = protaTransform.position + offset;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeCambiar = false;

            if (iconoInstanciado != null)
            {
                Destroy(iconoInstanciado);
                iconoInstanciado = null;
            }
        }
    }

    public void CambiarEscenario(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
