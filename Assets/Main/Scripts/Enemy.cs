using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Enemy : MonoBehaviour
{
    //public float damagePerSecond = 1;
    //public float attackRange = 100;
    //public LayerMask attackLayer;
    [SerializeField] protected float health;
    public Color newColor;
    private Color colorActual;
    private Color colorAuxiliar;
    protected SpriteRenderer rend;
    [SerializeField] private float colorCD;

    //public float speed = 5;

    protected Rigidbody2D rb;
    protected Transform playerTransform;
    //public GameObject currency;
    // private Animator animator;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
       /* rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;*/
        //rend.sprite = vivo;
        //animator = GetComponent<Animator>();
        //Girar();
    }

    public virtual void TakeDamage(float damage)
    {

    }

    public virtual void Death()
    {
        
    }
        
    public IEnumerator NewColor()
    {
        rend.color = newColor;
        yield return new WaitForSeconds(colorCD);
        rend.color = Color.white;
    }

    public IEnumerator hurtEnemy()
    {
        colorActual = rend.material.color;
        newColor = new Color(colorActual.r, colorActual.g, colorActual.b, 1f);
        colorAuxiliar = new Color(colorActual.r, colorActual.g, colorActual.b, 0.6f);
        
      
        rend.material.color = colorAuxiliar;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = newColor;

        yield return new WaitForSeconds(0.2f);
        rend.material.color = colorAuxiliar;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = newColor;

        rend.material.color = colorAuxiliar;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = newColor;


    }
    /*private void Girar()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }
    }*/
}
