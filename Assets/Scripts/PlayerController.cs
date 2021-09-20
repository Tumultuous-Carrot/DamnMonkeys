using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private Animator anim;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform feetpos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //���������� ���������� Rigidbody � Animator
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // �������� ���������
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); 

        //������� ������� ���������(�������������� � ����������� �� �������, � ������� ��� ��������)
        if ((!facingRight && moveInput > 0) || (facingRight && moveInput < 0)) 
        {
            Flip();
        }

        anim.SetBool("isRunning", moveInput != 0);
    }

    private void Update()
    {
        //�������� ��������������� � �����
        isGrounded = Physics2D.OverlapCircle(feetpos.position, checkRadius, whatIsGround);

        //������
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOff");
        }

        anim.SetBool("isJumping", !isGrounded);
    }

    //������� �������� ������� ���������
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("�� �������. ����������� � ���������� ���������");
        }
    }
}
