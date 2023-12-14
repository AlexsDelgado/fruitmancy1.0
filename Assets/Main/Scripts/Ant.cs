using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Enemy
{
    public float attackRange;
    public LayerMask attackLayer;
    

    public float stopTime;

    public float speed = 5;

    [SerializeField] private bool isBoss;
    public GameObject currency;
    public AudioClip deathClip;
    private Animator animator;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //animator = GetComponent<Animator>();
        Girar();
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
        if (isBoss)
        {
            win();
        }
        else
        {
            //rend.sprite = morido;
            animator.SetTrigger("death");
            SoundManager.Instance.PlaySound(deathClip);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //StartCoroutine(muerte());
            speed = 0;
            attackRange = 0;
        }
       
    }

    //public IEnumerator muerte()
    //{
    //    yield return new WaitForSeconds(3);
    //    GameObject loot = Instantiate(currency, transform.position, transform.rotation);
    //    Destroy(gameObject);
    //    // Code to execute after the delay
    //}
    public void win()
    {
        GameManager.Instance.Victory();
    }
    public void LootAndDeath()
    {
   

            GameObject loot = Instantiate(currency, transform.position, transform.rotation);
            Destroy(gameObject);

     

    }
    
    public void LootAndDeathFly()
    {

        GameObject loot = Instantiate(currency, transform.position, transform.rotation);
        Destroy(gameObject);

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Color del rango de ataque
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    private void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= attackRange)
        {
            // Persigue al personaje
            rb.velocity = playerTransform.position - transform.position;
            animator.SetBool("walking",true);

            //rend.sprite = caminando;

            Girar();
        }
        else
        {
            // El personaje está fuera del radio de ataque
            // Detiene el movimiento del enemigo
            rb.velocity = Vector2.zero;
            animator.SetBool("walking",false);
            //rend.sprite = vivo;
        }

        // Actualiza la velocidad del enemigo
        rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<Player>().TakeDamage(1, other.GetContact(0).normal);

            playerManager.player_Instance.TakeDamage(1, other.GetContact(0).normal);
            //Debug.Log("ñam");
            StartCoroutine(StopTime());

        }
    }

    public IEnumerator StopTime()
    {
        speed = 0;
        yield return new WaitForSeconds(stopTime);
        speed = 5;
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
}
