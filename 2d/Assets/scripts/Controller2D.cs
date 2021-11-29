using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 velocity = Vector2.zero;

    public int moveSpeed = 10;
    public int jumpForce = 10;
    public int maxJumpCount = 2;

    bool jump = false;
    float directionX;
    int jumpCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal")*moveSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        
    }

    private void FixedUpdate()
    {
        Move(directionX, jump);
        jump = false;
    }

    public void Move(float directionX, bool jump)
    {
        rb.velocity = new Vector2(directionX * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        
        if (jump && )
        {
            rb.velocity = new Vector2(velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce * 100));
        }
    }
}
