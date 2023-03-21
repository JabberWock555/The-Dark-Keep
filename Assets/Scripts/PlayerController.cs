using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{ 
    public KeyType key;
    public RoomNo RoomEnter;
    public RoomNo RoomExit;
    public bool roomChanged = false;

    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private Slider healthBar;

    private Rigidbody2D rb;
    private int health = 100;
    private float horizontal;
    private bool vertical;
    private bool doubleJump;
    private bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("IsAlive", true);

    }
    private void Update()
    {
        //Player Run
        horizontal = Input.GetAxisRaw("Horizontal");
        HorizontalMovement(horizontal);
        //Player Jump
        vertical = Input.GetButtonDown("Vertical");
        VerticalMovement(vertical);
        //Player Attack
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack1");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Attack2");
        }
    }

    private void VerticalMovement(bool _vertical)
    {
        if (isGrounded)
        {
           doubleJump = true;
        }
        if (_vertical)
        {
            if (isGrounded)
            {
                animator.SetBool("Jump", true);
                rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            }
            else if (!isGrounded && doubleJump)
            {
                animator.SetBool("Jump", true);
                rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
               doubleJump = false;
            }
        }
        else if (!isGrounded && !_vertical)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Land", true);
        }
        else if (isGrounded && !_vertical)
        {
            animator.SetBool("Land", false);
        }
    }


    private void HorizontalMovement(float _horizontal)
    {

        Vector3 position = transform.position;
        position.x += _horizontal * speed * Time.deltaTime;
        transform.position = position;

        animator.SetFloat("Run", Mathf.Abs(_horizontal));

        Vector3 scale = transform.localScale;
        if(_horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if(_horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    public void TakeHit(int damage)
    {
        if (health > 0)
        {
            animator.SetTrigger("Hurt");
            health -= damage;
            healthBar.value = health;

            if (health <= 0)
            {
                animator.SetBool("IsAlive", false);
                enabled = false;
            }
        }
       
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            isGrounded = true;
        }

        if( collision.gameObject.GetComponent<KeyController>() != null)
        {
            KeyController _key = collision.gameObject.GetComponent<KeyController>();
            key = _key.keyType;
        }
    }
}
