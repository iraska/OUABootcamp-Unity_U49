using CameraSystem;
using System.Collections;
using UnityEngine;

namespace Shunrald
{
    public class ShunraldMovementController : MonoBehaviour
    {
        [SerializeField] private TrailRenderer trailRend;
        [SerializeField] private Camera lensCam;

        [SerializeField] private LayerMask aimLayerMask;

        [SerializeField] private float speed, turnSpeed = 360, dashSpeed, dashTime;

        private ShunraldController controller;
        private Animator animator;
        private Rigidbody rb;

        private Vector3 targetPosition, input;

        private float velocityX, velocityZ;
        private bool isDashing, isUsingWeapon, isMovementFrozen = false;
        public bool IsUsingWeapon
        {
            get { return isUsingWeapon; }
            set { isUsingWeapon = value; }
        }

        private void Awake()
        {
            GetRequiredComponent();
        }

        // for physics
        private void FixedUpdate()
        {
            GatherInput();
            ShunraldMovement();

            if (isDashing) { StartCoroutine(Dash()); }
        }

        private void Update()
        {
            if (!isUsingWeapon) 
            {
                AimTowardMouse();
            }
            
            //LookIsometric();

            controller.Animation.AnimateCharacter(input, velocityX, velocityZ, animator);

            if (Input.GetKeyDown(KeyCode.Space)) { isDashing = true; }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                FreezeShunraldMovement();
            }
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
        private void ShunraldMovement()
        {
            if (input.magnitude > 0f && !isMovementFrozen)
            {
                input.Normalize();

                Vector3 movement = RotateVector(input, -1 * lensCam.transform.parent.eulerAngles.y) * speed * Time.deltaTime;
                Vector3 newPosition = rb.position + movement;

                targetPosition = Vector3.Lerp(rb.position, newPosition, 0.9f);
                rb.MovePosition(targetPosition);
            }
            else if (!isDashing)
            {
                // Stop motion only if still and not dash
                rb.velocity = Vector3.zero;
            }
        }

        // This method must be called when the Witch is killed and petrified by the Weeping angel.
        public void FreezeShunraldMovement()
        {
            // Reset the character's speed
            isMovementFrozen = true;
            rb.velocity = Vector3.zero; 
        }

        private Vector3 RotateVector(Vector3 vector, float angle)
        {
            float angleRad = angle * Mathf.Deg2Rad; // Convert angle to radians
            float sin = Mathf.Sin(angleRad);
            float cos = Mathf.Cos(angleRad);

            float newX = vector.x * cos - vector.z * sin;
            float newZ = vector.x * sin + vector.z * cos;

            return new Vector3(newX, vector.y, newZ);
        }

        private IEnumerator Dash()
        {
            float startTime = Time.time;

            Vector3 dashVelocity = (targetPosition - transform.position).normalized * dashSpeed;

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
    }
}
