using CameraSystem;
using System.Collections;
using UnityEngine;

namespace Shunrald
{
    public class ShunraldMovementController : MonoBehaviour
    {
        [SerializeField] private TrailRenderer trailRend;
        [SerializeField] private LayerMask aimLayerMask;
        [SerializeField] private float speed, turnSpeed = 360, dashSpeed, dashTime;
        [SerializeField] private Camera lensCam;

        private ShunraldController controller;
        private Animator animator;
        private Rigidbody rb;

        private Vector3 input;

        private float velocityX, velocityZ;
        private bool isDashing;

        private void Awake()
        {
            GetRequiredComponent();
        }

        // for physics
        private void FixedUpdate()
        {
            GatherInput();
            CharacterMovement();

            if (isDashing) { StartCoroutine(Dash()); }
        }

        private void Update()
        {
            AimTowardMouse();
            LookIsometric();

            controller.Animation.AnimateCharacter(input, velocityX, velocityZ, animator);

            if (Input.GetKeyDown(KeyCode.Space)) { isDashing = true; }
        }

        private void GetRequiredComponent()
        {
            controller = GetComponent<ShunraldController>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        // Reading the input
        private void GatherInput()
        {
            input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        
        // Character always facing mouse cursor position
        private void AimTowardMouse()
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = lensCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
            {
                var direction = hitInfo.point - transform.position;
                direction.y = 0f;
                direction.Normalize();
                //transform.forward = direction;

                // Allows the character to dash in the direction of movement
                if (!isDashing && rb.velocity.magnitude <= 0.01f)
                {
                    transform.forward = direction;
                }
                //transform.forward = rb.velocity.magnitude <= 0.01f ? direction : rb.velocity.normalized;
            }
        }

        // Moving the character
        private void CharacterMovement()
        {
            if (input.magnitude > 0f)
            {
                input.Normalize();

                Vector3 movement = input.ToIso() * speed * Time.deltaTime;
                //rb.MovePosition(rb.position + movement);
                Vector3 newPosition = rb.position + movement;


                Vector3 targetPosition = Vector3.Lerp(rb.position, newPosition, 0.9f);
                rb.MovePosition(targetPosition);
            }
            else if (!isDashing)
            {
                // Stop motion only if still and not dash
                rb.velocity = Vector3.zero;
            }
        }

        private void LookIsometric()
        {
            if (input != Vector3.zero)
            {
                var relative = (transform.position + input.ToIso()) - transform.position;
                var rotation = Quaternion.LookRotation(relative, Vector3.up);
                
                //var rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(relative, Vector3.up), .2f);
                //rb.MoveRotation(rotation);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
                
                // if u wanna use DOTween, do not use in Update()
                // DoRotate();
            }
        }

        private IEnumerator Dash()
        {
            float startTime = Time.time;

            Vector3 dashVelocity = transform.forward * dashSpeed;

            while (Time.time < startTime + dashTime)
            {
                //rb.AddForce(transform.forward * dashSpeed * Time.deltaTime, ForceMode.Impulse);
                rb.velocity = dashVelocity;
                trailRend.emitting = true;

                yield return new WaitForSeconds(.1f);
            }

            // Dash effect is over, slow speed to zero
            float declerationTime = .05f;
            float elapsedTime = 0f;
            Vector3 initialVelocity = rb.velocity;

            while (elapsedTime < declerationTime)
            {
                float time = elapsedTime / declerationTime;
                rb.velocity = Vector3.Lerp(initialVelocity, Vector3.zero, time);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            rb.velocity = Vector3.zero;

            isDashing = false;
            trailRend.emitting = false;
        }
    }
}
