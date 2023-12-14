using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Animator m_animator;
    [SerializeField] private Vector2 direction;
    private Rigidbody2D rb2D;
    public bool sePuedeMover = true;
    [SerializeField] private Vector2 reboundSpeed;
    private void Start()
    {
        m_animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerManager.player_Instance.muerte.AddListener(muertePablo);
    }

    // Update is called once per frame
    void Update()
    {
        var l_horizontal = Input.GetAxisRaw("Horizontal");
        var l_vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(l_horizontal, l_vertical);
        //transform.position += (Vector3.right * l_horizontal + Vector3.up * l_vertical) * (movementSpeed * Time.fixedDeltaTime);

        bool l_isRunning = l_horizontal != 0;

        m_animator.SetBool("walking", l_isRunning);

        if (l_horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        else if (l_horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }

    public void muertePablo()
    {
        sePuedeMover = false;
        m_animator.SetTrigger("death");
        SceneController.instance.Defeat();
    }
    private void FixedUpdate()
    {
        if (sePuedeMover)
        {
            rb2D.MovePosition(rb2D.position + direction * movementSpeed * Time.fixedDeltaTime);
        }
    }
    public void Rebound(Vector2 hitpoint)
    {
        rb2D.velocity = new Vector2(-reboundSpeed.x * hitpoint.x, reboundSpeed.y);
    }
}
