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

        private Animator _animator;

        private const string shunrald = "Shunrald";

        private void Awake()
        {
            Movement = GetComponent<WeepingAngelMovementController>();
            Animation = GetComponent<WeepingAngelAnimationController>();
            Stats = GetComponent<WeepingAngleStats>();

            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // When the angel touches the witch
            if (other.gameObject.CompareTag(shunrald))
            {
                _animator.speed = 0f;

                GameManager.instance.Player.GetComponent<ShunraldController>().Movement.IsDeath = true;
                GameManager.instance.Lose();
                //The witch petrifies
                GameManager.instance.Player.GetComponent<ShunraldController>().Animation.PetrificationByAngel();
                // The witch turns gray
                GameManager.instance.Player.GetComponent<ShunraldController>().Material.ChangeShunraldMaterial();
            }
        }
    }
}
