using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float speed;
    [SerializeField] private int health = 50;
    [SerializeField] private int damage = 5;
    private EnemyDeath enemyDeath;
    private Animator animator;
    private bool isMovingRight;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyDeath = GetComponentInParent<EnemyDeath>();
    }
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (isMovingRight)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            transform.localScale = new Vector2(Mathf.Sign(player.transform.localScale.x)*transform.localScale.x, transform.localScale.y);
            player.TakeHit(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("turn") || collision.CompareTag("Platform"))
        {
            if (isMovingRight)
            {
                isMovingRight = false;
            }
            else
            {
                isMovingRight = true;
            }
        }

        if (collision.CompareTag("Weapon_1"))
        {
            TakeHit(10);
        }
        else if (collision.CompareTag("Weapon_2"))
        {
            TakeHit(5);
        }

    }

    private void TakeHit(int _damage)
    {
        if (health > 0)
        {
            health -= _damage;
        }
        else if (health <= 0)
        {
            animator.SetBool("Dead", true);
            enemyDeath.Death_(gameObject);
        }
    }
}
