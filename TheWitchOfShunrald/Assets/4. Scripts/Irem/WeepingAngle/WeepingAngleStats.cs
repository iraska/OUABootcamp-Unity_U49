using CihanAkpınar;
using Shunrald;
using UnityEngine;

namespace WeepingAngle
{
    public class WeepingAngleStats : MonoBehaviour, Enemy
    {
        [SerializeField] private float health, damage;

        public float Health { get { return health; } }
        public float Damage { get { return damage; } }

        private Animator _animator;

        private const string shunrald = "Shunrald";

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        float Enemy.Health()
        {
            return health;
        }

        void Enemy.TakeDamage(Vector3 exploLocation, float damage)
        {
            health -= damage;

            if (health < 0)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Instance.weepingAngleDieAudio,transform.position);
                GameManager.instance.EnemyDestoyEvent();
                Destroy(gameObject);
            }
        }
        void Enemy.SetEnemyStats(float health, float damage)
        {
            this.health = health;
            this.damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            TouchTheWitch(other);
        }

        private void TouchTheWitch(Collider collider)
        {
            // When the angel touches the witch
            if (collider.gameObject.CompareTag(shunrald))
            {
                _animator.speed = 0f;

                //The witch petrifies
                GameManager.instance.Player.GetComponent<ShunraldController>().Animation.PetrificationByAngel();

                // The witch turns gray
                GameManager.instance.Player.GetComponent<ShunraldController>().Material.ChangeShunraldMaterial();

                GameManager.instance.Player.GetComponent<ShunraldController>().Movement.IsDeath = true;
                GameManager.instance.Lose();
            }
        }

    }
}
