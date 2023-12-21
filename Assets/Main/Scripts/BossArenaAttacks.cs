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

    private float difficultyMultiplier = 1;

    void Start()
    {
        crowSweepScript = CrowSweep.GetComponent<CrowSweepAttack>();
        bossBehaviour = boss.GetComponent<BossBehaviour>();

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
}
