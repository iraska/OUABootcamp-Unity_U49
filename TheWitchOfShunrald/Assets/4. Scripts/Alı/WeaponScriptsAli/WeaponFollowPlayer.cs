using UnityEngine;

namespace ali
{
    public class WeaponFollowPlayer : MonoBehaviour
    {
        [SerializeField] private Transform playerLocation;

        void Update()
        {
            transform.position = playerLocation.position;
        }
    }
}

