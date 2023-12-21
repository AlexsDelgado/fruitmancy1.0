using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowStorm : MonoBehaviour
{
    public enum direction
    {
        right, left, up, down
    }

    public direction StormDirection;

    Rigidbody2D otherrb;

    public float intensity;
    private bool inTriggerZone;

    private Vector2 rightForce;
    private Vector2 leftForce;
    private Vector2 upForce;
    private Vector2 downForce;


    //public GameObject playermanager;
    //private Rigidbody2D playerrb;


    private void Start()
    {
        rightForce = new Vector2(intensity, 0);
        leftForce = new Vector2(-intensity, 0);
        upForce = new Vector2(0, intensity);
        downForce= new Vector2(0, -intensity);
    }

    private void Update()
    {
        if (inTriggerZone)
        {
            switch (StormDirection)
            {
                case direction.right:
                    //Debug.Log("storm");
                    otherrb.AddForce(rightForce);
                    break;
                case direction.left:
                    otherrb.AddForce(leftForce);
                    break;
                case direction.up:
                    otherrb.AddForce(upForce);
                    break;
                case direction.down:
                    otherrb.AddForce(downForce);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerManager"))
        {
            otherrb = other.gameObject.GetComponent<Rigidbody2D>();
            inTriggerZone = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerManager"))
        {
            inTriggerZone = false;
        }
    }
}
