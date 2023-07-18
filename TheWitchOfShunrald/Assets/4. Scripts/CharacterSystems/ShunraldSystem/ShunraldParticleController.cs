using DG.Tweening;
using UnityEngine;

namespace Shunrald
{
    public class ShunraldParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem portalParticle;
        [SerializeField] private ParticleSystem circleParticle;

        public void TriggerPortalParticle() => portalParticle.Play();
        public void TriggerCircleParticle()
        {
            circleParticle.transform.position = new Vector3(transform.position.x, -1.15f, transform.position.z);
            circleParticle.Play();
        }

        public void StopAllParticles()
        {
            circleParticle.Stop();
            portalParticle.Stop();
        }
    }
}
