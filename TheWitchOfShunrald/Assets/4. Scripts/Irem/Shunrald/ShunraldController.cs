using UnityEngine;

namespace Shunrald
{
    public class ShunraldController : MonoBehaviour
    {
        public ShunraldMovementController Movement { get; private set; }
        public ShunraldAnimationController Animation { get; private set; }


        private void Awake()
        {
            Movement = GetComponent<ShunraldMovementController>();
            Animation = GetComponent<ShunraldAnimationController>();
        }
    }
}