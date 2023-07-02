using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    private float damage;
    public float Damage { set { damage = value;} }
    private bool isExploded;
    private void OnTriggerEnter(Collider collision)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        Destroy(gameObject, 2f);
        GetComponent<Rigidbody>().isKinematic = true;

        if(!isExploded)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.layer == 11)
                    hitCollider.gameObject.GetComponent<Enemy>().TakeDamage(transform.position, damage * 1.5f);
            }
            isExploded = true;
        }

    }
}
