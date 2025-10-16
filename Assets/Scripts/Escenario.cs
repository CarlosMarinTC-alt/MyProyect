using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenario : MonoBehaviour
{
    [Header("Configuración de cambio de escena")]
    public int indiceEscenario;   // Índice de la escena al que vas a cambiar
    public KeyCode teclaInteraccion = KeyCode.Space; // Tecla para interactuar (puedes cambiarla)

    private bool puedeCambiar = false; // Indica si el Prota está cerca

    void Update()
    {
        // Solo cambiar si el Prota está en rango y presiona la tecla
        if (puedeCambiar && Input.GetKeyDown(teclaInteraccion))
        {
            CambiarEscenario(indiceEscenario);
        }
    }

    // Detecta cuando el jugador entra en el área del portal
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeCambiar = true;
            Debug.Log("El jugador puede cambiar de escenario (presiona " + teclaInteraccion + ")");
        }
    }

    // Detecta cuando el jugador sale del área
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeCambiar = false;
            Debug.Log("El jugador se alejó del punto de cambio.");
        }
    }

    public void CambiarEscenario(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
