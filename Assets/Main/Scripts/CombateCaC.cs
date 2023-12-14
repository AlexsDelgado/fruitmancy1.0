using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{

    [SerializeField] public float stopTime;
    public Enemy enemy;
    //[SerializeField] private Transform controladorGolpe;
    //[SerializeField] private float hitRadio;
    //[SerializeField] private float hitDamage;
    //public GameObject Player;
    //public GameObject Ant;
    public void Start()
    {

    }
    //public float ObtenerDistanciaJugador()
    //{
    //    // Obtener las posiciones del jugador y el enemigo
    //    Vector2 playerPos = Player.transform.position;
    //    Vector2 antPos = transform.position;

    //    // Calcular la distancia entre las dos posiciones
    //    return Vector2.Distance(playerPos, antPos);
    //}
    //public void Update()
    //{
    //    // Calcula la distancia entre el jugador y el enemigo
    //    float distance = ObtenerDistanciaJugador();

    //    // Si el jugador está lo suficientemente cerca del enemigo, ataca
    //    if (distance <= hitRadio)
    //    {
    //        //Hit();
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<Player>().TakeDamage(1, other.GetContact(0).normal);
            playerManager.player_Instance.TakeDamage(1, other.GetContact(0).normal);
            //StartCoroutine(StopTime());

        }
    }

    /*public IEnumerator StopTime()
    {
        enemy.speed = 0;
        yield return new WaitForSeconds(stopTime);
        enemy.speed = 5;
    }*/

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(controladorGolpe.position, hitRadio);
    //}
}
