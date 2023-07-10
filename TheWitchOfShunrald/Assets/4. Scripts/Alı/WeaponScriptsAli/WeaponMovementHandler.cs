using Shunrald;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ali
{
    public class WeaponMovementHandler : MonoBehaviour
    {
        [SerializeField] private GameObject playerGameObject;

        private Vector2 initialMousePosition;
        private Vector2 mouseMovementVector;
        [SerializeField] private GameObject playerProjectilePrefab;
        [SerializeField] private GameObject weaponPivot;
        [SerializeField] private GameObject reverseGravityPoint;

        [SerializeField] private GameObject topParticleObject;

        [SerializeField] private GameObject staffWeapon;
        [SerializeField] private GameObject swordWeapon;
        private float playerDamage;

        [SerializeField] private int particleType;
        [SerializeField] private GameObject energyParticleObject;
        [SerializeField] private GameObject fireParticleObject;

        [SerializeField] private Transform cameraPivotTransform;
        [SerializeField] private Transform staffTopPoint;
        [SerializeField] private Rigidbody staffTopRB;
        [SerializeField] private float powerMultiplier;
        [SerializeField] private float manaSpendMultiplier;
        private float powerMagnitude;
        private float manaSpend;
        private Vector3 lastStaffTopPoint;

        [SerializeField] private Collider staffColider;
        [SerializeField] private Collider playerCollider;
        [SerializeField] private Collider staffTopSphereColider;

        [SerializeField] private bool isStaffEquipped;
        [SerializeField] private float weaponTopPivotPositionValue;

        //WeaponUI
        private Image circleImage;
        private RectTransform lineRectTransform;
        [SerializeField] private float maxLineLength = 200f;
        private Vector2 startDragPosition;

        private ShunraldMovementController shunraldMovementController;
        private PlayerStats playerStats;

        void Start ()
        {
            playerGameObject = GameManager.instance.Player;
            circleImage = UIManager.instance.WeaponUICircle;
            lineRectTransform = UIManager.instance.WeaponUILine.rectTransform;
            powerMagnitude = 0;
            manaSpend = 0;
            Physics.IgnoreCollision(staffColider, playerCollider, true);
            Physics.IgnoreCollision(staffTopSphereColider, playerCollider, true);
            shunraldMovementController = playerGameObject.GetComponent<ShunraldMovementController>();
            playerStats = playerGameObject.GetComponent<PlayerStats>();
            playerDamage = playerStats.Damage;
        }


        void Update()
        {
            if(GameManager.instance.GameState == GameManager.State.Playing)
            {
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
                    shunraldMovementController.IsUsingWeapon = true;

                    //check mana
                    if (playerStats.Mana > 0f)
                    {
                        //Change the position of the weapon based on the input
                        mouseMovementVector = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - initialMousePosition;
                        if (mouseMovementVector == Vector2.zero)
                        {
                            mouseMovementVector = initialMousePosition - new Vector2(960, 540);
                        }
                        float rotationValue = cameraPivotTransform.rotation.eulerAngles.y;
                        Vector3 changedLocalPosition = new Vector3(mouseMovementVector.normalized.x * 3, 0, mouseMovementVector.normalized.y * 3);
                        Vector3 changedReverseGravityLocation = new Vector3(mouseMovementVector.normalized.x * 1, weaponTopPivotPositionValue, mouseMovementVector.normalized.y * 1);
                        reverseGravityPoint.transform.localPosition = RotateVector(changedReverseGravityLocation, -1 * rotationValue);
                        weaponPivot.transform.localPosition = RotateVector(changedLocalPosition, -1 * rotationValue);

                        if (manaSpend > 0.5)
                        {
                            playerStats.SpendMana(manaSpend);
                            manaSpend = 0;
                        }
                        else
                        {
                            manaSpend = manaSpend + (manaSpendMultiplier / 10 * Vector3.Distance(lastStaffTopPoint, staffTopPoint.position));
                        }


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


                            //Resize the effects based on powerMagnitude
                            topParticleObject.transform.localScale = new Vector3(powerMagnitude / 100, powerMagnitude / 100, powerMagnitude / 100);
                        }
                        lastStaffTopPoint = staffTopPoint.position;

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
                    shunraldMovementController.IsUsingWeapon = false;
                    //launch the projectile if staff is equipped
                    if (isStaffEquipped)
                    {

                        if (playerStats.Mana > 0f && GameManager.instance.Player.GetComponent<ShunraldMovementController>().IsUsingSkill == false)
                        {
                            GameObject spawnedPlayerProjectile = Instantiate(playerProjectilePrefab);
                            spawnedPlayerProjectile.transform.position = staffTopRB.transform.position;
                            spawnedPlayerProjectile.GetComponent<RangedAttackProjectileScript>().PlayerProjectileDestination = staffTopRB.velocity;
                            spawnedPlayerProjectile.GetComponent<RangedAttackProjectileScript>().ProjectilePowerMagnitude = powerMagnitude;
                            spawnedPlayerProjectile.GetComponent<RangedAttackProjectileScript>().ProjectilePlayerDamage = playerDamage;
                            spawnedPlayerProjectile.GetComponent<RangedAttackProjectileScript>().ProjecileType = particleType;
                            playerStats.SpendMana(5f);
                        }
                        //reset the magnitude and the VFX
                        powerMagnitude = 0;
                        topParticleObject.transform.localScale = Vector3.zero;

                    }

                    //WeaponUI
                    circleImage.gameObject.SetActive(false);
                    lineRectTransform.gameObject.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (isStaffEquipped)
                    {
                        swordWeapon.SetActive(true);
                        staffWeapon.SetActive(false);
                        UIManager.instance.WeaponImage(1);
                    }
                    else
                    {
                        staffWeapon.SetActive(true);
                        swordWeapon.SetActive(false);
                        UIManager.instance.WeaponImage(0);
                    }
                }
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





