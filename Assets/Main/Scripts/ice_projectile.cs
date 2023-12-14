using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ice_projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    private bool isActive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isActive)
        {
            playerManager.player_Instance.TakeDamage(1, Vector2.zero);      
            
        };
        isActive = false;
        rb.velocity = Vector2.zero;
        animator.SetBool("hit", true);
    }

    private void hit()
    {
        Destroy(this.gameObject);
    }
}
