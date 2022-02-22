using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

   private void Start() {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
   }

   private void Update() {
        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead)
        {
            //사망시 종료
            return;
        }
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)//마우스 좌클릭&&최대 점프횟수에 도달하지 않았다면
        {
            //점프횟수증가, 점프직전 속도를0으로, 위쪽으로 힘주기, 오디오소스 재생
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        animator.SetBool("Grounded", isGrounded);
   }

    private void Die()
    {
        // 사망 처리
        animator.SetTrigger("Die"); //애니메이터의 Die 트리거 파라미터를 셋
        playerAudio.clip = deathClip; // 오디오 소스에 할당된 오디오 클립을 변경
        playerAudio.Play(); //사망 효과음 재생

        playerRigidbody.velocity = Vector2.zero; //속도를 제로로 변경
        isDead = true; //사망상태를 true로 변경
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        if (other.tag == "Dead" && !isDead) // 충돌한 상대방의 태그가 Dead이며 아직 사망하지 않았다면 Die()실행

        {
            Die();
        }
   }

   private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f) //어떤 콜라이더와 닿았으며 충돌표면이 위쪽을 보고있으면
        {
            isGrounded = true; //isGrounded를 true로 변경하고, 누적 점프 횟수를 0으로 리셋
            jumpCount = 0;
        }
   }

   private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false; //콜라이더에서 떼어진 경우 isGrounded를 false로 변경
   }
}