using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Health { get; set; }
    public int Score { get; set; }

    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private Rigidbody2D rb;
    private float horizontal;
    private bool vertical;
    private bool doubleJump;
    private bool isGrounded = true;

    [HideInInspector] public KeyType key;
    [HideInInspector] public RoomNo RoomEnter;
    [HideInInspector] public RoomNo RoomExit;
    [HideInInspector] public bool roomChanged = false;

    private void Awake()
    {
        Score = 0;
        Health = 100;
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
        if (Health > 0)
        {
            animator.SetTrigger("Hurt");
            Health -= damage;

            if (Health <= 0)
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

        if (collision.CompareTag("coin"))
        {
            Debug.Log("Coin Collected");
            Score += 5;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("potion"))
        {
            if (Health < 86)
            {
                Health += 15;
                Destroy(collision.gameObject);
            }
        }
    }

}
