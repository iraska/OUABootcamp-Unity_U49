using System.Collections;
using UnityEngine;

namespace WeepingAngle
{
    public class WeepingAngelAnimationController : MonoBehaviour
    {
        private Animator angelAnimator;

        private float initialAnimSpeed, freezeAnimSpeed = 0f, 
            lerpSpeed = .2f, elapsedTime = 0f;

        private void Awake()
        {
            GetRequiredComponents();
        }

        private void Start()
        {
            initialAnimSpeed = angelAnimator.speed;
        }

        private void GetRequiredComponents()
        {
            angelAnimator = GetComponent<Animator>();
        }

        public void AnimateAngel(float _time, Animator _animator)
        {
            elapsedTime += Time.deltaTime;
            float time = elapsedTime / lerpSpeed;

            angelAnimator.speed = Mathf.Lerp(freezeAnimSpeed, initialAnimSpeed, time);
        }


        public void FreezeAngel()
        {
            StartCoroutine(FreezeAnimation());
        }

        public IEnumerator FreezeAnimation()
        {
            elapsedTime = 0f;

            while (elapsedTime < lerpSpeed)
            {
                elapsedTime += Time.deltaTime;
                float time = elapsedTime / lerpSpeed;

                angelAnimator.speed = Mathf.Lerp(initialAnimSpeed, freezeAnimSpeed, time);

                yield return null;
            }

            angelAnimator.speed = freezeAnimSpeed;
        }
    }
}
