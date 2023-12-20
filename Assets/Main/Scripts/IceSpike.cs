using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    [SerializeField] private float warningTimer;
    private float warningTime;
    [SerializeField] private float destroyTimer;
    private float destroyTime;

    private int state = 0;

    private Animator anim;
    private Collider2D collider;

    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    
    void Update()
    {
        switch (state)
        {
            case 0:
                warningTime += Time.deltaTime;
                if (warningTime >= warningTimer)
                {
                    state = 1;
                }
                break;
            case 1:
                anim.SetBool("growth", true);

                break;
            case 2:
                collider.enabled = true;
                destroyTime += Time.deltaTime;
                if (destroyTime >= destroyTimer)
                {
                    state = 3;
                }
                break;
            case 3:
                collider.enabled = false;
                anim.SetBool("shatter", true);
                break;

        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerManager.player_Instance.TakeDamage(1, other.GetContact(0).normal);
        }
    }

    public void UpdateState(int newState)
    {
        state = newState;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
