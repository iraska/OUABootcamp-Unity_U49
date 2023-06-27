using Shunrald;
using System;
using UnityEngine;

namespace WeepingAngle
{
    public class WeepingAngelMovementController : MonoBehaviour
    {
        [SerializeField] private float chaseSpeed = 5f;

        [SerializeField] Transform cube;
        [SerializeField] private float maxViewAngle = 45f;

        private WeepingAngleController wController;
        private ShunraldController sController;
        private Animator animator;

        private Vector3 chaseDirection;

        private const string shunrald = "Shunrald";

        private float wblend;
        private bool isAngelVisible;

        private void Awake()
        {
            GetRequiredComponenent();
        }

        private void Update()
        {
            isAngelVisible = CheckAngelVisibility();

            if (isAngelVisible)
            {
                Debug.Log("fuckin' freeze");
                //wController.Animation.AnimateAngel(0f, animator);
                wController.Animation.FreezeAngel();
            }
            else
            {
                /*chaseDirection = sController.transform.position - transform.position;
                transform.Translate(chaseDirection.normalized * chaseSpeed * Time.deltaTime);*/

                //wController.Animation.AnimateAngel(1f, animator);

                Debug.Log("i can not seeeee");
                wController.Animation.AnimateAngel(1f, animator);

            }

            //wController.Animation.AnimateAngel(1f, animator);


        }

        private void GetRequiredComponenent()
        {
            wController = GetComponent<WeepingAngleController>();
            sController = GetComponent<ShunraldController>();
            animator = GetComponent<Animator>();
        }

        private bool CheckAngelVisibility()
        {
            // Checking for someone in line of sight using Raycasting
            RaycastHit hit;
            //chaseDirection = sController.transform.position - transform.position;

            chaseDirection = cube.position - transform.position;

            if (Vector3.Angle(chaseDirection, transform.forward) <= maxViewAngle)
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
