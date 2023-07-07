using CihanAkpınar;
using Shunrald;
using UnityEngine;

namespace WeepingAngle
{
    public class WeepingAngelMovementController : MonoBehaviour
    {
        [SerializeField] private Transform shunraldWitch;
        [SerializeField] private float chaseSpeed = 3f, maxViewAngle = 45f;

        private WeepingAngleController wController;
        private Animator animator;

        private Vector3 chaseDirection;

        private const string shunrald = "Shunrald";
        private bool isAngelVisible;

        private void Awake()
        {
            GetRequiredComponenent();
        }

        private void Update()
        {
            if (!GameManager.instance.Player.GetComponent<ShunraldController>().Movement.IsDeath) { ChaseOrFreezeAngel(); }
        }

        private void GetRequiredComponenent()
        {
            wController = GetComponent<WeepingAngleController>();
            animator = GetComponent<Animator>();
        }

        private void ChaseOrFreezeAngel()
        {
            isAngelVisible = CheckAngelVisibility();

            if (isAngelVisible)
            {
                wController.Animation.FreezeAngel();
            }
            else
            {
                chaseDirection = shunraldWitch.position - transform.position;

                chaseDirection.y = 0f;
                chaseDirection.Normalize();

                Quaternion targetRot = Quaternion.LookRotation(chaseDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);

                //float forwardDir = (transform.rotation.eulerAngles.y < 170f) ? -1f : 1f;
                //transform.Translate(Vector3.forward * forwardDir * chaseSpeed * Time.deltaTime);

                transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);

                wController.Animation.AnimateAngel(1f, animator);

                AudioManager.Instance.PlaySfx(AudioManager.Instance.weepingAngleWalkAudio);
            }
        }

        private bool CheckAngelVisibility()
        {
            // Checking for someone in line of sight using Raycasting
            RaycastHit hit;

            chaseDirection = shunraldWitch.position - transform.position;

            // Is the Witch facing the Weeping Angel?
            if (Vector3.Angle(shunraldWitch.forward, transform.position - shunraldWitch.position) < maxViewAngle)
            {
                if (Physics.Raycast(transform.position, chaseDirection, out hit))
                {
                    if (hit.collider.CompareTag(shunrald))
                    {
                        // Shunrald sees Angel
                        return true;
                    }
                }
            }

            // Shunrald doesn't see Angel
            return false;
        }

    }
}
