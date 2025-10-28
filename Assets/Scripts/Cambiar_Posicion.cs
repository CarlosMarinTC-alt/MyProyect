using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambiar_Posicion : MonoBehaviour
{
    [SerializeField] private int indiceEscenario;   // Índice de la escena al que vas a cambiar
    public Vector3 offset = new Vector3(0f, 3.5f, 0f); // Posición arriba del Prota
    public Vector2 Posicion_pasillo = new Vector2(8.92f, 0.55f);
    public Vector2 Posicion_habitacion = new Vector2(0.27f, 0.52f);
    private Transform protaTransform;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
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