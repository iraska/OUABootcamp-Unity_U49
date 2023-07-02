using UnityEngine;

namespace WeepingAngle
{
    public class WeepingAngleStats : MonoBehaviour, Enemy
    {
        [SerializeField] private float angelHealth, angelDamage;

        public float AngelHealth { get { return angelHealth; } }
        public float AngelDamage { get { return angelDamage; } }

        void Enemy.TakeDamage(Vector3 exploLocation, float damage)
        {
            angelHealth -= damage;

            if (angelHealth < 0)
            {
                Destroy(gameObject);
            }

            Debug.Log("CALISIYORUM");
        }

        private void Update()
        {
            //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAA" + angelHealth);
        }
    }
}
