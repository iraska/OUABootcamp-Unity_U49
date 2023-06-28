using UnityEngine;

namespace WeepingAngle
{
    public class WeepingAngleStats : MonoBehaviour
    {
        [SerializeField] private int angelHealth, angelDamage;

        public int AngelHealth { get { return angelHealth; } }
        public int AngelDamage { get { return angelDamage; } }

        public void TakeDamageFromWitch(int damage)
        {
            angelHealth -= damage;
        }
    }
}
