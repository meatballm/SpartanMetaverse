using UnityEngine;
public class JumpPlayerController : MonoBehaviour
{
    public float maxChargeTime = 1.5f;
    public float maxJumpForce = 15f;
    public float minJumpForce = 1f;
    public float fixedAngle = 60f * Mathf.Deg2Rad;
    private Rigidbody2D rb;

    public Sprite emptySprite;   // 게이지 0%
    public Sprite mid1Sprite;     // 게이지 1~50%
    public Sprite mid2Sprite;     // 게이지 50~99%
    public Sprite fullSprite;    // 게이지 100%

    bool isGrounded = true;
    float chargeTimer = 0f;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = emptySprite;
    }

    void Update()
    {
        // ↔ 입력에 따라 바라보는 방향 전환
        float h = Input.GetAxisRaw("Horizontal");
        if (h < 0) sr.flipX = true;
        else if (h > 0) sr.flipX = false;
        if (isGrounded)
        {
            // 게이지 충전
            if (Input.GetButton("Jump"))
            {
                chargeTimer = Mathf.Min(chargeTimer + Time.deltaTime, maxChargeTime);
                float normalized = chargeTimer / maxChargeTime;
                JumpUIManager.Instance.UpdateChargeUI(chargeTimer / maxChargeTime);
                UpdateChargeSprite(normalized);
            }
            // 점프 실행
            if (Input.GetButtonUp("Jump"))
            {
                DoJump();
            }
        }
        else if (chargeTimer > 0f)
        {
            // 차지 취소 시 스프라이트도 초기화
            chargeTimer = 0f;
            JumpUIManager.Instance.UpdateChargeUI(0f);
            UpdateChargeSprite(0f);
        }
    }

    void DoJump()
    {
        UpdateChargeSprite(0f);
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
    private void UpdateChargeSprite(float normalized)
    {
        if (normalized <= 0f)
            sr.sprite = emptySprite;
        else if (normalized >= 1f)
            sr.sprite = fullSprite;
        else if (normalized <= 0.5f)
            sr.sprite = mid1Sprite;
        else
            sr.sprite = mid2Sprite;
    }
}
