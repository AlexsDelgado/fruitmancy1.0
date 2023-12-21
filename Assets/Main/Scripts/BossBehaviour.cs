using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : Enemy
{
    [SerializeField] private float restTimer;
    private float restTime;
    private Animator animator;
    //private Rigidbody2D rb;

    public Vector2 FlyToPosition;
    public Vector2 FlyFromPosition;
    public Vector2 BasePosition;

    public float MaxHealth;
    private float currentHealth;
    public float CurrentHealth => currentHealth;

    public bool attacking = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        BasePosition = new Vector2(transform.position.x, transform.position.y);
        currentHealth = MaxHealth;
    }

    public enum state {
        Grounded,
        Flying,
        Away,
        Landing
    }
    private state crowState = state.Grounded;
    void Update()
    {
        switch (crowState)
        {
            case state.Grounded:
                animator.SetTrigger("Landed");
                restTime += Time.deltaTime;
                if (restTime > restTimer)
                {
                    crowState = state.Flying;
                }

                break;
            case state.Flying:
                animator.SetTrigger("Takeoff");
                //rb.position = new Vector2(rb.position.x - 0.05f, rb.position.y + 0.05f);
                transform.position = Vector2.MoveTowards(rb.position, FlyToPosition, 0.05f);
                if (transform.position == new Vector3(FlyToPosition.x, FlyToPosition.y, transform.position.z))
                {
                    crowState = state.Away;
                }
                break;
            case state.Away:
                transform.position = FlyFromPosition;
                attacking = true;
                /*if (transform.position == new Vector3(FlyFromPosition.x, FlyFromPosition.y, transform.position.z))
                {
                    crowState = state.Landing;
                }*/

                break;
            case state.Landing:
                attacking = false;
                animator.SetTrigger("Landing");
                transform.position = Vector2.MoveTowards(rb.position, BasePosition, 0.05f);
                if (transform.position == new Vector3(BasePosition.x, BasePosition.y, transform.position.z))
                {
                    crowState = state.Grounded;
                    restTime = 0;
                }

                break;
        }
    }

    public void changeState(state newState)
    {
        crowState = newState;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(FlyToPosition, 1);
        Gizmos.DrawWireSphere(FlyFromPosition, 1);
        Gizmos.DrawWireSphere(BasePosition, 1);
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //StartCoroutine(NewColor());
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public override void Death() 
    {
        GameManager.Instance.Victory();
    }
}
