using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ali
{
    public class WeaponMovementHandler : MonoBehaviour
    {
        private Vector2 initialMousePosition;
        private Vector2 mouseMovementVector;
        [SerializeField] private GameObject weaponPivot;
        [SerializeField] private GameObject reverseGravityPoint;
        [SerializeField] private Transform cameraPivotTransform;


        //WeaponUI
        [SerializeField] private Image circleImage;
        [SerializeField] private RectTransform lineRectTransform;
        [SerializeField] private float maxLineLength = 200f;
        private Vector2 startDragPosition;


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = Input.mousePosition;

                //WeaponUI
                startDragPosition = new Vector2(initialMousePosition.x, initialMousePosition.y);
                circleImage.rectTransform.position = startDragPosition;
                circleImage.gameObject.SetActive(true);
                lineRectTransform.gameObject.SetActive(true);
            }

            else if (Input.GetMouseButton(0))
            {
                mouseMovementVector = new Vector2 (Input.mousePosition.x, Input.mousePosition.y) - initialMousePosition;
                float rotationValue = cameraPivotTransform.rotation.eulerAngles.y;
                Vector3 changedLocalPosition = new Vector3(mouseMovementVector.normalized.x * 3, 0, mouseMovementVector.normalized.y * 3);
                Vector3 changedReverseGravityLocation = new Vector3(mouseMovementVector.normalized.x * 1, 3, mouseMovementVector.normalized.y * 1);
                reverseGravityPoint.transform.localPosition = RotateVector(changedReverseGravityLocation, -1 * rotationValue);
                weaponPivot.transform.localPosition = RotateVector(changedLocalPosition, -1 * rotationValue);

                //WeaponUI
                Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 lineVector = currentMousePosition - startDragPosition;
                float lineLength = Mathf.Min(lineVector.magnitude, maxLineLength);
                lineVector = lineVector.normalized * lineLength;
                lineRectTransform.position = startDragPosition + lineVector / 2f;
                lineRectTransform.sizeDelta = new Vector2(lineLength, 2f);
                lineRectTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(lineVector.y, lineVector.x) * Mathf.Rad2Deg);
            }

            else if (Input.GetMouseButtonUp(0))
            {
                //Action

                //WeaponUI
                circleImage.gameObject.SetActive(false);
                lineRectTransform.gameObject.SetActive(false);
            }
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
    }
}

