using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ExplosiveFeather : MonoBehaviour
{
    [SerializeField] private GameObject feather;
    [SerializeField] private GameObject marker;

    [SerializeField] private GameObject explosion;
    private ExplosionDamage explosionDamage;

    public float FallSpeed;


    private void Start()
    {
        explosionDamage = explosion.GetComponent<ExplosionDamage>();
    }

    void Update()
    {
        feather.transform.position = new Vector3(feather.transform.position.x, feather.transform.position.y - 1 * FallSpeed * Time.deltaTime, feather.transform.position.z);
        if (feather.transform.position.y <= marker.transform.position.y)
        {
            explosion.SetActive(true);
            feather.SetActive(false);
            marker.SetActive(false);
        }

        if (explosionDamage.ExplosionFinished == true)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
