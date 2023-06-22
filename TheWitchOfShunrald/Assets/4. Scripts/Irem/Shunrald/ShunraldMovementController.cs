using CameraSystem;
using UnityEngine;

namespace Shunrald
{
    public class ShunraldMovementController : MonoBehaviour
    {
        [SerializeField] private LayerMask aimLayerMask;
        [SerializeField] private float speed = 3f, turnSpeed = 360;

        private ShunraldController controller;
        private Animator animator;
        private Rigidbody rb;

        private Vector3 input;
        private float velocityX, velocityZ;

        private void Awake()
        {
            GetRequiredComponent();
        }

        // for physics
        private void FixedUpdate()
        {
            GatherInput();
            CharacterMovement();
        }

        private void Update()
        {
            //AimTowardMouse();
            LookIsometric();

            controller.Animation.AnimateCharacter(input, velocityX, velocityZ, animator);
        }

        private void GetRequiredComponent()
        {
            controller = GetComponent<ShunraldController>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        // Character always facing mouse cursor position
        private void AimTowardMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
            {
                var direction = hitInfo.point - transform.position;
                direction.y = 0f;
                direction.Normalize();
                transform.forward = direction;
            }
        }

        // Reading the input
        private void GatherInput()
        {
            input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        // Moving the character
        private void CharacterMovement()
        {
            if (input.magnitude > 0f)
            {
                input.Normalize();

                Vector3 movement = input.ToIso() * speed * Time.deltaTime;
                rb.MovePosition(rb.position + movement);
            }
            /*else
            {
                rb.velocity = Vector3.zero; 
            }*/
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
