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
            rb.constraints = RigidbodyConstraints.FreezePositionY;

            DGMove(rightHandPivot, 4f, .1f);
            DGMove(leftHandPivot, 4f, .1f);

            yield return new WaitForSeconds(.3f);

            DGMove(gameObject, 2f, .3f);
        }

        // The Necromancer releases the Witch.
        public void ReleasesTheWitch()
        {
            rb.constraints = RigidbodyConstraints.None;

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
