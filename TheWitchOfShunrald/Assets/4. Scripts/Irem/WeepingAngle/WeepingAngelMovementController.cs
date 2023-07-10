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
        private bool isAngelVisible, isWalking, isWalkingAudioPlaying;

        private void Awake()
        {
            GetRequiredComponenent();
        }

        private void Update()
        {
            if (!GameManager.instance.Player.GetComponent<ShunraldController>().Movement.IsDeath && GameManager.instance.GameState == GameManager.State.Playing) 
            { 
                ChaseOrFreezeAngel(); 
            }
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
                isWalking = false;
                isWalkingAudioPlaying = false;

                wController.Animation.FreezeAngel();
            }
            else
            {
                isWalking = true;

                chaseDirection = shunraldWitch.position - transform.position;
                chaseDirection.y = 0f;
                chaseDirection.Normalize();

                Quaternion targetRot = Quaternion.LookRotation(chaseDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);

                transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);

                wController.Animation.AnimateAngel(1f, animator);

                if (!isWalkingAudioPlaying)
                {
                    isWalkingAudioPlaying = true;
                    
                    PlayWalkSFX();
                }
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

        private void PlayWalkSFX()
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.weepingAngleWalkAudio, transform.position);
        }
    }
}
