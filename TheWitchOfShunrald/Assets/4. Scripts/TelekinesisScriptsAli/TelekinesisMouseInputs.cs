using Shunrald;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace ali
{
    public class TelekinesisMouseInputs : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToCreate;
        private GameObject createdPrefab;
        private SpringJoint connectedSpringJoint;
        private Rigidbody connectedObstacleRB;
        private bool isRightClicking = false;

        private Vector3 pullingPoint;

        [SerializeField] private Camera mainCamera;
        private int objectLayer = 13;
        private GameObject previousPointedObject;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                isRightClicking = true;
                GameManager.instance.Player.GetComponent<ShunraldMovementController>().IsUsingWeapon = true;
                if (previousPointedObject != null)
                {
                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitForInstantiation;
                    if (Physics.Raycast(ray, out hitForInstantiation))
                    {
                        pullingPoint = hitForInstantiation.point;
                        createdPrefab = Instantiate(prefabToCreate, hitForInstantiation.point, Quaternion.identity);

                        Vector3 localPosition = previousPointedObject.transform.InverseTransformPoint(pullingPoint);
                        connectedObstacleRB = previousPointedObject.GetComponent<Rigidbody>();
                        connectedSpringJoint = previousPointedObject.GetComponent<SpringJoint>();
                        connectedSpringJoint.anchor = localPosition;
                        connectedSpringJoint.connectedBody = createdPrefab.GetComponent<Rigidbody>();
                        connectedObstacleRB.drag = 3;
                        connectedObstacleRB.angularDrag = 5;
                        connectedSpringJoint.spring = 10f;
                    } 
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                isRightClicking = false;
                GameManager.instance.Player.GetComponent<ShunraldMovementController>().IsUsingWeapon = false;
                if (createdPrefab != null)
                {
                    connectedObstacleRB.drag = 0;
                    connectedObstacleRB.angularDrag = 0.05f;
                    connectedSpringJoint.spring = 0f;
                    connectedObstacleRB = null;
                    connectedSpringJoint = null;
                    // Destroy the created prefab
                    Destroy(createdPrefab);
                }
            }
        }

        private void FixedUpdate()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == objectLayer)
                {
                    MoveableObjectScript moveableObjectScript = hit.collider.gameObject.GetComponent<MoveableObjectScript>();

                    if (moveableObjectScript != null)
                    {
                        if (previousPointedObject != hit.collider.gameObject)
                        {
                            if (previousPointedObject != null)
                            {
                                MoveableObjectScript previousMoveableObjectScript = previousPointedObject.GetComponent<MoveableObjectScript>();
                                previousMoveableObjectScript.IsObjectPointed = false;
                            }

                            moveableObjectScript.IsObjectPointed = true;
                            previousPointedObject = hit.collider.gameObject;
                        }
                    }
                }
                else
                {
                    if (previousPointedObject != null)
                    {
                        MoveableObjectScript previousMoveableObjectScript = previousPointedObject.GetComponent<MoveableObjectScript>();
                        previousMoveableObjectScript.IsObjectPointed = false;
                        previousPointedObject = null;
                    }
                }
                if (isRightClicking && createdPrefab != null)
                {
                    Vector3 newPosition = new Vector3(hit.point.x, 3, hit.point.z);
                    createdPrefab.transform.position = newPosition;
                }
            }
            else
            {
                if (previousPointedObject != null)
                {
                    MoveableObjectScript previousMoveableObjectScript = previousPointedObject.GetComponent<MoveableObjectScript>();
                    previousMoveableObjectScript.IsObjectPointed = false;
                    previousPointedObject = null;
                }
            }
            
        }
    }
}

