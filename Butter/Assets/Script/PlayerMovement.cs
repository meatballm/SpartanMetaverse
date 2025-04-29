using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    [SerializeField] private Transform graphicsTransform;
    [SerializeField] private float moveSpeed = 1f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("Animator not found!");
        if (rb == null)
            Debug.LogError("Rigidbody2D not found!");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = input * moveSpeed;
        Vector2 graphic = graphicsTransform.localScale;
        if (rb.velocity.x > 0f)
        {
            graphic.x = -1f;
        }
        else if (rb.velocity.x < 0f)
        {
            graphic.x = 1f;
        }
        graphicsTransform.localScale = graphic;
        if (rb.velocity != Vector2.zero)
        {
            animator.SetFloat("RunState", 0.55f);
        }
        else
        {
            animator.SetFloat("RunState", 0);
        }
    }
}
