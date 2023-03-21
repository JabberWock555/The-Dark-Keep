using System.Collections;
using UnityEngine;

public class BossMovement : EnemyMovement
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float gapBtwPlayer;
    [SerializeField] private float attackDistance;
    private bool moving;
    private bool isMovingRight_;
    private float distance;

    public int Health { get; set; }

    private void Awake()
    {
        Health = 200;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (distance < gapBtwPlayer && distance > attackDistance)
        {
            Chase();
            animator.SetBool("Attack", false);
        }
        else if (distance < attackDistance)
        {
            animator.SetBool("Attack",true);
        }
        else
        {
            StartCoroutine(Boss_Movement());
        }
    }

    private IEnumerator Boss_Movement()
    {
        if (!moving)
        {
            animator.SetBool("Walk", false);
            yield return new WaitForSeconds(5f);
            moving = true;
        }
        else
        {
            animator.SetBool("Walk", true);
            Movement(isMovingRight_);
            yield return null;
        }
    }

    private IEnumerator Idle()
    {
        yield return new WaitForSeconds(3f);
        moving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("turn1") || collision.CompareTag("Platform"))
        {
            if (isMovingRight_)
            {
                isMovingRight_ = false;
                StartCoroutine(Idle());
            }
            else
            {
                isMovingRight_ = true;
            }
        }

        if (collision.CompareTag("Weapon_1"))
        {
            TakeHit(10);
            animator.SetTrigger("Hurt");
        }
        else if (collision.CompareTag("Weapon_2"))
        {
            TakeHit(5);
            animator.SetTrigger("Hurt");
        }
        else if( collision.gameObject == player.gameObject)
        {
            player.TakeHit(damage);
        }
    }

    private void Chase()
    {
        animator.SetBool("Walk", true);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (transform.position.x > player.transform.position.x - 10f)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
}
