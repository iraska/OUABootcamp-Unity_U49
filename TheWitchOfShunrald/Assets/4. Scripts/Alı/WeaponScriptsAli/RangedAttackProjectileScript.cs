using CihanAkpınar;
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

        [SerializeField] private LayerMask moveableObjectsLayerMask;

        [SerializeField] private GameObject projectileParticle;

        [SerializeField] private GameObject energyParticle;
        [SerializeField] private GameObject dieEnergyParticlePrefab;

        [SerializeField] private GameObject fireParticle;
        [SerializeField] private GameObject dieFireParticlePrefab;

        [SerializeField] private float projectilePowerMultiplier;

        private GameObject enemyToBeTracked;

        private Rigidbody rb;
        private bool isTracking = false;

        private Vector3 playerProjectileDestination;
        private float projectilePowerMagnitude;
        private float projectilePlayerDamage;
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

        public float ProjectilePlayerDamage
        {
            get { return projectilePlayerDamage; }
            set { projectilePlayerDamage = value; }
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
                spawnedEffect.transform.position = transform.position + Vector3.up;
                spawnedEffect.transform.localScale = new Vector3(projectilePowerMagnitude / 100, projectilePowerMagnitude / 100, projectilePowerMagnitude / 100);
            }
            else
            {
                GameObject spawnedEffect = Instantiate(dieFireParticlePrefab);
                spawnedEffect.transform.position = transform.position + Vector3.up;
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, 4f, canExplodeLayerMask);

            foreach (Collider collider in colliders)
            {
                Rigidbody objectInTrigger = collider.gameObject.GetComponent<Rigidbody>();
                float multiplierValue = (10f - ((objectInTrigger.gameObject.transform.position - transform.position).magnitude)) * projectilePowerMagnitude / 100 * projectilePowerMultiplier;
                if (multiplierValue < 1f)
                {
                    multiplierValue = 1f;
                }

                if (((1 << collider.gameObject.layer) & moveableObjectsLayerMask) != 0)
                {
                    objectInTrigger.AddForce((objectInTrigger.gameObject.transform.position - transform.position).normalized * multiplierValue, ForceMode.Impulse);
                    collider.gameObject.GetComponent<MoveableObjectScript>().MoveableObjectTakeDamage(multiplierValue);
                }
                else if (((1 << collider.gameObject.layer) & targetLayerMask) != 0)
                {
                    collider.gameObject.GetComponent<Enemy>().TakeDamage((objectInTrigger.gameObject.transform.position - transform.position).normalized * multiplierValue / 5, multiplierValue);
                }

            }

            Collider[] collidersTinyParts = Physics.OverlapSphere(transform.position, 4f, tinyPartsLayerMask);

            foreach (Collider collider in collidersTinyParts)
            {
                Rigidbody objectInTrigger = collider.gameObject.GetComponent<Rigidbody>();

                float multiplierValue = (10f - ((objectInTrigger.gameObject.transform.position - transform.position).magnitude)) * projectilePowerMagnitude / 100 * projectilePowerMultiplier;
                if (multiplierValue < 1f)
                {
                    multiplierValue = 1f;
                }

                if (((1 << collider.gameObject.layer) & tinyPartsLayerMask) != 0)
                {
                    objectInTrigger.AddForce((objectInTrigger.gameObject.transform.position - transform.position).normalized * multiplierValue, ForceMode.Impulse);
                }
            }


            Destroy(this.gameObject);
        }
    }
}


