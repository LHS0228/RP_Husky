using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("설정")]
    public float moveSpeed = 5f;
    public float interactDistance = 1.0f; // 감지 거리 (1칸 정도)
    public LayerMask interactLayer;       // 감지할 레이어 (NPC, 사물 등)

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDir = Vector2.down; // 마지막으로 바라본 방향 (기본: 아래)
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // [이동 입력]
    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (animator)
        {
            // 움직이고 있다면, 바라보는 방향을 갱신
            if (movement.sqrMagnitude > 0)
            {
                lastDir = movement.normalized;

                // 애니메이터에도 전달 (가만히 있을 때도 방향 유지하기 위함)
                animator.SetFloat("InputX", lastDir.x);
                animator.SetFloat("InputY", lastDir.y);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    // [추가됨] 상호작용 키(E)를 눌렀을 때 호출
    void OnInteract(InputValue value)
    {
        Debug.Log("누름");
        if (value.isPressed)
        {
            Debug.Log("ㅇㅇ");
            DetectObject();
        }
    }

    // [핵심] 앞에 뭐가 있는지 확인하는 함수
    void DetectObject()
    {
        // 내 위치에서 + 바라보는 방향으로 + 거리만큼 레이저 발사
        // interactLayer에 설정된 물체만 검사함
        RaycastHit2D hit = Physics2D.Raycast(rb.position, lastDir, interactDistance, interactLayer);

        if (hit.collider != null)
        {
            Debug.Log("감지된 물체: " + hit.collider.name);

            // 물체에서 EventObjectBase 컴포넌트를 찾음
            EventObjectBase eventObj = hit.collider.GetComponent<EventObjectBase>();

            if (eventObj != null)
            {
                // "나(this.gameObject)야, 상호작용 좀 해줘"라고 요청
                eventObj.OnInteract(this.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // [디버깅용] 게임 씬에서 빨간 선으로 감지 범위를 보여줌
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // 플레이어 중심에서 바라보는 방향으로 선 그리기
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)lastDir * interactDistance);
    }
}