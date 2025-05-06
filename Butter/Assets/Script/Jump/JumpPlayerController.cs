using UnityEngine;
public class JumpPlayerController : MonoBehaviour
{
    public float maxChargeTime = 1.5f;
    public float maxJumpForce = 15f;
    public float minJumpForce = 1f;
    public float fixedAngle = 60f * Mathf.Deg2Rad;
    private Rigidbody2D rb;

    bool isGrounded = true;
    float chargeTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded)
        {
            // 게이지 충전
            if (Input.GetButton("Jump"))
            {
                chargeTimer = Mathf.Min(chargeTimer + Time.deltaTime, maxChargeTime);
                JumpUIManager.Instance.UpdateChargeUI(chargeTimer / maxChargeTime);
            }
            // 점프 실행
            if (Input.GetButtonUp("Jump"))
            {
                DoJump();
            }
        }
    }

    void DoJump()
    {
        // 방향 결정
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 dir;
        if (h < 0) dir = new Vector2(-Mathf.Cos(fixedAngle), Mathf.Sin(fixedAngle));
        else if (h > 0) dir = new Vector2(Mathf.Cos(fixedAngle), Mathf.Sin(fixedAngle));
        else dir = Vector2.up;

        float force = (chargeTimer / maxChargeTime) * (maxJumpForce-minJumpForce) + minJumpForce;
        rb.velocity = Vector2.zero;
        rb.AddForce(dir.normalized * force, ForceMode2D.Impulse);

        chargeTimer = 0f;
        JumpUIManager.Instance.UpdateChargeUI(0);
        isGrounded = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fin"))
        {
            JumpGameManager.Instance.TriggerGameOver();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (rb.velocity.x < 1f && rb.velocity.y < 1f)
            isGrounded = true;
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (rb.velocity.x < 1f && rb.velocity.y < 1f)
            isGrounded = true;
    }
}
