using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace ali
{
    public class RangedAttackProjectileScript : MonoBehaviour
    {
        [SerializeField] private SphereCollider enemyFinderCollider;
        [SerializeField] private LayerMask enemyLayerMask;

        [SerializeField] private GameObject projectileParticle;
        [SerializeField] private GameObject energyParticle;
        [SerializeField] private GameObject fireParticle;

        private GameObject enemyToBeTracked;

        private Rigidbody rb;
        private bool isTracking = false;

        private Vector3 playerProjectileDestination;
        private float projectilePowerMagnitude;
        private int projecileType; //0 for energy, 1 for fire

        public Vector3 PlayerProjectileDestination
        {
            get { return playerProjectileDestination; }
            set { playerProjectileDestination = value; }
        }

        public float ProjectilePowerMagnitude
        {
            get { return projectilePowerMagnitude; }
            set { projectilePowerMagnitude = value; }
        }

        public int ProjecileType
        {
            get { return projecileType; }
            set { projecileType = value; }
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = PlayerProjectileDestination;

            if (ProjecileType == 0)
            {
                energyParticle.SetActive(true);
            }
            else
            {
                fireParticle.SetActive(true);
            }

            projectileParticle.transform.localScale = new Vector3(projectilePowerMagnitude / 100, projectilePowerMagnitude / 100, projectilePowerMagnitude / 100);
        }

        private void Update()
        {
            if (isTracking)
            {
                TrackTheEnemy();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.name == "Dummy")
            {
                Debug.Log(other.name);
                enemyToBeTracked = other.gameObject;
                isTracking = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == enemyLayerMask)
            {
                Destroy(this.gameObject);
            }
            
        }

        private void TrackTheEnemy()
        {
            Vector3 direction = enemyToBeTracked.transform.position - transform.position;
            Vector3 desiredVelocity = direction.normalized * rb.velocity.magnitude;

            float adjustmentSpeed = 5f;
            rb.velocity = Vector3.Lerp(rb.velocity, desiredVelocity, adjustmentSpeed * Time.deltaTime);
        }
    }
}


