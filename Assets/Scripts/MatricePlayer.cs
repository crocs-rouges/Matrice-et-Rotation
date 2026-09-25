using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.VectorGraphics;

// Author : Romain Chevalier

namespace Com.IsartDigital
{
    [RequireComponent(typeof(Rigidbody))]
    public class MatricePlayer : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float moveSpeed = 20f;
        [SerializeField] private float maxMoveSpeed = 40f;
        [SerializeField] private float friction = 0.8f;


        void Start()
        {
            if (rb == null) rb = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            Vector3 lVelocityToApply = Vector3.zero;
            // Up
            if (Keyboard.current != null && Keyboard.current.wKey.isPressed)
                lVelocityToApply += transform.rotation * new Vector3(0, 0, 1);

            lVelocityToApply = lVelocityToApply.normalized * moveSpeed;

            rb.AddForce(lVelocityToApply, ForceMode.Force);
        }

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="pCollider">The Collision data associated with this collision.</param>
        private void OnCollisionEnter(Collision pCollider)
        {
            // Make Rotation With Matrice
            // Get Wall Normal
            ContactPoint lContact = pCollider.GetContact(0);
            Vector3 lContactNormal = lContact.normal;

            // Transform it to Matrice
            float lRad = lContactNormal.z * Mathf.Deg2Rad;
            float lCos = Mathf.Cos(lRad);
            float lSin = Mathf.Sin(lRad);

            Vector4 lMatriceRotation = new Vector4
            {
                x = lCos,
                y = -lSin,
                z = lSin,
                w = lCos
            };

            // Apply Matrice to Velocity
            Vector3 lVelocity = rb.linearVelocity;
            float lFirstLine = lMatriceRotation.x * lVelocity.x;
            lFirstLine += lMatriceRotation.y * lVelocity.z;

            float lSecondLine = lMatriceRotation.z * lVelocity.x;
            lSecondLine += lMatriceRotation.w * lVelocity.z;

            // Apply Rotation
            Vector3 lNewRotation = new Vector3(lFirstLine, 0, lSecondLine);
            transform.rotation.SetLookRotation(lNewRotation);


        }
    }
}