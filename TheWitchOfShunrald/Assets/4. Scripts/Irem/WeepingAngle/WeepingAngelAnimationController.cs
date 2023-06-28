using System.Collections;
using UnityEngine;

namespace WeepingAngle
{
    public class WeepingAngelAnimationController : MonoBehaviour
    {
        private Animator animator;

        private float initialAnimSpeed, freezeAnimSpeed = 0f, 
            lerpSpeed = .2f, elapsedTime = 0f;

        private void Awake()
        {
            GetRequiredComponents();
        }

        private void Start()
        {
            initialAnimSpeed = animator.speed;
        }

        private void GetRequiredComponents()
        {
            animator = GetComponent<Animator>();
        }

        public void AnimateAngel(float _time, Animator _animator)
        {
            float time = elapsedTime / lerpSpeed;
            animator.speed = Mathf.Lerp(freezeAnimSpeed, initialAnimSpeed, time);
        }

        public void FreezeAngel()
        {
            StartCoroutine(FreezeAnimation());
        }

        public IEnumerator FreezeAnimation()
        {
            while (elapsedTime < lerpSpeed)
            {
                elapsedTime += Time.deltaTime;
                float time = elapsedTime / lerpSpeed;

                animator.speed = Mathf.Lerp(initialAnimSpeed, freezeAnimSpeed, time);

                yield return null;
            }

            animator.speed = freezeAnimSpeed;
        }
    }
}
