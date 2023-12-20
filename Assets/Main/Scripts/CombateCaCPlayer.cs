using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CombateCaCPlayer : MonoBehaviour
{

    [SerializeField] private Transform hitController;
    [SerializeField] private float hitRadio;
    [SerializeField] private GameObject[] quarters;
    [SerializeField] private float hitDamage;
    [SerializeField] private float cooldownAttack;
    public bool puedeAtacar =true;
    private Animator m_animator;
    public AudioClip attackClip;
    private Movement movement;


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        playerManager.player_Instance.muerteFruta.AddListener(MuerteFruta);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && puedeAtacar == true)
        {
           // Punch();


            SoundManager.Instance.PlaySound(attackClip);
            StartCoroutine(Attack());

        }
    }
    private void MuerteFruta()
    {
        StartCoroutine(UnableCollider());
        m_animator.SetTrigger("death");

    }
    public void transformarPablito()
    {
        playerManager.player_Instance.transformacion(0);
    }
    private void Punch()
    {
        m_animator.SetTrigger("attack");
        //sfx_animator.SetTrigger("q1");

        switch (playerManager.player_Instance.status)
        {
            case 1:
                Collider2D[] rangoManzana = Physics2D.OverlapCircleAll(hitController.position, hitRadio);
                foreach (Collider2D collider in rangoManzana)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.transform.GetComponent<Enemy>().TakeDamage(hitDamage);
                    }
                }
                //Debug.Log("ataca manzana");
                break;
            case 2:
                Collider2D[] rangoBanana = Physics2D.OverlapBoxAll(hitController.position, new Vector2(hitRadio, 1), 1);
                foreach (Collider2D collider in rangoBanana)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.transform.GetComponent<Enemy>().TakeDamage(hitDamage);
                    }
                }
                //Debug.Log("ataca banana");
                break;
            case 3:
                //Collider2D[] rangoNaranja = Physics2D.OverlapCircleAll(hitController.position, hitRadio*2);
                Collider2D[] rangoDurazno = Physics2D.OverlapBoxAll(hitController.position, new Vector2(hitRadio, 1), 1);
                foreach (Collider2D collider in rangoDurazno)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.transform.GetComponent<Enemy>().TakeDamage(hitDamage);
                    }
                }
                //Debug.Log("ataca durazno");
                break;
            case 0:
                //no deberia hacer nada
                break;
        }
        StartCoroutine(Attack());


    }
    public void ataqueNuevo(int quarter)
    {

        StartCoroutine(animacionSFX(quarter));

        switch (playerManager.player_Instance.status)
        {
            case 0:
                break;
            case 1:

                Collider2D[] rangoManzana = Physics2D.OverlapCircleAll(quarters[quarter].GetComponent<Transform>().position, hitRadio);
                foreach (Collider2D collider in rangoManzana)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.transform.GetComponent<Enemy>().TakeDamage(hitDamage);
                    }
                }
                break;
            case 2:
                Collider2D[] rangoBanana = Physics2D.OverlapBoxAll(quarters[quarter].GetComponent<Transform>().position, new Vector2(hitRadio, 1), 1);
                foreach (Collider2D collider in rangoBanana)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.transform.GetComponent<Enemy>().TakeDamage(hitDamage);
                    }
                }
                //
                break;
            case 3:
                Collider2D[] rangoDurazno = Physics2D.OverlapBoxAll(quarters[quarter].GetComponent<Transform>().position, new Vector2(hitRadio, 1), 1);
                foreach (Collider2D collider in rangoDurazno)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.transform.GetComponent<Enemy>().TakeDamage(hitDamage);
                    }
                }
                //
                break;
        }



    }

    private IEnumerator Attack()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(cooldownAttack);
        puedeAtacar = true;
    }



    public IEnumerator animacionSFX(int quarter)
    {
        quarters[quarter].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        quarters[quarter].gameObject.SetActive(false);
      
    }

//puedeAtacar = false;
//yield return new WaitForSeconds(cooldownAttack);
//puedeAtacar = true;

private IEnumerator UnableCollider()
    {
        playerManager.player_Instance.notFollowing();
        movement.sePuedeMover = false;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(1);
        movement.sePuedeMover = true;
        playerManager.player_Instance.isFollowing();
        yield return new WaitForSeconds(2);
        Physics2D.IgnoreLayerCollision(6, 7, false);
       
    }
    //private void OnDrawGizmos()
    //{
       
    //        //Gizmos.color = Color.red;

    //        //Gizmos.DrawWireSphere(quarters[0].GetComponent<Transform>().position, hitRadio);
    //        //Gizmos.DrawWireSphere(quarters[1].GetComponent<Transform>().position, hitRadio);
    //        //Gizmos.DrawWireSphere(quarters[2].GetComponent<Transform>().position, hitRadio);
    //        //Gizmos.DrawWireSphere(quarters[3].GetComponent<Transform>().position, hitRadio);

    //        //Gizmos.color = Color.red;
    //        //Gizmos.DrawWireSphere(hitController.position, hitRadio);
    //        //Gizmos.color = Color.yellow;
    //        //Gizmos.DrawWireCube(quarters[0].GetComponent<Transform>().position, new Vector3(hitRadio, 1, 1));
    //        //Gizmos.DrawWireCube(quarters[1].GetComponent<Transform>().position, new Vector3(hitRadio, 1, 1));
    //        //Gizmos.DrawWireCube(quarters[2].GetComponent<Transform>().position, new Vector3(hitRadio, 1, 1));
    //        //Gizmos.DrawWireCube(quarters[3].GetComponent<Transform>().position, new Vector3(hitRadio, 1, 1));
    //        //Gizmos.color = Color.cyan;
    //        //Gizmos.DrawWireCube(hitController.position, new Vector3(hitRadio, 1, 1));
        

    //}
}
