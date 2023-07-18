using UnityEngine;

namespace Shunrald
{
    public class ShunraldMaterialChanger : MonoBehaviour
    {
        [SerializeField] Material initialMat, greyMat;

        private SkinnedMeshRenderer skinnedMeshRend;

        private float duration = 2f, lerp, currentLerpTime = 0f;
        private bool isChangingMaterial = false;

        private void Awake()
        {
            GetRequiredComponents();
        }

        private void GetRequiredComponents()
        {
            skinnedMeshRend = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        private void Update()
        {
            CheckMaterialChanging();

            //if (Input.GetKeyDown(KeyCode.K)) { ChangeShunraldMaterial(); }
        }

        private void CheckMaterialChanging()
        {
            if (isChangingMaterial)
            {
                currentLerpTime += Time.deltaTime;

                if (currentLerpTime >= duration)
                {
                    currentLerpTime = duration;
                    isChangingMaterial = false;
                }

                lerp = currentLerpTime / duration;
                skinnedMeshRend.material.Lerp(initialMat, greyMat, lerp);
            }
        }

        // This code must be invoked when our witch was killed and turned to stone by the Weeping Angel.
        public void ChangeShunraldMaterial()
        {
            if (!isChangingMaterial)
            {
                isChangingMaterial = true;
                currentLerpTime = 0f;
            }
        }

        // The material should be reset when the game restarts.
        public void ResetShunraldMaterial()
        {
            if (isChangingMaterial)
            {
                isChangingMaterial = false;
                currentLerpTime = 0f;
            }

            skinnedMeshRend.material = initialMat;
        }
    }
}

