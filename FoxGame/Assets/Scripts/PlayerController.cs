using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_speed = 5f;

    private Rigidbody2D m_playerRb;
    private Vector2 m_velocity;

    private void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //m_velocity *= m_speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        m_velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        m_playerRb.MovePosition((Vector2)transform.position + m_velocity.normalized * m_speed * Time.deltaTime);
    }

}
