using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodwormMovement : Enemy
{
    private enum state
    {
        Awake,
        Asleep,
        Moving,
        Lookout,
        Attacking
    };
    private state isAwake = state.Asleep;
    private state currentState = state.Moving;

    [SerializeField] private float wakeUpRange;
    [SerializeField] private float attackTryRange;
    [SerializeField] private float attackRange;

    public float attackTimer;
    private float lastAttack;
    
    private Animator animator;
    private Rigidbody2D rb2d;

    public bool canAttack;

    public float movementTimer;
    private float lastMovement;

    public Transform attackPoint;
    public LayerMask playerLayer;
    public LayerMask collisionLayer;


    public GameObject currency;

    private bool dug;
    private bool flagAwaken  = false;


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
      
        switch (isAwake)
        {
            case state.Asleep:
                float distance = Vector2.Distance(transform.position, playerTransform.position);
                //Debug.Log(distance);

                if (distance <= wakeUpRange)
                {
                    Debug.Log("Awake");
                    isAwake = state.Awake;
                    flagAwaken = true;
                    //Debug.Log("Awake");
                }
         
                break;
            case state.Awake:
                WBehaviour();
                break;
        }
        float distancia = Vector2.Distance(transform.position, playerTransform.position);
        
        if (distancia >= wakeUpRange*3 && flagAwaken)
        {
            Debug.Log("Se durmio");
            gameObject.SetActive(false);
        }
    }

    private void WBehaviour()
    {

        switch (currentState)
        {
            case state.Moving:
                //animator.Play("WDigIn");
                if (!dug)
                {
                    animator.SetTrigger("DigIn");
                    dug = true;
                }
                //Debug.Log("moving");
                //currentState = state.Lookout;

                break;
            case state.Lookout:
                dug = false;
                float distance = Vector2.Distance(transform.position, playerTransform.position);
                lastMovement += Time.deltaTime;
                lastAttack += Time.deltaTime;
                if (distance <= attackTryRange && canAttack && lastAttack > attackTimer)
                {
                    Debug.Log("esta en diistancia puede atacar y timer ok");
                    animator.SetTrigger("Attack");
                    //currentState = state.Attacking;
                    Girar();

                    Collider2D[] playerHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

                    foreach (Collider2D playerHit2 in playerHit)
                    {
                        Debug.Log("ataco el worm");
                        playerHit2.GetComponent<playerManager>().TakeDamage(1, transform.position);

                    }
                    lastAttack = 0;
                }

                if (lastMovement >= movementTimer)
                {
                    lastMovement = 0;
                    stateToMoving();
                }


                break;
            case state.Attacking:
                

                //currentState = state.Moving;
                break;
            default:
                currentState = state.Moving;
                break;

        }
    }

    public void stateToMoving()
    {
        currentState = state.Moving;
    }

    public void stateToLookout()
    {
        currentState = state.Lookout;
        lastMovement = 0;
    }


    private IEnumerator WMovement()
    {

        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Collider2D>().enabled = true;
        float newX = playerTransform.position.x + Random.Range(-5, 5);
        float newY = playerTransform.position.y + Random.Range(-5, 5);

        Collider2D[] newPosition = Physics2D.OverlapCircleAll(new Vector2(newX, newY), 1, collisionLayer);
        if (newPosition.Length == 0) {
            transform.position = new Vector3(newX, newY, transform.position.z);

        }
        lastMovement = 0;
        animator.SetTrigger("DigOut");
        
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log("larva dañada");
        health -= damage;
        StartCoroutine(hurtEnemy());
        if (health <= 0)
        {
            Death();
        }
    }
    public override void Death()
    {
        //animator.SetBool("Death");
        GameObject loot = Instantiate(currency, transform.position, transform.rotation);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);
    }

    private void Girar()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            //rend.flipX = true;
            //attackPoint.position = new Vector3(attackPoint.position.x + 2 * attackPoint.position.x, attackPoint.position.y, attackPoint.position.z);
        }
        else
        {

            transform.rotation = new Quaternion(0, 0, 0, 0);
            //rend.flipX = false;
            //attackPoint.position = new Vector3(attackPoint.position.x - 2* attackPoint.position.x, attackPoint.position.y, attackPoint.position.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wakeUpRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackTryRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
