using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    void Update()
    {
        if (player != null)
        {
            // Mover hacia el jugador
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );

            // --- Voltear el sprite ---
            if (player.position.x > transform.position.x)
            {
                // Jugador está a la derecha
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (player.position.x < transform.position.x)
            {
                // Jugador está a la izquierda
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
