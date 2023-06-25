using Shunrald;
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
        [SerializeField] private GameObject playerProjectilePrefab;
        [SerializeField] private GameObject weaponPivot;
        [SerializeField] private GameObject reverseGravityPoint;

        [SerializeField] private GameObject topParticleObject;

        [SerializeField] private int particleType;
        [SerializeField] private GameObject energyParticleObject;
        [SerializeField] private GameObject fireParticleObject;

        [SerializeField] private Transform cameraPivotTransform;
        [SerializeField] private Transform staffTopPoint;
        [SerializeField] private Rigidbody staffTopRB;
        [SerializeField] private float powerMultiplier;
        private float powerMagnitude;
        private Vector3 lastStaffTopPoint;
        private bool firstTimeClick;

        [SerializeField] private Collider staffColider;
        [SerializeField] private Collider playerCollider;
        [SerializeField] private Collider staffTopSphereColider;

        [SerializeField] bool isStaffEquipped;

        //WeaponUI
        [SerializeField] private Image circleImage;
        [SerializeField] private RectTransform lineRectTransform;
        [SerializeField] private float maxLineLength = 200f;
        private Vector2 startDragPosition;

        void Start ()
        {
            powerMagnitude = 0;
            Physics.IgnoreCollision(staffColider, playerCollider, true);
            Physics.IgnoreCollision(staffTopSphereColider, playerCollider, true);

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                DialogSystem.DialogStruct [] birinciDiyaloglar = new DialogSystem.DialogStruct[3];
                for (int i = 0; i < 3; i++)
                {
                    birinciDiyaloglar[i].text = i + ". diyalog metni";
                    birinciDiyaloglar[i].name = "The Witch of Shunrald";
                    birinciDiyaloglar[i].icon = null;
                }

                UIManager.instance.DialogPanel(birinciDiyaloglar);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (particleType == 0)
                {
                    energyParticleObject.SetActive(true);
                    fireParticleObject.SetActive(false);
                }
                else
                {
                    energyParticleObject.SetActive(false);
                    fireParticleObject.SetActive(true);
                }
                initialMousePosition = Input.mousePosition;
                lastStaffTopPoint = staffTopPoint.position;


                //WeaponUI
                startDragPosition = new Vector2(initialMousePosition.x, initialMousePosition.y);
                circleImage.rectTransform.position = startDragPosition;
                circleImage.gameObject.SetActive(true);
                lineRectTransform.gameObject.SetActive(true);
            }

            else if (Input.GetMouseButton(0))
            {
                GameManager.instance.Player.GetComponent<ShunraldMovementController>().IsUsingWeapon = true;
                //Change the position of the weapon based on the input
                mouseMovementVector = new Vector2 (Input.mousePosition.x, Input.mousePosition.y) - initialMousePosition;
                if (mouseMovementVector == Vector2.zero)
                {
                    mouseMovementVector = initialMousePosition - new Vector2 (960, 540);
                }
                float rotationValue = cameraPivotTransform.rotation.eulerAngles.y;
                Vector3 changedLocalPosition = new Vector3(mouseMovementVector.normalized.x * 3, 0, mouseMovementVector.normalized.y * 3);
                Vector3 changedReverseGravityLocation = new Vector3(mouseMovementVector.normalized.x * 1, 3, mouseMovementVector.normalized.y * 1);
                reverseGravityPoint.transform.localPosition = RotateVector(changedReverseGravityLocation, -1 * rotationValue);
                weaponPivot.transform.localPosition = RotateVector(changedLocalPosition, -1 * rotationValue);

                //Track the movement of the wand for deciding its powerMagnitude for the ranged attack
                if (isStaffEquipped)
                {
                    if (powerMagnitude > 100f)
                    {
                        powerMagnitude = 100f;
                    }
                    else if (powerMagnitude > 50)
                    {
                        powerMagnitude += Vector3.Distance(lastStaffTopPoint, staffTopPoint.position);
                    }
                    else
                    {
                        powerMagnitude += Vector3.Distance(lastStaffTopPoint, staffTopPoint.position) * 2;
                    }
                    lastStaffTopPoint = staffTopPoint.position;

                    //Resize the effects based on powerMagnitude
                    topParticleObject.transform.localScale = new Vector3(powerMagnitude / 100, powerMagnitude / 100, powerMagnitude / 100);
                }
                

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
                GameManager.instance.Player.GetComponent<ShunraldMovementController>().IsUsingWeapon = false;
                //launch the projectile if staff is equipped
                if (isStaffEquipped)
                {
                    GameObject spawnedPlayerProjectile = Instantiate(playerProjectilePrefab);
                    spawnedPlayerProjectile.transform.position = staffTopRB.transform.position;
                    spawnedPlayerProjectile.GetComponent<RangedAttackProjectileScript>().PlayerProjectileDestination = staffTopRB.velocity;
                    spawnedPlayerProjectile.GetComponent<RangedAttackProjectileScript>().ProjectilePowerMagnitude = powerMagnitude;
                    spawnedPlayerProjectile.GetComponent<RangedAttackProjectileScript>().ProjecileType = particleType;

                    //reset the magnitude and the VFX
                    powerMagnitude = 0;
                    topParticleObject.transform.localScale = Vector3.zero;
                }

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





