using UnityEngine;

namespace Shunrald
{
    [RequireComponent(typeof(ShunraldAnimationController))]
    [RequireComponent(typeof(ShunraldMovementController))]
    //[RequireComponent(typeof(ShunraldMaterialChanger))]

    public class ShunraldController : MonoBehaviour
    {
        public ShunraldMovementController Movement { get; private set; }
        public ShunraldAnimationController Animation { get; private set; }
        //public ShunraldMaterialChanger Material { get; private set; }

        private void Awake()
        {
            Movement = GetComponent<ShunraldMovementController>();
            Animation = GetComponent<ShunraldAnimationController>();
            //Material = GetComponent<ShunraldMaterialChanger>();
        }
    }
}