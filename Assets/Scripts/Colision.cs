using UnityEngine;

public class Colision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Sigo chocando con: " + collision.gameObject.name);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Salí de la colisión con: " + collision.gameObject.name);
    }
}
