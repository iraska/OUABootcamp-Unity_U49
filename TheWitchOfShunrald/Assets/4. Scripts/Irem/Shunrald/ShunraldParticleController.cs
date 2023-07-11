using DG.Tweening;
using UnityEngine;

namespace Shunrald
{
    public class ShunraldParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem portalParticle;
        [SerializeField] private ParticleSystem circleParticle;

        public void TriggerPortalParticle() => portalParticle.Play();
        public void TriggerCircleParticle() => circleParticle.Play();

        public void StopAllParticles()
        {
            circleParticle.Stop();
            portalParticle.Stop();
        }
    }
}
