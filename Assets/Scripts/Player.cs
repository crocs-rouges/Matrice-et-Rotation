using UnityEngine;
using UnityEngine.InputSystem;

// Author : Romain Chevalier

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float maxMoveSpeed = 40f;
    [SerializeField] private float friction = 0.8f;


    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Deceleration
        if (rb.linearVelocity.magnitude > maxMoveSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxMoveSpeed;
        rb.AddForce(-rb.linearVelocity * friction);
        Move();
    }

    private void Move()
    {
        Vector3 lVelocityToApply = Vector3.zero;
        // Left
        if (Keyboard.current != null && Keyboard.current.aKey.isPressed)
            lVelocityToApply += new Vector3(-1, 0, 0);

        // Right
        if (Keyboard.current != null && Keyboard.current.dKey.isPressed)
            lVelocityToApply += new Vector3(1, 0, 0);

        // Up
        if (Keyboard.current != null && Keyboard.current.wKey.isPressed)
            lVelocityToApply += new Vector3(0, 0, 1);

        // Down
        if (Keyboard.current != null && Keyboard.current.sKey.isPressed)
            lVelocityToApply += new Vector3(0, 0, -1);

        lVelocityToApply = transform.rotation * lVelocityToApply.normalized * moveSpeed;
        lVelocityToApply = lVelocityToApply.normalized * moveSpeed;

        rb.AddForce(lVelocityToApply, ForceMode.Force);
    }
}
