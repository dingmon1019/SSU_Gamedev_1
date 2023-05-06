using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    public float speed = 10.0f; // 이동 속도
    public float jumpHeight = 2.0f; // 점프 최대치

    private bool isGrounded = true; // 땅에 붙어 있는지 여부를 저장할 변수

    private Tilemap tilemap;

    void Start()
    {
        // Tilemap 객체를 가져옴
        tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); 

        Vector2 movement = new Vector2(horizontalInput * speed, GetComponent<Rigidbody2D>().velocity.y); 
        GetComponent<Rigidbody2D>().velocity = movement;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // 스페이스바 입력을 받고 땅에 붙어 있는 경우에만 점프 가능
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Sqrt(jumpHeight * -2f * Physics2D.gravity.y)); // 점프 처리
            isGrounded = false; // 점프 후 땅에서 벗어남
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Tilemap 객체와 충돌한 경우
        if (collision.gameObject.GetComponent<TilemapCollider2D>() == tilemap.GetComponent<TilemapCollider2D>())
        {
            isGrounded = true; // 땅에 붙어 있음
        }
    }

    
}