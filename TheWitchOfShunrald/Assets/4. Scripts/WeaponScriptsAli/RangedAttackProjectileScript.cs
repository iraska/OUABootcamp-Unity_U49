using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace ali
{
    public class RangedAttackProjectileScript : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayerMask;
        public LayerMask TargetLayerMask
        {
            get { return targetLayerMask; }
        }

        [SerializeField] private LayerMask explodeOnContactLayerMask;

        [SerializeField] private LayerMask canExplodeLayerMask;

        [SerializeField] private LayerMask tinyPartsLayerMask;

        [SerializeField] private GameObject projectileParticle;

        [SerializeField] private GameObject energyParticle;
        [SerializeField] private GameObject dieEnergyParticlePrefab;

        [SerializeField] private GameObject fireParticle;
        [SerializeField] private GameObject dieFireParticlePrefab;

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

        public void ProjectileFoundEnemy(GameObject enemy)
        {
            enemyToBeTracked = enemy;
            isTracking = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & explodeOnContactLayerMask) != 0)
            {
                GetDestroyed();
            }
            
        }


        private void TrackTheEnemy()
        {
            Vector3 direction = enemyToBeTracked.transform.position - transform.position;
            Vector3 desiredVelocity = direction.normalized * (rb.velocity.magnitude + 5f);

            float adjustmentSpeed = 5f;
            rb.velocity = Vector3.Lerp(rb.velocity, desiredVelocity, adjustmentSpeed * Time.deltaTime);
        }

        private void GetDestroyed()
        {
            if (projecileType == 0)
            {
                GameObject spawnedEffect = Instantiate(dieEnergyParticlePrefab);
                spawnedEffect.transform.position = transform.position;
            }
            else
            {
                GameObject spawnedEffect = Instantiate(dieFireParticlePrefab);
                spawnedEffect.transform.position = transform.position;
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, 4f, canExplodeLayerMask);

            foreach (Collider collider in colliders)
            {
                
                if (((1 << collider.gameObject.layer) & canExplodeLayerMask) != 0)
                {
                    Rigidbody objectInTrigger = collider.gameObject.GetComponent<Rigidbody>();

                    float multiplierValue = (5f - ((objectInTrigger.gameObject.transform.position - transform.position).magnitude)) * 6f;
                    if (multiplierValue < 5f)
                    {
                        multiplierValue = 5f;
                    }

                    objectInTrigger.AddForce((objectInTrigger.gameObject.transform.position - transform.position).normalized * multiplierValue, ForceMode.Impulse);

                    if (collider.gameObject.GetComponent<MoveableObjectScript>() != null)
                    {
                        collider.gameObject.GetComponent<MoveableObjectScript>().MoveableObjectTakeDamage(multiplierValue);
                    }
                }

            }
            
            Collider[] tinyColliders = Physics.OverlapSphere(transform.position, 4f, tinyPartsLayerMask);
            foreach (Collider collider in tinyColliders)
            {

                if (((1 << collider.gameObject.layer) & tinyPartsLayerMask) != 0)
                {
                    Rigidbody objectInTrigger = collider.gameObject.GetComponent<Rigidbody>();

                    float multiplierValue = (5f - ((objectInTrigger.gameObject.transform.position - transform.position).magnitude)) * 6f;
                    if (multiplierValue < 5f)
                    {
                        multiplierValue = 5f;
                    }

                    objectInTrigger.AddForce((objectInTrigger.gameObject.transform.position - transform.position).normalized * multiplierValue, ForceMode.Impulse);
                }

            }


            Destroy(this.gameObject);
        }
    }
}


