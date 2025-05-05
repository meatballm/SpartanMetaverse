using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    [SerializeField] private Transform graphicsTransform;
    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 20f;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        this.transform.position = GameManager.Instance.playerlocation;
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
        //가속 관련 코드
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector2 targetVelocity = input * moveSpeed;
        Vector2 velocityDiff = targetVelocity - rb.velocity;

        float accelX = Mathf.Abs(targetVelocity.x) > 0.01f ? acceleration : deceleration;
        float accelY = Mathf.Abs(targetVelocity.y) > 0.01f ? acceleration : deceleration;

        Vector2 force = new Vector2(velocityDiff.x * accelX, velocityDiff.y * accelY);

        rb.AddForce(force);

        //애니메이터 코드
        if (input != Vector2.zero) animator.SetBool("isRun", true);
        else animator.SetBool("isRun", false);

        //좌우반전 코드
        Vector2 graphic = graphicsTransform.localScale;
        if (rb.velocity.x > 0f)
        {
            graphic.x = 1f;
        }
        else if (rb.velocity.x < 0f)
        {
            graphic.x = -1f;
        }
        graphicsTransform.localScale = graphic;
    }
}
