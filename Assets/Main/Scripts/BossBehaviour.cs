using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class BossBehaviour : Enemy
{
    [SerializeField] private float restTimer;
    private float restTime;
    private Animator animator;
    //private Rigidbody2D rb;
    private Collider2D collider2d;

    public GameObject FlyTo;
    public GameObject FlyFrom;
    public GameObject Base;

    private Vector2 FlyToPosition;
    private Vector2 FlyFromPosition;
    private Vector2 BasePosition;

    public float MaxHealth;
    private float currentHealth;
    public float CurrentHealth => currentHealth;

    public bool attacking = false;

    public GameObject Trigger;
    private BossfightTrigger trigger;

    UI_Manager uimanager;
    private bool uiinitialized = false;


    private void Start()
    {
        trigger = Trigger.GetComponent<BossfightTrigger>();

        FlyToPosition = FlyTo.transform.position;
        FlyFromPosition = FlyFrom.transform.position;
        BasePosition = Base.transform.position;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        //BasePosition = new Vector2(transform.position.x, transform.position.y);
        
        currentHealth = MaxHealth;
        restTime = 4;

        uimanager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

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
        if (trigger.bossFightStarted)
        {
            if(currentHealth < MaxHealth * 0.6)
            {
                animator.SetBool("Tired", true);
            }
            initializeHealthUI();
            switch (crowState)
            {
                case state.Grounded:
                    animator.SetTrigger("Landed");
                    collider2d.enabled = true;
                    restTime += Time.deltaTime;
                    if (restTime > restTimer)
                    {
                        crowState = state.Flying;
                    }

                    break;
                case state.Flying:
                    animator.SetTrigger("Takeoff");
                    collider2d.enabled = false;
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
    }

    public void changeState(state newState)
    {
        crowState = newState;
    }

    public void Land()
    {
        crowState = state.Landing;
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
        updateHealthUI();
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

    private void initializeHealthUI()
    {
        if (!uiinitialized)
        {
            uiinitialized = true;
            uimanager.DisplayBossHealth(true);
            uimanager.UpdateBossHealth(currentHealth, MaxHealth);
        }
    }

    private void updateHealthUI()
    {
        uimanager.UpdateBossHealth(currentHealth, MaxHealth);
        if (currentHealth <= 0)
        {
            uimanager.DisplayBossHealth(false);
        }
    }
}
