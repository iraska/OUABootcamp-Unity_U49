using ali;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ali
{
    public class RangedAttackEnemyFinder : MonoBehaviour
    {
        private RangedAttackProjectileScript projectileScript;

        bool enemyFound = false;

        private void Start()
        {
            projectileScript = transform.parent.GetComponent<RangedAttackProjectileScript>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & projectileScript.TargetLayerMask) != 0 && enemyFound == false)
            {
                Debug.Log(other.gameObject.name);
                projectileScript.ProjectileFoundEnemy(other.gameObject);
                enemyFound = true;
            }
        }
    }

}
