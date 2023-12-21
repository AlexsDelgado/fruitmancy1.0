using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSweepAttack : MonoBehaviour
{
    [SerializeField] private GameObject crow;
    private Rigidbody2D crowrb;
    [SerializeField] private GameObject warning;
    private SpriteRenderer warningRenderer;

    public float WarningTimer;
    private float warningTime;
    [SerializeField] private float warningBlinkTimer = 0.5f;
    private float warningBlinkTime;

    public float crowSpeed;

    [SerializeField] private GameObject flyTo;
    private Vector2 flyToPosition;
    [SerializeField] private GameObject flyFrom;
    private Vector2 flyFromPosition;

    public bool Sweeping;

    //private Vector2 flyToPositionLocal;
    //private Vector2 flyFromPositionLocal;

    //private Vector2 flyToPositionLocalRotated;
    //private Vector2 flyFromPositionLocalRotated;
    //private Vector3 eulerRotation;

    void Start()
    {

        //RandomRotation();

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
        if (warningTime < WarningTimer) 
        {
            Sweeping = true;
            warningBlinkTime += Time.deltaTime;
            if (warningBlinkTime >= warningBlinkTimer)
            {
                warningRenderer.enabled = !warningRenderer.enabled;
                warningBlinkTime = 0;
            }
        }

        else if (warningTime >= WarningTimer)
        {
            Sweeping = true;
            warningRenderer.enabled = false;
            crow.transform.position = Vector2.MoveTowards(crowrb.position, flyToPosition, crowSpeed * Time.deltaTime);
            //crow.transform.position = new Vector2(crowrb.position.x + crowSpeed * Time.deltaTime, crowrb.position.y);
        }

        bool igualX = Mathf.Approximately(crow.transform.position.x, flyToPosition.x);
        bool igualY = Mathf.Approximately(crow.transform.position.y, flyToPosition.y);
        
        //Debug.Log("X: " + crow.transform.position.x + "vs" + flyToPosition.x);
        //Debug.Log("Y: " + crow.transform.position.y + "vs" + flyToPosition.y);
        //Debug.Log("Iguales en x: " + igualX);
        //Debug.Log("Iguales en y: " + igualY);

        if (Mathf.Approximately(crow.transform.position.x, flyToPosition.x) && Mathf.Approximately(crow.transform.position.y, flyToPosition.y))
        {
            Debug.Log("Sweep attack finished");
            Sweeping = false;
            gameObject.SetActive(false);
        }

    }

    public void ResetAttack()
    {
        warningTime = 0;
        flyToPosition = flyTo.transform.position;
        flyFromPosition = flyFrom.transform.position;
        crow.transform.position = flyFromPosition;
    }

    public void RandomRotation()
    {
        transform.Rotate(0, 0, Random.Range(-180, 180));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(flyToPosition, 1);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(flyFromPosition, 1);
    }
}
