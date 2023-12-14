using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private int currentHealthHearts;
    [SerializeField] private RectTransform m_heartContainer;
    private List<GameObject> m_instancedHearts = new List<GameObject>();
    //[SerializeField] private Image m_lifebar;
    [SerializeField] private GameObject m_heartPrefab;

    [SerializeField] public int maxHealth;
    //[SerializeField] private BarraDeVida barraDeVida;
    private Movement movement;
    [SerializeField] private float lostControlTime;
    [SerializeField] private float invincibleTime;
    private Animator animator;
    public CombateCaCPlayer combatePlayer;
    public bool gusano;


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        combatePlayer = GetComponent<CombateCaCPlayer>();

        health = maxHealth;
        SetHearts(maxHealth);
        //  barraDeVida.initializeHealthBar(health);
    }
    private void SetHearts(int p_hearts)
    {
        currentHealthHearts = p_hearts;
        
        for (int i = 0; i < p_hearts; i++)
        {
            var l_heart = Instantiate(m_heartPrefab, m_heartContainer);
            m_instancedHearts.Add(l_heart);
        }
    }
    public void TakeDamage(float damage, Vector2 position)
    {
        health -= damage;
        //m_lifebar.fillAmount = health / maxHealth;
        currentHealthHearts--;
       
        for (int i = m_instancedHearts.Count - 1; i >= 0; i--)
        {
            var l_heart = m_instancedHearts[i];
            if (!l_heart.activeSelf)
            {
                continue;
            }

            l_heart.SetActive(false);
            break;
        }
        //barraDeVida.ChangeActualHealth(health);
        if (health <= 0)
        {
            health = 0;
            //Muerte();

            // animator.SetTrigger("Hit");
            //perder control
            StartCoroutine(LostControl());
            StartCoroutine(UnableCollider());
            movement.Rebound(position);
            //barraDeVida.ChangeActualHealth(health);
                //animator.SetBool("gusano", true);
                Muerte();
            if (gusano)
            {
                //gameover
                //GameManager.Instance.GameOver();
            }
        }
    }
        
    private IEnumerator UnableCollider()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(invincibleTime);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
    private IEnumerator LostControl()
    {
        movement.sePuedeMover = false;
        yield return new WaitForSeconds(lostControlTime);
        movement.sePuedeMover = true;
    }
    private void Muerte()
    {
        //animator.SetTrigger("Muerte");


        Debug.Log("morido");

        //Destroy(gameObject);
        //animator.
        gusano = true;
        combatePlayer.enabled = false;
        animator.SetBool("gusano", true);

    }
    public void curar()
    {
        health = maxHealth;
    }
    private void GetHealth(int p_amount)
    {
        var l_isCurrentHealthEnough = m_instancedHearts.Count - currentHealthHearts - p_amount > 0;

        if (l_isCurrentHealthEnough)
        {
            var l_maxAmount = currentHealthHearts + p_amount;
            for (int i = 0; i < l_maxAmount; i++)
            {
                m_instancedHearts[i].SetActive(true);
            }
        }
        else
        {
            var l_diff = currentHealthHearts + p_amount - m_instancedHearts.Count;

            //Renable old hearts until at the limit

            for (int i = 0; i < m_instancedHearts.Count; i++)
            {
                m_instancedHearts[i].SetActive(true);
            }

            //Add other hearts

            for (int i = 0; i < l_diff; i++)
            {
                var l_heart = Instantiate(m_heartPrefab, m_heartContainer);
                m_instancedHearts.Add(l_heart);
            }
        }

        currentHealthHearts = m_instancedHearts.Count;

    }
}
