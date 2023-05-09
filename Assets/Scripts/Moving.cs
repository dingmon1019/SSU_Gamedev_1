using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Moving : MonoBehaviour
{
    public float speed = 5.0f; // 이동 속도
    public float jumpHeight = 7.5f; // 점프 최대치
    public float dashSpeed = 7.0f; // 대쉬 속도
    public float dashDuration = 0.5f; // 대쉬 지속 시간
    public float dashCooldown = 1.0f; // 대쉬 쿨타임

    private bool isGrounded = true; 
    private bool isDashing = false; 
    private float dashTimer = 0.0f; 
    private float dashCooldownTimer = 0.0f; 
    private float lastMoveDirection = 1.0f; // 이동방향


    [SerializeField] private TilemapCollider2D groundCollider;

    
    delegate void InputHandler();

    
    InputHandler horizontalInputHandler;
    InputHandler jumpInputHandler;
    InputHandler dashInputHandler;
    

    Rigidbody2D rb;

    void Start()
    {
   
    rb = GetComponent<Rigidbody2D>();
  


    
    horizontalInputHandler = HandleHorizontalInput;
    jumpInputHandler = HandleJumpInput;
    dashInputHandler = HandleDashInput;
    }


    void Update()
    {
        // 대쉬 timer
        if (dashCooldownTimer > 0.0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
            }
            return;
        }
        horizontalInputHandler();
        jumpInputHandler();
        dashInputHandler();
    }

    
    void HandleHorizontalInput()
    {
        // 이동 함수
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(horizontalInput * speed, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = movement;

        //이동방향
        if (horizontalInput > 0.0f)
        {
            lastMoveDirection = 1.0f;
        }
        else if (horizontalInput < 0.0f)
        {
            lastMoveDirection = -1.0f;
        }
    }


    void HandleJumpInput()
    {
        // 점프 함수
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            isGrounded = false;
        }
    }


    void HandleDashInput()
    {   
        // 대쉬 함수 - q버튼
        if (Input.GetKeyDown(KeyCode.Q) && dashCooldownTimer <= 0.0f)
        {
            isDashing = true;
            dashTimer = 0.0f;
            dashCooldownTimer = dashCooldown;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            float dashDirection = lastMoveDirection;

            rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);
        }
    }
    

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 감지 함수
        TilemapCollider2D tilemapCollider = collision.gameObject.GetComponent<TilemapCollider2D>();
        if (tilemapCollider == null)
        {
        
            return;
        }

        if (tilemapCollider == groundCollider)
        {
            isGrounded = true;
            return;
        }

    
    } 
  


}