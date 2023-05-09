using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;

    [SerializeField] private AudioSource jumpSoundEffect;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (rigid.velocity.x > maxSpeed) //���� �̵�
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1)) // ���� �̵�
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space)) // ����
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpSoundEffect.Play();
        }

    }
}
