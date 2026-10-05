using UnityEngine;

// Author : Romain Chevalier

namespace Com.IsartDigital
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float timeBetweenSpawn = 0.5f;
        private float timer;


        protected virtual void Update()
        {
            timer += Time.deltaTime;
            // Spawn Projectile
            if (timer >= timeBetweenSpawn)
            {
                timer = 0;
                Instantiate(projectilePrefab);
            }
        }
    }
}