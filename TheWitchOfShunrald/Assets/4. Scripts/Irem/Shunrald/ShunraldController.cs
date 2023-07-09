using UnityEngine;

namespace Shunrald
{
    [RequireComponent(typeof(ShunraldAnimationController))]
    [RequireComponent(typeof(ShunraldMovementController))]
    [RequireComponent(typeof(ShunraldMaterialChanger))]
    [RequireComponent(typeof(ShunraldHangInTheAir))]

    public class ShunraldController : MonoBehaviour
    {
        public ShunraldAnimationController Animation { get; private set; }
        public ShunraldMovementController Movement { get; private set; }
        public ShunraldMaterialChanger Material { get; private set; }
        public ShunraldHangInTheAir Hang { get; private set; }

        private void Awake()
        {
            Animation = GetComponent<ShunraldAnimationController>();
            Movement = GetComponent<ShunraldMovementController>();
            Material = GetComponent<ShunraldMaterialChanger>();
            Hang = GetComponent<ShunraldHangInTheAir>();
        }
    }
}