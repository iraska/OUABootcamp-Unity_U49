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

        [SerializeField] private float speed, turnSpeed = 360, dashSpeed, dashTime, dashCoolDown, dashManaburn;

        private ShunraldController controller;
        private Animator animator;
        private Rigidbody rb;

        private Vector3 targetPosition, input;

        private float velocityX, velocityZ;
        private bool isDashing, isUsingWeapon, isMovementFrozen = false, isDashCoolDown, isDeath, isUsingSkill;
        public bool IsUsingWeapon
        {
            get { return isUsingWeapon; }
            set { isUsingWeapon = value; }
        }
        public bool IsDeath
        {
            get { return isDeath; }
            set { isDeath = value; }
        }
        public bool IsUsingSkill
        {
            get { return isUsingSkill; }
            set { isUsingSkill = value; }
        }

        private void Awake()
        {
            GetRequiredComponent();
        }

        // for physics
        private void FixedUpdate()
        {
            if (!isDashing && !IsDeath)
            {
                GatherInput();
                ShunraldMovement();
            }
        }

        private void Update()
        {
            if (!isUsingWeapon && !IsDeath)
            {
                AimTowardMouse();
            }

            controller.Animation.AnimateCharacter(input, velocityX, velocityZ, animator);

            if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.GameState == GameManager.State.Playing)
            {
                if (!isDashing && rb.velocity.magnitude <= 0.01f && GameManager.instance.Player.GetComponent<PlayerStats>().Mana > 0 && !isDashCoolDown)
                {
                    GameManager.instance.Player.GetComponent<PlayerStats>().SpendMana(dashManaburn);
                    isDashCoolDown = true;
                    StartCoroutine(Dash());
                    StartCoroutine(DashCooldown());
                }
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
            if (!IsDeath)
            {
                Ray ray = lensCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
                {
                    var direction = hitInfo.point - transform.position;
                    direction.y = 0f;
                    direction.Normalize();

                    // Allows the character to dash in the direction of movement
                    if (!isDashing && rb.velocity.magnitude <= 0.01f)
                    {
                        transform.forward = direction;
                    }
                }
            }
        }

        // Moving the character
        private void ShunraldMovement()
        {
            if (GameManager.instance.GameState == GameManager.State.Playing)
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
            float angleRad = angle * Mathf.Deg2Rad;         // Convert angle to radians
            float sin = Mathf.Sin(angleRad);
            float cos = Mathf.Cos(angleRad);

            float newX = vector.x * cos - vector.z * sin;
            float newZ = vector.x * sin + vector.z * cos;

            return new Vector3(newX, vector.y, newZ);
        }

        private IEnumerator Dash()
        {
            isDashing = true;

            float startTime = Time.time;
            Vector3 dashDirection = input.normalized;

            // Adjust dash direction based on isometric camera angle
            dashDirection = RotateVector(dashDirection, -lensCam.transform.parent.eulerAngles.y);

            while (Time.time < startTime + dashTime)
            {
                rb.velocity = dashDirection * dashSpeed;
                trailRend.emitting = true;

                yield return new WaitForSeconds(.1f);
            }

            // Dash effect is over, slow speed to zero
            float decelerationTime = .05f;
            float elapsedTime = 0f;
            Vector3 initialVelocity = rb.velocity;

            while (elapsedTime < decelerationTime)
            {
                float time = elapsedTime / decelerationTime;
                rb.velocity = Vector3.Lerp(initialVelocity, Vector3.zero, time);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            rb.velocity = Vector3.zero;

            isDashing = false;
            trailRend.emitting = false;
        }

        private IEnumerator DashCooldown()
        {
            yield return new WaitForSeconds(dashCoolDown);
            isDashCoolDown = false;
        }

        public void StopedUsingSkill()
        {
            StartCoroutine(NotUsingSkill());
        }

        private IEnumerator NotUsingSkill()
        {
            yield return new WaitForSeconds(0.5f);
            isUsingSkill = false;
        }
    } 
}
