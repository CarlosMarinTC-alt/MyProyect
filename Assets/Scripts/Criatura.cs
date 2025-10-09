using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Criatura : MonoBehaviour
{
    public Transform player;
    public float vel = 3f;              // velocidad de seguimiento
    public float followDistance = 2f;   // distancia mínima para dejar de acercarse

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator animator;
    private bool isMoving;
    private bool wasMoving;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        // Opcional: mejorar suavidad física
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        if (player == null) return;

        // --- Voltear sprite mirando al jugador ---
        // Solo cambiamos flipX si hay una diferencia apreciable en X para evitar parpadeos cuando están casi alineados
        float mx = player.position.x - transform.position.x;
        if (Mathf.Abs(mx) > 0.01f)
        {
            sr.flipX = mx > 0f; // si el jugador está a la izquierda, mirar a la izquierda
        }
        // --- Control del Animator según movimiento ---
        if (!isMoving)
        {
            // Nos aseguramos de pausar y llevar al frame 0 solo al momento de detenerse
            if (wasMoving)
            {
                animator.speed = 0f;
                var stateHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
                animator.Play(stateHash, 0, 0f); // rebobina al frame 0 una sola vez
            }
            else
            {
                animator.speed = 0f; // ya estaba quieto: mantener pausa
            }
        }
        else
        {
            // En movimiento: reproducir normalmente
            if (animator.speed == 0f) animator.speed = 1f;
        }

        // Actualiza memoria para el próximo frame
        wasMoving = isMoving;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Distancia actual
        Vector2 pos = rb.position;
        Vector2 target = player.position;
        float distance = Vector2.Distance(pos, target);

        // Mover solo si está fuera del radio (agregamos un pequeño margen para estabilidad)
        const float epsilon = 0.01f;
        if (distance > followDistance + epsilon)
        {
            Vector2 dir = (target - pos).normalized;
            Vector2 next = pos + dir * vel * Time.fixedDeltaTime;
            rb.MovePosition(next);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
