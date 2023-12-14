using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : Enemy
{
    private Animator animator;
    public GameObject currency;

    [SerializeField] private float speed;

    [SerializeField] private float wakeUpRange;
    [SerializeField] private float jumpRange;

    [SerializeField] private float lockOnTime = 2;
    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float stunDuration = 2;

    [SerializeField] private float dashMaxLength = 2;
    private float timeSinceDashStart;

    [SerializeField] private float dashCooldown;
    private float dashTimer;

    private bool isDead = false;
    private bool isAwake = false;
    private bool isJumping = false;
    private bool isLockedOn = false;
    private bool isStunned = false;

    private float distance;
    private Vector2 direction;

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
        
        //chequeo que no esté dormida, en el aire o stunned
        if (isAwake && !isJumping && !isStunned)
        {
            Girar();

            //si está lejos, camina hacia el jugador
            if (distance > jumpRange)
            {
                animator.SetBool("isMoving", true);
                rb.velocity = -direction * speed;

            }

            //si está suficientemente cerca, salta al jugador (empieza la animación de preparar el salto)
            if (distance < jumpRange && dashTimer > dashCooldown )
            {
                rb.velocity = Vector2.zero;
                animator.SetTrigger("alert");
                isJumping = true;
            }
        }

        //controla la duración del salto una vez en el aire
        if (isJumping && isLockedOn)
        {
            jumpTimer();
        }

        dashTimer += Time.deltaTime;
    }

    //controla el tiempo de preparación del salto
    private IEnumerator alert()
    {
        yield return new WaitForSeconds(lockOnTime);
        animator.SetBool("isMoving", false);
        animator.SetBool("isJumping", true);
    }

    //controla el tiempo de stun despues de errar un salto
    private IEnumerator standUp()
    {
        yield return new WaitForSeconds(stunDuration);
        animator.SetTrigger("standUp");
        isStunned = false;
    }

    //controla el salto en sí, asignando su velocidad
    private void jump()
    {
        if (!isLockedOn)
        {
            //Debug.Log("Speed set");
            rb.velocity = -direction * dashSpeed;
            isLockedOn = true;
        }

    }

    //controla la duración máxima del salto
    private void jumpTimer()
    {
        if (timeSinceDashStart >= dashMaxLength)
        {
            // Reset state variables for the next dash.
            stopJump();
            isStunned = true;
            animator.SetTrigger("failed");
            
        }
        else
        {
            timeSinceDashStart += Time.deltaTime;
        }
    }

    //controla la colisión con el jugador o un obstáculo durante el salto
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isJumping)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                timeSinceDashStart = 0;
                rb.velocity = Vector2.zero;
                playerManager.player_Instance.TakeDamage(1, other.GetContact(0).normal);
                animator.SetTrigger("hit");
                Debug.Log("ñam");
                stopJump();
                //StartCoroutine(StopTime());
            }
            else
            {
                stopJump();
                isStunned = true;
                animator.SetTrigger("failed");
            }
        }
    }

    //frena el salto, auxiliar a otros metodos
    private void stopJump()
    {
        isLockedOn = false;
        timeSinceDashStart = 0;
        rb.velocity = Vector2.zero; // Stop the enemy's movement.
        animator.SetBool("isJumping", false);
        isJumping = false;
        dashTimer = 0;
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
        //StartCoroutine(muerte());
        GameObject loot = Instantiate(currency, transform.position, transform.rotation);
        Destroy(gameObject);
        //animator.SetBool("isDead", true);
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
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, desiredRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, jumpRange);
    }
}
