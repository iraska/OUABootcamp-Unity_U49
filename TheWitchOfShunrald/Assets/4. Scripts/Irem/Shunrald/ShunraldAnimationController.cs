using CameraSystem;
using UnityEngine;

namespace Shunrald
{
    public class ShunraldAnimationController : MonoBehaviour
    {
        private ShunraldController controller;

        // Animate the character
        public void AnimateCharacter(Vector3 _input, float veloX, float veloZ, Animator _animator)
        {
            veloX = Vector3.Dot(_input.normalized, transform.right);
            veloZ = Vector3.Dot(_input.normalized, transform.forward);

            _animator.SetFloat("VelocityX", veloX, .1f, Time.deltaTime);
            _animator.SetFloat("VelocityZ", veloZ, .1f, Time.deltaTime);
        }
    }
}