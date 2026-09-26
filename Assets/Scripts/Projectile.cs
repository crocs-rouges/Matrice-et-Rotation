using UnityEngine;
using System;

// Author : Romain Chevalier

namespace Com.IsartDigital
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject destination;
        [SerializeField] private float spawnDistance;
        [SerializeField] private float posY = 4;
        [SerializeField] private float moveSpeed;

        protected virtual void Start()
        {
            if (destination == null) destination = GameObject.FindWithTag("Pillar");

            SetProjectilePosition();
            SetProjectileVelocity();
        }

        private void SetProjectilePosition()
        {
            float lPosX = UnityEngine.Random.Range(-1f, 1f);
            float lPosZ = UnityEngine.Random.Range(-1f, 1f);

            transform.position = new Vector3(lPosX * spawnDistance, posY, lPosZ * spawnDistance);
        }

        private void SetProjectileVelocity()
        {
            Vector3 lDistance = destination.transform.position - transform.position;
            Vector3 lVelocity = lDistance.normalized * moveSpeed;
            Rigidbody lRb = GetComponent<Rigidbody>();
            lRb.AddForce(lVelocity);
        }

        private void OnCollisionEnter(Collision pCollider)
        {
            // Collision with Pillar 
            if (pCollider.gameObject == destination)
            {
                Debug.Log($"explosion of the pillar : YOU LOST");
                Destroy(gameObject);
                return;
            }

            // Collision with Shield
            if (pCollider.gameObject.TryGetComponent(out Shield lShield))
                Destroy(gameObject);
        }
    }
}