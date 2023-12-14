using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour
{
    public float detectionRange = 10f;
    public float lockOnTime = 2f;
    public float dashSpeed = 10f;
    public float stunDuration = 2f;
    public float dashCooldown = 2f; // Cooldown duration after dashing.

    private Transform player;
    private bool isLockedOn = false;
    private bool isStunned = false;
    private bool isDashing = false; // New state to track if the enemy is currently dashing.
    private Vector2 dashDirection;
    private float timeSinceLockOn = 0f;
    private float timeSinceDashCooldown = 0f; // Track time since the last dash.
    private Rigidbody2D rb;             // Reference to the enemy's Rigidbody2D.

    public float attackRange = 2.0f;
    private float lastAttackTime;
    private float attackCD = 2.0f;
    private float damage = 1;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isStunned && !isDashing)
        {
            // Calculate the distance to the player.
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // If the player is within detection range, start locking on.
            if (distanceToPlayer < detectionRange && !isLockedOn)
            {
                timeSinceLockOn += Time.deltaTime;

                // Lock on to the player after the lock-on time.
                if (timeSinceLockOn >= lockOnTime)
                {
                    isLockedOn = true;
                    dashDirection = (player.position - transform.position).normalized;
                    Debug.Log("Locked on to player.");
                }
            }

            // If locked on, dash toward the player.
            if (isLockedOn)
            {
                rb.velocity = dashDirection * dashSpeed;
                Debug.Log("Dashing towards player.");
                isDashing = true; // Set the dashing state to true.
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Attack(collider.gameObject);
                    lastAttackTime = Time.time;
                }
            }
        }
        else if (isDashing)
        {
            // Check if enough time has passed for the cooldown.
            if (timeSinceDashCooldown >= dashCooldown)
            {
                // Reset state variables for the next dash.
                isDashing = false;
                isLockedOn = false;
                timeSinceLockOn = 0f;
                timeSinceDashCooldown = 0f;
                rb.velocity = Vector2.zero; // Stop the enemy's movement.
            }
            else
            {
                // Increment the cooldown timer.
                timeSinceDashCooldown += Time.deltaTime;
            }
        }
    }

    // Function to be called when the dash misses.
    public void StunEnemy()
    {
        isStunned = true;
        rb.velocity = Vector2.zero;
        // Optionally, play a stunned animation or handle other logic.
        Invoke("EndStun", stunDuration);
    }

    private void EndStun()
    {
        isStunned = false;
    }

    void Attack(GameObject player)
    {
        // You can play an attack animation or perform other actions here.



        //// Deal damage to the player.
        //Health playerHealth = player.GetComponent<Health>();
        //if (playerHealth != null)
        //{
        //    playerHealth.TakeDamage();
        //}
        //Debug.Log("Confirm Hit

        lastAttackTime += Time.deltaTime;
        if (lastAttackTime >= attackCD)
        {

            lastAttackTime = 0;
            Debug.Log("daño realizado");
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage, other.GetContact(0).normal);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        // Color del rango de ataque
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
