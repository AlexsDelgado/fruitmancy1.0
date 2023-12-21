using UnityEngine;

public class LadderEndGame : MonoBehaviour
{
    public GameObject Hatch;
    private bool touching = false;
    Collider2D otherCollider;

    private void Update()
    {
        if (touching && Input.GetButtonDown("ActionButton"))
        {
            //Input.GetButtonDown("ActionButton")
            //Input.GetKeyDown(KeyCode.T)
            otherCollider.transform.position = Hatch.transform.position;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerManager"))
        {
            otherCollider = other;
            touching = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerManager"))
        {
            touching = false;
        }
    }
}