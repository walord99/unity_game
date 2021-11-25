using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2d : MonoBehaviour
{
    [SerializeField] private Transform groudCheck;
    [SerializeField] private float jumpForce = 400f;

    private Rigidbody2D m_Rigidbody2D;

    const float groudCheckRadius = .2f;
    public UnityEvent OnLandEvent;
    bool isGrounded;
    // Start is called before the first frame update
    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groudCheck.position, groudCheckRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
    }

    public void Move(float Xdir, bool jump)
    {
        if (isGrounded && jump)
        {
            isGrounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }
}
