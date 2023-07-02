using UnityEngine;

namespace WeepingAngle
{
    public class WeepingAngleStats : MonoBehaviour, Enemy
    {
        [SerializeField] private float angelHealth, angelDamage;

        public float AngelHealth { get { return angelHealth; } }
        public float AngelDamage { get { return angelDamage; } }

        float Enemy.Health()
        {
            return angelHealth;
        }

        void Enemy.TakeDamage(Vector3 exploLocation, float damage)
        {
            angelHealth -= damage;

            if (angelHealth < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
