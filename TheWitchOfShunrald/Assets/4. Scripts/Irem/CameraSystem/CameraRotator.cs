using TMPro;
using UnityEngine;

namespace CameraSystem
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 2f, initialYRot, smoothSpeed = 10f;

        private Transform parentTransform;

        private float currentYRot, targetYRot;
        private bool isRotating = false;


        private void Start()
        {
            GetRequiredComponents();
        }

        /*private void LateUpdate()
        {
            RotateCamera();
        }*/

        private void GetRequiredComponents()
        {
            parentTransform = transform.parent;
            initialYRot = parentTransform.eulerAngles.y;

            targetYRot = initialYRot;
            currentYRot = initialYRot;
        }

        private void RotateCamera()
        {
            if (Input.GetMouseButtonDown(2)) { isRotating = true; }
            else if (Input.GetMouseButtonUp(2)) { isRotating = false; }

            if (isRotating)
            {
                float mouseX = Input.GetAxis("Mouse X");
                targetYRot += mouseX * sensitivity;

                currentYRot = Mathf.Lerp(currentYRot, targetYRot, smoothSpeed * Time.deltaTime);

                Vector3 newRot = parentTransform.eulerAngles;
                newRot.y = currentYRot;
                parentTransform.eulerAngles = newRot;
            }
        }
    }
}
