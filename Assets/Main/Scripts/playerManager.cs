using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerManager : MonoBehaviour
{


    //variables 
    [SerializeField] public int health;
    [SerializeField] public int maxHealth = 3;
    private seguimientoCamara movmientoCamara;
    [SerializeField] private float lostControlTime;
    [SerializeField] private float invincibleTime;
    public CombateCaCPlayer combatePlayer;
    public Vector2 posicion; 

    //nuevo
    [SerializeField] public int status; //0 -> gusano -> 1 fruta -> 2 banana etc
    [SerializeField] public GameObject[] frutas;
    public static playerManager player_Instance;
    public UnityEvent muerte = new UnityEvent();
    public UnityEvent recibirDmg = new UnityEvent();
    public UnityEvent frutamancia = new UnityEvent();
    public UnityEvent muerteFruta = new UnityEvent();
    public UnityEvent muertePablo = new UnityEvent();


    public BarraDeVida HealthBarra;
    public bool direccion; //izq false derecha true
    public bool puedeAtacar;

    //nuevo 2.0
    //[SerializeField] public GameObject prefruit;
    //pruebas a eliminar:

   // [SerializeField] public Sprite q1,q2,q3,q4;
   // [SerializeField] public GameObject sfxHIT;



    public void FruitScroll(GameObject frutaPrefab)
    {

        GameObject fruta = Instantiate(frutaPrefab, transform.position, transform.rotation);

    }

    private void Awake()
    {
        if (player_Instance == null)
        {
            player_Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        movmientoCamara = GetComponent<seguimientoCamara>();
     
        combatePlayer = GetComponent<CombateCaCPlayer>();
        status = 0;
        transformacion(status);

        health = 1;
        //muerte.AddListener(SceneController.instance.Defeat);
    }

    public void TakeDamage(int damage, Vector2 position)
    {
        Debug.Log("Recibe dmg");
        health -= damage;
        recibirDmg.Invoke();
        //HealthBarra.ChangeActualHealth(health);

        if (playerManager.player_Instance.status == 0 && health <= 0 )
        {
            //health = 0;
            //StartCoroutine(LostControl());
            //StartCoroutine(UnableCollider());
            //movement.Rebound(position);
              
                
                muertePablo.Invoke();

             GameManager.Instance.Death();
        }
        if(health <= 0 && playerManager.player_Instance.status != 0)
        {
            Muerte();
            
            StartCoroutine(UnableCollider());
        }
        //currentHealthHearts--;

        //for (int i = m_instancedHearts.Count - 1; i >= 0; i--)
        //{
        //    var l_heart = m_instancedHearts[i];
        //    if (!l_heart.activeSelf)
        //    {
        //        continue;
        //    }

        //    l_heart.SetActive(false);
        //    break;
        //}
       
    }
    //public void attackFX(int position)
    //{

    //    sfxHIT.SetActive(true);
    //    sfxHIT.GetComponent<SpriteRenderer>().sprite = q1;
        

    //    sfxHIT.transform.position = new Vector3(-5, 5,10);
    //    //frutas[status].GetComponentInChildren<SpriteRenderer>().sprite = q1;
    //    //frutas[status].GetComponentInChildren<Transform>().position = new Vector2(-5,5);

    //}
    public void isFollowing()
    {
        movmientoCamara.sePuedeMover = true;
    }
    public void notFollowing()
    {
        movmientoCamara.sePuedeMover = false;
    }

    private IEnumerator UnableCollider()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(invincibleTime);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        transformacion(0);
    }
    private IEnumerator LostControl()
    {
        //movement.sePuedeMover = false;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(lostControlTime);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        //movement.sePuedeMover = true;
        transformacion(0);
    }


    private void Muerte()
    {
      
        muerteFruta.Invoke();
        


    }

    private void Update()
    {
        //this.transform.position = frutas[status].transform.position;
        // transform.position = Vector2.MoveTowards(transform.position,frutas[status].transform.position,5f*Time.deltaTime);
        posicion = this.transform.position;
        //posicion = new Vector2(frutas[status].transform.position.x, frutas[status].transform.position.y);
        if (Input.GetButtonDown("Fire2") )
        {
            //FruitScroll();
            //Debug.Log("invoca fruta");

        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameManager.Instance.pause)
            {
                GameManager.Instance.Continue();
            }
            GameManager.Instance.Pause();
        }


        //Debug.Log(posicion);
    }


    public void transformacion(int scroll)
    {
        switch (scroll)
        {
            case 0:
                frutas[0].SetActive(true);
                frutas[1].SetActive(false);
                frutas[2].SetActive(false);
                frutas[3].SetActive(false);
                playerManager.player_Instance.status = 0;

                health = 1;

                //SetHearts = 1;


                break;
            case 1:
                frutas[1].SetActive(true);
                frutas[2].SetActive(false);
                frutas[3].SetActive(false);
                frutas[0].SetActive(false);
                playerManager.player_Instance.status = 1;
                health = maxHealth;
                frutamancia.Invoke();

                break;
            case 2:
                frutas[2].SetActive(true);
                frutas[0].SetActive(false);
                frutas[1].SetActive(false);
                frutas[3].SetActive(false);
                playerManager.player_Instance.status = 2;
                health = maxHealth;
                frutamancia.Invoke();

                break;
            case 3:
                frutas[3].SetActive(true);
                frutas[1].SetActive(false);
                frutas[2].SetActive(false);
                frutas[0].SetActive(false);
                playerManager.player_Instance.status = 3;
                health = maxHealth;
                frutamancia.Invoke();
                break;
            default:
                //codigo que mantiene la fruta actual,
                break;
        }
    }
}
