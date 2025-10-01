using UnityEngine;

public class Criatura : MonoBehaviour
{
    public Transform player;
    public float vel = 3f;
    public float followDistance = 2f;
    private Rigidbody2D rb;

    void Start()
    { rb = GetComponent<Rigidbody2D>(); }

    void FixedUpdate()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > followDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.MovePosition(rb.position + direction * vel * Time.fixedDeltaTime);
            }
        }
    }

}
