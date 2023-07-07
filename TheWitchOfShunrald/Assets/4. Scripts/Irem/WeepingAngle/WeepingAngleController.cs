using Shunrald;
using UnityEngine;

namespace WeepingAngle
{
    [RequireComponent(typeof(WeepingAngelAnimationController))]
    [RequireComponent(typeof(WeepingAngelMovementController))]
    [RequireComponent(typeof(WeepingAngleStats))]

    public class WeepingAngleController : MonoBehaviour
    {
        public WeepingAngelMovementController Movement { get; private set; }
        public WeepingAngelAnimationController Animation { get; private set; }
        public WeepingAngleStats Stats { get; private set; }

        private void Awake()
        {
            Movement = GetComponent<WeepingAngelMovementController>();
            Animation = GetComponent<WeepingAngelAnimationController>();
            Stats = GetComponent<WeepingAngleStats>();
        }
    }
}
