using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Shunrald
{
    public class ShunraldHangInTheAir : MonoBehaviour
    {
        [SerializeField] private GameObject rightHandPivot, leftHandPivot;

        private Rigidbody rb;

        private float initialYPos, initialRightHandYPos, initialLeftHandYPos;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            initialYPos = transform.position.y;
            initialRightHandYPos = rightHandPivot.transform.position.y;
            initialLeftHandYPos = leftHandPivot.transform.position.y;
        }

        // The Necromancer grabs the Witch and lifts her from her arms.
        public IEnumerator HangInTheAir()
        {
            GameManager.instance.Player.GetComponent<ShunraldController>().Particle.TriggerCircleParticle();
            yield return new WaitForSeconds(.1f);

            //GameManager.instance.GameState = GameManager.State.Hang;
            
            // Freeze shunrald movement and animation
            GameManager.instance.Player.GetComponent<ShunraldMovementController>().FreezeShunraldMovement();
            //mouse input u kapat


            rb.isKinematic = true;

            DGMove(rightHandPivot, 4f, .1f);
            DGMove(leftHandPivot, 4f, .1f);

            GameManager.instance.Player.GetComponent<ShunraldController>().Particle.TriggerPortalParticle();
            yield return new WaitForSeconds(.3f);

            DGMove(gameObject, 0.6f, .3f);
        }

        // The Necromancer releases the Witch.
        public IEnumerator ReleasesTheWitch()
        {
            GameManager.instance.Player.GetComponent<ShunraldController>().Particle.StopAllParticles();
            yield return new WaitForSeconds(.5f);

            //GameManager.instance.GameState = GameManager.State.Playing;
           
            GameManager.instance.Player.GetComponent<ShunraldMovementController>().FreeShunraldMovement();

            rb.isKinematic = false;

            DGMove(rightHandPivot, initialRightHandYPos, .1f);
            DGMove(leftHandPivot, initialLeftHandYPos, .1f);
            DGMove(gameObject, initialYPos, .1f);
        }

        private void DGMove(GameObject gObject, float endVal, float duration)
        {
            gObject.transform.DOMoveY(endVal, duration).SetEase(Ease.Linear);
        }
    }
}
