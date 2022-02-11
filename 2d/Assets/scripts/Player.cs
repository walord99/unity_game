using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float bonusFallSpeed = 0.2f;
    [SerializeField] private int maxJumpCount = 1;

    [SerializeField] private float groundCheckOffsetDown = 0.01f;
    [SerializeField] LayerMask GroundLayer;

    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private int attackDamage = 1;

    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerBoxCollider;
    private SpriteRenderer playerSpriteRenderer;

    private int health;
    private int maxHealth = 10;

    private int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
   {
        //left right movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, playerRigidbody.velocity.y);

        //flip
        if (horizontalInput < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        //reset jump count
        if (IsGrounded())
        {
            jumpCount = maxJumpCount;
        }

        //jump
        if (Input.GetButtonDown("Jump")){


            if (jumpCount > 0)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                jumpCount--;
            }
        }
        
        //slow/fast fall
        if (!Input.GetButton("Jump"))
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y - bonusFallSpeed);
        }

        //attack
        if (Input.GetButtonDown("Fire1"))
        {
            TakeDamage(1);
            Collider2D[] hitList = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
            for (int i = 0; i < hitList.Length; i++)
            {
                hitList[i].GetComponent<Enemy>().TakeDamage(attackDamage, playerRigidbody.position, 1.25f);

            }
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerBoxCollider.bounds.center, playerBoxCollider.bounds.size, 0f, Vector2.down, groundCheckOffsetDown, GroundLayer);
        return raycastHit.collider != null;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void TakeDamage(int dmg)
    {
        TakeDamage(dmg, new Vector2(0,0));
    }

    public void TakeDamage(int dmg, Vector2 dmgOrigin, float knockbackStrenght = 0)
    {
        health -= dmg;
        PlayerHurtAnimation();
    }

    void PlayerHurtAnimation()
    {
        StartCoroutine(PlayerHurtAnimationCoroutine());
    }

    private IEnumerator PlayerHurtAnimationCoroutine()
    {
        playerSpriteRenderer.color = new Color(255f, 50f, 50f);
        yield return new WaitForSeconds(1f);
        playerSpriteRenderer.color = Color.white; 
    }
}

