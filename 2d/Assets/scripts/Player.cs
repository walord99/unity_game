using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float bonusFallSpeed = 0.2f;
    [SerializeField] public int maxJumpCount = 1;

    [SerializeField] private float groundCheckOffsetDown = 0.01f;
    [SerializeField] LayerMask GroundLayer;

    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] public Transform attackPos;
    [SerializeField] public LayerMask enemies;
    [SerializeField] private int attackDamage = 1;

    Rigidbody2D playerRB;
    BoxCollider2D playerC;
    

    private int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerC = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
   {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRB.velocity = new Vector2(horizontalInput * playerSpeed, playerRB.velocity.y);

        if(horizontalInput < 0)
        {
            
        }

        if (IsGrounded())
        {
            jumpCount = maxJumpCount;
        }

        if (Input.GetButtonDown("Jump")){


            if (jumpCount > 0)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                jumpCount--;
            }
        }
        if (!Input.GetButton("Jump"))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y - bonusFallSpeed);
        }

        //attack
        if (Input.GetButtonDown("Fire1"))
        {
            Collider2D[] hitList = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
            for (int i = 0; i < hitList.Length; i++)
            {
                hitList[i].GetComponent<Enemy>().TakeDamage(attackDamage, playerRB.position, 1.25f);
            }
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerC.bounds.center, playerC.bounds.size, 0f, Vector2.down, groundCheckOffsetDown, GroundLayer);
        return raycastHit.collider != null;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

