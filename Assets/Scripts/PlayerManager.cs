using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;

    public Transform respawnPoint;

    [SerializeField] private AudioSource jumpSoundEffect;

    public GameManager manager;

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


    private void OnTriggerEnter2D(Collider2D other) 
    {
     if(other.CompareTag("Trap"))
     {
        Die();
     }   
     else if(other.CompareTag("Flag"))
     {
      respawnPoint=  other.gameObject.transform;
      Destroy(other);
     }
    }
   


    void Die()
    {
        Invoke("RespawnPlayer",2f);
        gameObject.SetActive(false);
        

    }






}
