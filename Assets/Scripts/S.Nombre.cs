using UnityEngine;

public class S_Nombre : MonoBehaviour
{
    public float vel = 3f;
    private Rigidbody2D rb;
    private Vector2 v2;

    void Start()
    { rb = GetComponent<Rigidbody2D>();}

    void Update()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");
        v2 = new Vector2(mx, my).normalized;

    }

    void FixedUpdate()
    {
        rb.linearVelocity = v2 * vel;
    }

}
