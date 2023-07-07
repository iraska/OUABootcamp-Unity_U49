using CihanAkpınar;
using Shunrald;
using UnityEngine;

namespace WeepingAngle
{
    public class WeepingAngleStats : MonoBehaviour, Enemy
    {
        [SerializeField] private float angelHealth, angelDamage;

        public float AngelHealth { get { return angelHealth; } }
        public float AngelDamage { get { return angelDamage; } }

        private Animator _animator;

        private const string shunrald = "Shunrald";

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        float Enemy.Health()
        {
            return angelHealth;
        }

        void Enemy.TakeDamage(Vector3 exploLocation, float damage)
        {
            angelHealth -= damage;

            if (angelHealth < 0)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Instance.weepingAngleDieAudio); 

                Destroy(gameObject);
            }
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
