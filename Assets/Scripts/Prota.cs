using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Prota : MonoBehaviour
{
    public float vel = 3f;
    private Rigidbody2D rb;
    private Vector2 v2;
    private SpriteRenderer sr;
    public RuntimeAnimatorController spriteUp;
    public RuntimeAnimatorController spriteDown;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");
        v2 = new Vector2(mx, my).normalized;

        if (Interactuar.quitarMovimiento)
        {
            rb.linearVelocity = Vector2.zero;
            animator.speed = 0f;

            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, 
                          animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            return; 
        }

        if (mx != 0) 
        {
            sr.flipX = mx < 0;
        }
        
        if (my > 0.1f)
        {
            animator.runtimeAnimatorController = spriteUp;
        }
        else if (my < -0.1f)
        {
            animator.runtimeAnimatorController = spriteDown;
        }
        if (mx == 0 && my == 0)
        {
            animator.speed = 0f;
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, 0f);
        }
        else
        {
            animator.speed = 1f;
        }
    }

    void FixedUpdate()
    {   
        if(Interactuar.quitarMovimiento == false)
        {
            rb.linearVelocity = v2 * vel;
        }
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }
}
