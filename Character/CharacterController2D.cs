using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_PreviousPosition;
    private Vector2 m_CurrentPosition;
    private Vector2 m_NextMovement;

    public Vector2 Velocity { get; protected set; }

    // Start is called before the first frame update
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        m_CurrentPosition = m_Rigidbody2D.position;
        m_PreviousPosition = m_Rigidbody2D.position;
    }


    void FixedUpdate()
    {
        m_PreviousPosition = m_Rigidbody2D.position;
        m_CurrentPosition = m_PreviousPosition + m_NextMovement;
        Velocity = (m_CurrentPosition - m_PreviousPosition) / Time.deltaTime;
        m_Rigidbody2D.MovePosition(m_CurrentPosition);
        m_NextMovement = Vector2.zero;
    }

    /// <summary>
    /// This moves a rigidbody and so should only be called from FixedUpdate or other Physics messages.
    /// </summary>
    /// <param name="movement">The amount moved in global coordinates relative to the rigidbody2D's position.</param>
    public void Move(Vector2 movement)
    {
        m_NextMovement += movement;
    }

    public void Teleport(Vector2 position)
    {
        Vector2 delta = position - m_CurrentPosition;
        m_PreviousPosition += delta;
        m_CurrentPosition = position;
        m_Rigidbody2D.MovePosition(position);
    }
}
