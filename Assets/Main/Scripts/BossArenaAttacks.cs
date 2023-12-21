using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossArenaAttacks : MonoBehaviour
{
    public GameObject boss;
    public GameObject IceSpike;
    public GameObject ExplodingFeather;
    public GameObject CrowSweep;
    private CrowSweepAttack crowSweepScript;
    private BossBehaviour bossBehaviour;

    public float attackTimerBase;
    private float attackTimer;
    private float attackTime;

    public float LandingTimer;
    private float landingTime;

    private float difficultyMultiplier = 1;

    public GameObject Storm;
    private SnowStorm snowStorm;

    public GameObject StormParticlesRight;
    public GameObject StormParticlesLeft;
    public GameObject StormParticlesUp;
    public GameObject StormParticlesDown;
    private ParticleSystem stormParticlesRight;
    private ParticleSystem stormParticlesLeft;
    private ParticleSystem stormParticlesUp;
    private ParticleSystem stormParticlesDown;

    public bool Storming = false;
    private bool stormStarted = false;
    public float StormTimer;
    private float stormTime;

    void Start()
    {
        crowSweepScript = CrowSweep.GetComponent<CrowSweepAttack>();
        bossBehaviour = boss.GetComponent<BossBehaviour>();

        snowStorm = Storm.GetComponent<SnowStorm>();
        stormParticlesRight = StormParticlesRight.GetComponent<ParticleSystem>();
        stormParticlesLeft = StormParticlesLeft.GetComponent<ParticleSystem>();
        stormParticlesUp = StormParticlesUp.GetComponent<ParticleSystem>();
        stormParticlesDown = StormParticlesDown.GetComponent<ParticleSystem>();

        //IceSpikeAttack(1);
        //ExplodingFeatherAttack(1);
        //randomAttack();

    }

    void Update()
    {
        if (bossBehaviour.attacking)
        {
            attackTimer = attackTimerBase / difficultyMultiplier;
            attackTime += Time.deltaTime;
            
            if (attackTime > attackTimer)
            {
                randomAttack();
                attackTime = 0;
            }

            landingTime += Time.deltaTime;
            if (landingTime > LandingTimer && !crowSweepScript.Sweeping)
            {
                bossBehaviour.Land();
                landingTime = 0;
            }
        
        }

        float bossMissingHealth = (bossBehaviour.MaxHealth - bossBehaviour.CurrentHealth) / 100;
        difficultyMultiplier = 1 + bossMissingHealth;

        if (bossBehaviour.CurrentHealth <= bossBehaviour.MaxHealth * 0.2) 
        {
            if (!Storming)
            {
                Storming = true;
            }
            else
            {
                /*stormTime += Time.deltaTime;
                if (stormTime >= StormTimer)
                {
                    updateStorm();
                    stormTime = 0;
                }*/
            }
        }

        if (Storming)
        {
            if (!stormStarted)
            {
                startStorm();
            }
            stormTime += Time.deltaTime;
            if (stormTime >= StormTimer)
            {
                updateStorm();
                stormTime = 0;
            }
        }
    }

    private void randomAttack()
    {
        float r = Random.Range(0, 3);
        //Debug.Log("Ataque: " + r);
        switch (r)
        {
            case 0:
                IceSpikeAttack(difficultyMultiplier);
                break;
            case 1:
                ExplodingFeatherAttack(difficultyMultiplier);
                break;
            case 2:
                CrowSweepAttack(difficultyMultiplier);
                break;
        }
    }

    void IceSpikeAttack(float multiplier)
    {
        int amountBase = Random.Range(10, 20);
        float amountMultiplied = amountBase * multiplier;
        int amount = Mathf.RoundToInt(amountMultiplied);

        for (int i = 0; i < amount; i++) 
        {
            float angle = Random.Range(-180f, 180f);
            float r = Random.Range(0f, 6f) + Random.Range(0f, 6f) + Random.Range(0f, 6f) + Random.Range(0f, 6f) + Random.Range(0f, 2f);
            Vector2 spikePosition = new Vector2(r * Mathf.Cos(angle), r * Mathf.Sin(angle));

            Instantiate(IceSpike, spikePosition, Quaternion.identity);
        }
    }

    public void ExplodingFeatherAttack(float multiplier)
    {
        int amountBase = Random.Range(8, 15);
        float amountMultiplied = amountBase * multiplier;
        int amount = Mathf.RoundToInt(amountMultiplied);

        for (int i = 0; i < amount; i++)
        {
            float angle = Random.Range(-180f, 180f);
            float r = Random.Range(4f, 6f) + Random.Range(2f, 6f) + Random.Range(0f, 6f) + Random.Range(0f, 6f);
            Vector2 featherPosition = new Vector2(r * Mathf.Cos(angle), r * Mathf.Sin(angle));

            Instantiate(ExplodingFeather, featherPosition, Quaternion.identity);
        }
    }

    public void CrowSweepAttack(float multiplier)
    {
        if (!crowSweepScript.Sweeping)
        {
            CrowSweep.SetActive(true);
            float baseSpeed = crowSweepScript.crowSpeed;
            crowSweepScript.crowSpeed = baseSpeed * multiplier;
            float baseWarning = crowSweepScript.WarningTimer;
            crowSweepScript.WarningTimer = baseWarning / multiplier;
            crowSweepScript.RandomRotation();
            crowSweepScript.ResetAttack();
        }
        else return;
    }

    private void startStorm()
    {
        Storm.SetActive(true);
        snowStorm.StormDirection = SnowStorm.direction.right;
        StormParticlesRight.SetActive(true);
        var mainRight = stormParticlesRight.main;
        mainRight.prewarm = true;
        var mainLeft = stormParticlesLeft.main;
        mainLeft.prewarm = true;
        var mainUp = stormParticlesUp.main;
        mainUp.prewarm = true;
        var mainDown= stormParticlesDown.main;
        mainDown.prewarm = true;
        stormStarted = true;
    }

    private void updateStorm()
    {
        int newDirection = Random.Range(0, 4);
        switch (newDirection)
        {
            case 0:
                snowStorm.StormDirection = SnowStorm.direction.right;
                StormParticlesRight.SetActive(true);
                StormParticlesLeft.SetActive(false);
                StormParticlesUp.SetActive(false);
                StormParticlesDown.SetActive(false);
                Debug.Log("Storm right");
                break;
            case 1:
                snowStorm.StormDirection = SnowStorm.direction.left;
                StormParticlesRight.SetActive(false);
                StormParticlesLeft.SetActive(true);
                StormParticlesUp.SetActive(false);
                StormParticlesDown.SetActive(false);
                Debug.Log("Storm left");
                break;
            case 2:
                snowStorm.StormDirection = SnowStorm.direction.up;
                StormParticlesRight.SetActive(false);
                StormParticlesLeft.SetActive(false);
                StormParticlesUp.SetActive(true);
                StormParticlesDown.SetActive(false);
                Debug.Log("Storm up");
                break;
            case 3:
                snowStorm.StormDirection = SnowStorm.direction.down;
                StormParticlesRight.SetActive(false);
                StormParticlesLeft.SetActive(false);
                StormParticlesUp.SetActive(false);
                StormParticlesDown.SetActive(true);
                Debug.Log("Storm down");
                break;
        }
    }
}
