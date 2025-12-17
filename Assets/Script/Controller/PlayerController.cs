using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("설정")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // [신형] Update에서 입력을 받는 게 아니라, 이 함수가 자동으로 호출됩니다.
    // 'Player Input' 컴포넌트가 'OnMove'라는 메시지를 보냅니다.
    void OnMove(InputValue value)
    {
        // 입력값(Vector2)을 읽어옵니다. (WASD가 자동으로 처리됨)
        movement = value.Get<Vector2>();

        // [애니메이션 처리] - 똑같음
        if (animator != null)
        {
            bool isMoving = movement.sqrMagnitude > 0;
            animator.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                animator.SetFloat("InputX", movement.x);
                animator.SetFloat("InputY", movement.y);
            }
        }
    }

    void FixedUpdate()
    {
        // 물리 이동은 똑같음
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
