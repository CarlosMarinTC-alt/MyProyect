using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenario : MonoBehaviour
{
    [Header("Configuración de cambio de escena")]
    [SerializeField] private int indiceEscenario;   // Índice de la escena al que vas a cambiar
    public KeyCode teclaInteraccion = KeyCode.Space; // Tecla para interactuar

    [Header("Indicador de interacción")]
    public Sprite iconoSprite; // El sprite que mostrará el ícono (asígnalo desde el Inspector)
    public Vector3 offset = new Vector3(0f, 3.5f, 0f); // Posición arriba del Prota
    public Vector2 Posicion_pasillo = new Vector2(8.92f, 0.55f);
    public Vector2 Posicion_habitacion = new Vector2(0.27f, 0.52f);
    private bool puedeCambiar = false;
    private Transform protaTransform;
    private GameObject iconoInstanciado; // El sprite creado dinámicamente
    private Cambiar_Posicion scriptCambiar_Posicion; 

    void Update()
    {
        if (puedeCambiar && protaTransform != null)
        {
            // Mantiene el ícono flotando encima del Prota
            if (iconoInstanciado != null)
                iconoInstanciado.transform.position = protaTransform.position + offset;

            // Detecta si el jugador presiona la tecla para cambiar de escena
            if (Input.GetKeyDown(teclaInteraccion))
            {
                CambiarEscenario(indiceEscenario);
                switch (indiceEscenario)
                {
                    case 0:
                        protaTransform.transform.position = Posicion_habitacion;
                        break;
                    case 1:
                        protaTransform.transform.position = Posicion_pasillo;
                        break;
                    case 3:
                        protaTransform.transform.position = Posicion_habitacion;
                        break;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeCambiar = true;
            protaTransform = collision.transform;

            // --- Crear el ícono dinámicamente ---
            if (iconoSprite != null && iconoInstanciado == null)
            {
                iconoInstanciado = new GameObject("IconoInteraccion");
                var sr = iconoInstanciado.AddComponent<SpriteRenderer>();
                sr.sprite = iconoSprite;
                sr.sortingOrder = 10; // Para que se vea encima de todo
                iconoInstanciado.transform.position = protaTransform.position + offset;
            }

            Debug.Log("El jugador puede cambiar de escenario (presiona " + teclaInteraccion + ")");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeCambiar = false;

            // Destruir el ícono cuando el jugador se aleja
            if (iconoInstanciado != null)
            {
                Destroy(iconoInstanciado);
                iconoInstanciado = null;
            }

            Debug.Log("El jugador se alejó del punto de cambio.");
        }
    }

    public void CambiarEscenario(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
