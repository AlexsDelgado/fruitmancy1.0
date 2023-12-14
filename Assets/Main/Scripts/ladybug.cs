using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class ladybug : Enemy
{
    private Animator animator;
    [SerializeField] private float wakeUpRange;
    [SerializeField] private float desiredRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTimer;
    private float lastAttack;
    public float speed;
    private bool isAwake = false;
    public GameObject projectile;
    private float distance;
    Vector2 direction;
    public GameObject currency;
    private bool isDead = false;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, playerTransform.position);
        direction = (transform.position - playerTransform.position).normalized;

        if (distance < wakeUpRange)
        {
            isAwake = true;
        }

        if (isAwake && !isDead)
        {
            movement();
            lastAttack += Time.deltaTime;
        }

        if (distance < attackRange && lastAttack > attackTimer)
        {
            animator.SetTrigger("attack");
            lastAttack = 0;
        }
    }

    private void movement()
    {
        if (distance < desiredRange)
        {
            rb.velocity = direction * speed;
        }

        if (distance > desiredRange)
        {
            rb.velocity = -direction * speed;
        }
        Girar();
    }

    private void shoot() 
    {
        Instantiate(projectile, transform.position + new Vector3(direction.x, direction.y, 0), Quaternion.identity);
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        //StartCoroutine(NewColor());
        if (health <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isDead", true);
    }

    public IEnumerator muerte()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject loot = Instantiate(currency, transform.position, transform.rotation);
        Destroy(gameObject);
        // Code to execute after the delay
    }

    private void Girar()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wakeUpRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, desiredRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
