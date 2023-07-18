using CihanAkpınar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WizardEnemy : MonoBehaviour, Enemy
{
    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private float attackRange;
    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float coolDown;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    public float Damage { get { return damage; } }
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem electric, explosion;
    [SerializeField] private Transform staff;
    private Transform playerTransform;
    private bool isAttackActivate = true;

    [SerializeField] private GameObject basicManaPot;
    [SerializeField] private GameObject basicHealthPot;
    [SerializeField] private int healthPotProbability;
    [SerializeField] private int manaPotProbability;
    private int mainProbability;
    [SerializeField] private GameObject destroyParticle;
    //Spawner entegration
    public GameObject spawnerOfThisEnemy;

    private IEnumerator Start()
    {
        electric.Stop();
        electric.transform.SetParent(null);
        explosion.transform.SetParent(null);

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerTransform = GameManager.instance.Player.transform;
        yield return null;
        electric.gameObject.SetActive(false);
        explosion.gameObject.SetActive(false);
        while (true)
        {
            if (GameManager.instance.GameState == GameManager.State.Playing)
            {
                if (Physics.Raycast(new Vector3(transform.position.x, 0.3f, transform.position.z), playerTransform.position - transform.position, out RaycastHit hitInfo, 200f))
                {
                    if (hitInfo.transform.CompareTag("Shunrald"))
                    {
                        if ((transform.position - playerTransform.position).magnitude < attackRange)
                        {
                            if (isAttackActivate)
                            {
                                isAttackActivate = false;
                                Invoke(nameof(AttackActivate), coolDown);
                                transform.DOLookAt(playerTransform.position, 1f);
                                anim.SetBool("walk", false);
                                yield return new WaitForSeconds(0.2f);
                                anim.SetTrigger("attack");
                            }
                            else
                                anim.SetBool("walk", false);
                        }
                        else
                        {
                            anim.SetBool("walk", true);
                            Vector3 direction = playerTransform.position - transform.position;
                            transform.DOLookAt(playerTransform.position, 1f);
                            direction.Normalize();
                            transform.position += direction * speed * Time.deltaTime;
                        }
                    }
                    else
                    {
                        Vector3 randomPos;
                        Vector3 direction;
                        while (true)
                        {
                            randomPos = transform.position + 2 * transform.right + transform.forward;
                            direction = randomPos - transform.position;
                            if (Physics.Raycast(randomPos + Vector3.up, Vector3.down, 10f, groundLayer))
                            {
                                break;
                            }
                            else
                                transform.Rotate(Vector3.up);
                            yield return null;
                        }
                        anim.SetBool("walk", true);
                        transform.DOLookAt(randomPos, 1f);
                        while (true)
                        {
                            direction = randomPos - transform.position;
                            direction.Normalize();
                            if (Physics.Raycast(new Vector3(transform.position.x, 0.3f, transform.position.z), playerTransform.position - transform.position, out RaycastHit hit, 200f))
                            {
                                if (hit.transform.CompareTag("Shunrald"))
                                {
                                    break;
                                }
                            }
                            if (Physics.Raycast(new Vector3(transform.position.x, 0.3f, transform.position.z), direction, 0.5f))
                            {
                                break;
                            }
                            else if (direction.magnitude > 0.5f)
                            {
                                transform.position += direction * speed * Time.deltaTime;
                            }
                            else
                            {
                                break;
                            }
                            yield return null;
                        }
                    }
                }
            }
            yield return null;
        }
    }

    private void AttackActivate()
    {
        isAttackActivate = true;
    }
    public void Attack()
    {
        StartCoroutine(Attackk());
    }
    public IEnumerator Attackk()
    {
        electric.transform.position = staff.position;
        electric.gameObject.SetActive(true);
        electric.Play();
        AudioManager.Instance.PlaySfx(AudioManager.Instance.rangedEnemyRangeAttackAudio, transform.position);
        Vector3 targetPos = playerTransform.position + Vector3.up * 0.5f;
        while (true)
        {
            Vector3 direction = targetPos - electric.transform.position;
            if (direction.magnitude > 0.5f)
                electric.transform.position += direction * attackSpeed * Time.deltaTime;
            else
            {
                electric.gameObject.SetActive(false);
                explosion.transform.position = targetPos;
                explosion.gameObject.SetActive(true);
                explosion.Play();
                AudioManager.Instance.PlaySfx(AudioManager.Instance.rangedEnemyExploseAudio,explosion.transform.position);
                yield return new WaitForSeconds(0.3f);
                explosion.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }
    void DropMath()
    {
        mainProbability = UnityEngine.Random.Range(1, 101);
        if (mainProbability <= healthPotProbability)
        {
            Instantiate(basicHealthPot, transform.position, Quaternion.identity);
        }
        else if (healthPotProbability < mainProbability && mainProbability <= healthPotProbability + manaPotProbability)
        {
            Instantiate(basicManaPot, transform.position, Quaternion.identity);
        }
    }
    void Enemy.TakeDamage(Vector3 exploLocation, float damage)
    {
        anim.SetTrigger("injured");
        Vector3 jumpDirection = ((transform.position - exploLocation) + Vector3.up).normalized * 3;
        rb.AddForce(3 * jumpDirection, ForceMode.VelocityChange);
        health -= damage;
        AudioManager.Instance.PlaySfx(AudioManager.Instance.basicSpawnedEnemyTakeDamageAudio, transform.position);
        if (health <= 0)
        {
            if (spawnerOfThisEnemy != null)
            {
                spawnerOfThisEnemy.GetComponent<BasicEnemySpawner>().currentEnemy.Remove(this.gameObject);
            }
            AudioManager.Instance.PlaySfx(AudioManager.Instance.rangedEnemyDieAudio, transform.position);
            DropMath();
            Destroy(gameObject);
            Destroy(electric.gameObject);
            Destroy(explosion.gameObject);
        }
    }

    float Enemy.Health()
    {
        return health;
    }

    void Enemy.SetEnemyStats(float health, float damage)
    {
        this.health = health;
        this.damage = damage;
    }
    private void OnDestroy()
    {
        GameObject particle = Instantiate(destroyParticle,transform.position+Vector3.up, Quaternion.identity);
        Destroy(particle,2f);
        GameManager.instance.EnemyDestoyEvent();
        transform.DOKill();
    }
}
