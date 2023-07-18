using System;
using System.Collections;
using System.Collections.Generic;
using CihanAkpınar;
using Shunrald;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Sidar
{
    public class NecroAI : MonoBehaviour, Enemy
    {
        private GameObject player;
        private Animator animator;
        private NavMeshAgent agent;
        private Transform[] projectilePoints;
        public Transform projectilePointParent;
        public GameObject projectilePrefab;
        
        
        
        [SerializeField] private float attackRange; //10
        [SerializeField] private int random;
        [SerializeField] private float distance;
        [SerializeField] private float currentHealth;
        
        
        private bool canCrMultiProjectile = true;
        private bool canCrSpawnMinions = true;
        private bool canCrStunPLayer = true;
        private bool canCrHealthRegen = true;
        private bool canCrSingleProjectile = true;
        private bool isAttacking;
        private bool canMultiProjectile = true;
        
        
        [SerializeField] private float projectileAnimDelay;
        [SerializeField] private float stunAnimDelay; //6f
        [SerializeField] private float spawnAnimDelay; //2
        
        [SerializeField] private float multiProjectileTimeBetween;
        [SerializeField] private float projectileCooldown = 3f;
        [SerializeField] private float stunCooldown = 30f;
        [SerializeField] private float healthCooldown = 40f;
        [SerializeField] private float spawnCooldown = 20f;
        [SerializeField] private float multiprojectileCooldown; //12f
        
        [SerializeField] private int maxHealth = 100;
        
        [SerializeField] private float regenSpeedMin = 5f;
        [SerializeField] private float regenSpeedMax = 100f;
        
        [SerializeField] private ParticleSystem healthRegenParticle;
        
        private float regenSpeed;
        private float spawnTimer; // Timer to control spawn timing

        
        [SerializeField] private GameObject enemyClonePrefab; // The prefab for the enemy clone
        [SerializeField] private int numClones = 8; // Number of clones to spawn
        [SerializeField] private float radius = 5f; // Radius of the circle
        [SerializeField] private float spawnDelay = 1f; // Delay between each clone spawn

        private float angleIncrement; // Angle increment between clones
        
        
        private bool isRegenDone;
        private bool isRegenInterrupt;
        private int explosionsCount;
        private bool isMultiProjectile;

        [SerializeField] private float playerDamage;
        [SerializeField] private float objectDamage;
        [SerializeField] private float speed;
        
        
        private NecroUIHealthBarManager necroHealthBar;

        [SerializeField] private GameObject henricDialogueTrigger;

        private bool isDead;
        
        private bool halfHealthFirstTime;
        private int halfHealthCount;


        private void Start()
        {
            UIManager.instance.BossHealthBarPanel();
            currentHealth = maxHealth;
            player = GameObject.FindWithTag("Shunrald");
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            projectilePoints = new Transform[projectilePointParent.childCount];
            for (int i = 0; i < projectilePointParent.childCount; i++)
            {
                projectilePoints[i] = projectilePointParent.GetChild(i);
            }
            angleIncrement = 360f / numClones;
            spawnTimer = spawnDelay;
            StartCoroutine(StunPlayer());

        }

        private void Update()
        {
            if (!isDead)
            {
                if (GameManager.instance.GameState == GameManager.State.Playing)
                {
                    

                    distance = Vector3.Distance(transform.position, player.transform.position);
                    if (!isAttacking)
                    {
                        if (halfHealthFirstTime)
                        {
                            AudioManager.Instance.PlaySfx(AudioManager.Instance.necroHalfHealthAudio, transform.position);
                            StartCoroutine(HealthRegen());
                            halfHealthFirstTime = false;
                        }
                        if (distance > attackRange)
                        {
                            random = Random.Range(1, 4);
                            switch (random)
                            {
                                case 1:
                                    ChasePlayer();
                                    break;
                                case 2:
                                    if (canCrSpawnMinions)
                                    {
                                        StartCoroutine(SpawnMinions(false));
                                    }

                                    break;
                                case 3:
                                    if (canCrHealthRegen)
                                    {
                                        if (currentHealth <= 100)
                                        {
                                            StartCoroutine(SpawnMinions(true));
                                            StartCoroutine(HealthRegen());
                                        }
                                        else if (currentHealth <= 250)
                                        {
                                            StartCoroutine(HealthRegen());
                                        }
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            if (isMultiProjectile)
                            {
                                ChasePlayer();
                            }

                            agent.SetDestination(transform.position);
                            animator.SetBool("isChasing", false);
                            random = Random.Range(1, 6);
                            switch (random)
                            {
                                case 1:
                                    if (canCrMultiProjectile)
                                    {
                                        StartCoroutine(MutliProjectileAttack());
                                    }

                                    break;
                                case 2:
                                    if (canCrSingleProjectile)
                                    {
                                        StartCoroutine(SingleProjectileAttack());
                                    }

                                    break;
                                case 3:
                                    if (canCrStunPLayer)
                                    {
                                        StartCoroutine(StunPlayer());
                                    }

                                    break;
                                case 4:
                                    if (canCrSpawnMinions)
                                    {
                                        StartCoroutine(SpawnMinions(false));
                                    }

                                    break;
                                case 5:
                                    if (canCrHealthRegen)
                                    {
                                        if (currentHealth <= 100)
                                        {
                                            SpawnClone();
                                            StartCoroutine(HealthRegen());
                                        }
                                        else if (currentHealth <= 250)
                                        {
                                            StartCoroutine(HealthRegen());
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    StopAllCoroutines();
                    animator.Play("BossIdle");
                    agent.SetDestination(transform.position);

                }
            }
        }

        private void LookAtPlayer()
        {
            transform.LookAt(player.transform);
        }

        private void ChasePlayer(){
                agent.SetDestination(player.transform.position);
                animator.SetBool("isChasing", true);
        }


        /*private IEnumerator StayIdle(){
            canCrIdle = false;
            agent.SetDestination(transform.position);
            animator.SetBool("isIdle", true);
            yield return new WaitForSeconds(3f);
        }*/

        private IEnumerator MutliProjectileAttack()
        {
            isMultiProjectile = true;
            int count = 0;
            
            agent.SetDestination(transform.position);
            canCrMultiProjectile = false;
            isAttacking = true;
            animator.SetBool("isMutliProjectileAttack",true);
            yield return new WaitForSeconds(projectileAnimDelay);
            animator.SetBool("isMutliProjectileAttack", false);
            while (true)
            {
                LookAtPlayer();
                /*if(distance>attackRange)
                    ChasePlayer();
                else
                {
                    transform.LookAt(player.transform);
                    agent.SetDestination(transform.position);
                    animator.SetBool("isChasing", false);
                }   */
                    
                if (canMultiProjectile)
                {
                    canMultiProjectile = false;
                    AudioManager.Instance.PlaySfx(AudioManager.Instance.necroWhisperAudio, transform.position);
                    CircleProjectile();
                    Invoke(nameof(ResetMultiProjectile), multiProjectileTimeBetween);
                    count++;
                }
                if(count == 5)
                    break;
                yield return null;
            }
            isAttacking = false;
            yield return new WaitForSeconds(multiprojectileCooldown);
            canCrMultiProjectile = true;
        }


        private void ResetMultiProjectile()
        {
            canMultiProjectile = true;
        }

        private IEnumerator SingleProjectileAttack(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            canCrSingleProjectile = false;
            isAttacking = true;
            animator.SetBool("isSingleProjectileAttack",true);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.necroFollowBombAudio, transform.position);
            yield return new WaitForSeconds(2f);
            isAttacking = false;
            animator.SetBool("isSingleProjectileAttack", false);
            //SingleProjectile();
            yield return new WaitForSeconds(projectileCooldown);
            canCrSingleProjectile = true;
        }

        private void SingleProjectile()
        {
            speed = 8f;
            GameObject projectile = Instantiate(projectilePrefab, projectilePointParent.GetChild(3).position, Quaternion.identity);
            NecroProjectile projectileComponent = projectile.GetComponent<NecroProjectile>();
            projectileComponent.PlayerDamage = playerDamage;
            projectileComponent.ObjectDamage = objectDamage;
            projectileComponent.Speed = speed;
            projectileComponent.SetIsSingle(true);
            
        }

        private void CircleProjectile()
        {
            
            foreach (var pj in projectilePoints)
            {
                Vector3 direction = (pj.position - transform.position).normalized;
                GameObject projectile = Instantiate(projectilePrefab, pj.position, Quaternion.identity);
                NecroProjectile projectileComponent = projectile.GetComponent<NecroProjectile>();
                speed = 10f;
                playerDamage = 25;
                objectDamage = 40;
                projectileComponent.PlayerDamage = playerDamage;
                projectileComponent.ObjectDamage = objectDamage;
                projectileComponent.Speed = speed;
                projectileComponent.Shoot(direction);
                
            }
        }

        /*
        private IEnumerator areaAttack()
        {
            isMultiProjectile = true;
            int count = 0;
            canCrAreaAttack = false;
            isAttacking = true;
            animator.SetBool("isMutliProjectileAttack",true);
            yield return new WaitForSeconds(projectileAnimDelay);
            animator.SetBool("isMutliProjectileAttack", false);
            while (true)
            {
                if(distance>attackRange)
                    ChasePlayer();
                else
                {
                    transform.LookAt(player.transform);
                    agent.SetDestination(transform.position);
                    animator.SetBool("isChasing", false);
                }
                    
                if (canMultiProjectile)
                {
                    canMultiProjectile = false;
                    CircleProjectile();
                    Invoke(nameof(ResetMultiProjectile), multiProjectileTimeBetween);
                    count++;
                }
                if(count == 10)
                    break;
                yield return null;
            }
            isAttacking = false;
            yield return new WaitForSeconds(projectileCooldown);
            canCrMultiProjectile = true;
        }
        */


        private IEnumerator SpawnMinions(bool forHealth){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            animator.SetBool("isChasing", false);
            if(!forHealth)
                canCrSpawnMinions = false;
            animator.SetBool("isSpawningMinions",true);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.bossSpawnerAudio, transform.position);
            isAttacking = true;
            yield return new WaitForSeconds(spawnAnimDelay);
            SpawnClone();
            if(!forHealth)
                isAttacking = false;
            animator.SetBool("isSpawningMinions", false);
            yield return new WaitForSeconds(spawnCooldown);
            canCrSpawnMinions = true;
        }
        
        private void SpawnClone()
        {
            Vector3 bossPosition = transform.position;

            for (int i = 0; i < numClones; i++)
            {
                float angle = angleIncrement * i;
                float xPos = radius * Mathf.Cos(Mathf.Deg2Rad * angle) + bossPosition.x;
                float zPos = radius * Mathf.Sin(Mathf.Deg2Rad * angle) + bossPosition.z;

                Vector3 clonePosition = new Vector3(xPos, bossPosition.y, zPos);

                // Instantiate the clone GameObject at the calculated position
                GameObject clone = Instantiate(enemyClonePrefab, clonePosition, Quaternion.identity);

                // Set the forward direction of the clone to face outward
                Vector3 direction = bossPosition - clonePosition;
                clone.transform.forward = direction.normalized;
            }
        }

        private IEnumerator StunPlayer(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            animator.SetBool("isChasing", false);
            canCrStunPLayer = false;
            animator.SetBool("isStunDone", false);
            animator.SetBool("isStunPlayer",true);
            isAttacking = true;
            AudioManager.Instance.PlaySfx(AudioManager.Instance.necroStanCackleAudio, transform.position);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.necroStun, transform.position);
            StartCoroutine(player.GetComponent<ShunraldHangInTheAir>().HangInTheAir());
            yield return new WaitForSeconds(stunAnimDelay);
            animator.SetBool("isStunDone", true);
            isAttacking = false;
            animator.SetBool("isStunPlayer", false);
            StartCoroutine(player.GetComponent<ShunraldHangInTheAir>().ReleasesTheWitch());
            yield return new WaitForSeconds(stunCooldown);
            canCrStunPLayer = true;
        }


        private IEnumerator HealthRegen(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            animator.SetBool("isChasing", false);
            canCrHealthRegen = false;
            animator.SetBool("isHealthRegen",true);
            isAttacking = true;
            AudioManager.Instance.PlaySfx(AudioManager.Instance.necroHealthRejStartAudio, transform.position);
            if (!healthRegenParticle.isPlaying)
            {
                healthRegenParticle.Play();
            }
            while (true)
            {
                RegenerateHealth();
                if (isRegenDone || isRegenInterrupt)
                {
                    if (healthRegenParticle.isPlaying)
                    {
                        healthRegenParticle.Stop();
                    }
                    animator.SetBool("isHealthRegen", false);
                    AudioManager.Instance.PlaySfx(
                        isRegenDone
                            ? AudioManager.Instance.necroHealthRejFinishAudio
                            : AudioManager.Instance.necroHealthRejDamageAudio, transform.position);
                    isRegenDone = false;
                    isRegenInterrupt = false;
                    break;
                }
                yield return null;
            }
            //yield return new WaitForSeconds(healthAnimDelay);
            isAttacking = false;
            yield return new WaitForSeconds(healthCooldown);
            canCrHealthRegen = true;
        }

        private void RegenerateHealth()
        {
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
                isRegenDone = true;
                return;
            }

            float regenAmount = regenSpeed * Time.deltaTime;
            currentHealth += regenAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            UIManager.instance.BossHealthBar(maxHealth, currentHealth);
            UpdateRegenSpeed();
        }

        private void UpdateRegenSpeed()
        {
            // Adjust the regeneration speed based on the current health percentage
            var healthPercentage = currentHealth / maxHealth;
            regenSpeed = Mathf.Lerp(regenSpeedMin, regenSpeedMax, healthPercentage);
        }

        private void Die()
        {
            isDead = true;
            UIManager.instance.BossHealthBarDeactivate();
            Debug.Log("Boss defeated!");
            StopAllCoroutines();
            animator.Play("BossDeath");
            DisableAnimatorParametersExcept("isDead");
        }

        public void TriggerHenric()
        {
            henricDialogueTrigger.SetActive(true);
            henricDialogueTrigger.GetComponent<GeneralUseTrigger>().infoStarterForNecroDeath();
        }

        public void TakeDamage(Vector3 exploLocation, float damage)
        {
            isRegenInterrupt = true;
            currentHealth -= damage;
            AudioManager.Instance.PlaySfx(AudioManager.Instance.necroHealthRejDamageAudio, transform.position);
            if (currentHealth <= 250 && halfHealthCount == 0)
            {
                halfHealthFirstTime = true;
                halfHealthCount += 1;
            }
            
            UIManager.instance.BossHealthBar(maxHealth, currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public float Health()
        {
            return currentHealth;
        }

        public void SetEnemyStats(float health, float damage)
        {
            currentHealth = health;
        }
        
        void DisableAnimatorParametersExcept(string parameterName)
        {
            for (int i = 0; i < animator.parameters.Length; i++)
            {
                AnimatorControllerParameter parameter = animator.parameters[i];
                if (parameter.name.Equals(parameterName))
                {
                    animator.SetBool(parameter.name, true);
                }
                else
                {
                    animator.SetBool(parameter.name, false);
                }
            }
        }
    }
}




