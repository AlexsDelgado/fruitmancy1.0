using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSweepAttack : MonoBehaviour
{
    [SerializeField] private GameObject crow;
    private Rigidbody2D crowrb;
    [SerializeField] private GameObject warning;
    private SpriteRenderer warningRenderer;

    [SerializeField] private float warningTimer;
    private float warningTime;
    [SerializeField] private float warningBlinkTimer = 0.5f;
    private float warningBlinkTime;

    public float crowSpeed;

    [SerializeField] private GameObject flyTo;
    private Vector2 flyToPosition;
    [SerializeField] private GameObject flyFrom;
    private Vector2 flyFromPosition;

    //private Vector2 flyToPositionLocal;
    //private Vector2 flyFromPositionLocal;

    //private Vector2 flyToPositionLocalRotated;
    //private Vector2 flyFromPositionLocalRotated;
    //private Vector3 eulerRotation;

    void Start()
    {
        warningRenderer = warning.GetComponent<SpriteRenderer>();
        crowrb = crow.GetComponent<Rigidbody2D>();

        flyToPosition = flyTo.transform.position;
        flyFromPosition = flyFrom.transform.position;

        //eulerRotation = transform.eulerAngles();

        //flyFromPositionLocal = new Vector2(FlyFromPosition.x + transform.position.x, FlyFromPosition.y + transform.position.y);
        //flyToPositionLocal = new Vector2(FlyToPosition.x + transform.position.x, FlyToPosition.y + transform.position.y);

        //flyToPositionLocalRotated = new Vector2(flyToPositionLocal.x + flyToPositionLocal.x * Mathf.Cos(transform.localEulerAngles.z), flyToPositionLocal.y + flyToPositionLocal.y * Mathf.Sin(transform.localEulerAngles.z));

        
    }

    void Update()
    {
        warningTime += Time.deltaTime;
        if (warningTime < warningTimer) 
        {
            warningBlinkTime += Time.deltaTime;
            if (warningBlinkTime >= warningBlinkTimer)
            {
                warningRenderer.enabled = !warningRenderer.enabled;
                warningBlinkTime = 0;
            }
        }

        else if (warningTime >= warningTimer)
        {
            warningRenderer.enabled = false;
            crow.transform.position = Vector2.MoveTowards(crowrb.position, flyToPosition, crowSpeed * Time.deltaTime);
            //crow.transform.position = new Vector2(crowrb.position.x + crowSpeed * Time.deltaTime, crowrb.position.y);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(flyToPosition, 1);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(flyFromPosition, 1);
    }
}
