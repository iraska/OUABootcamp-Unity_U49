using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        Destroy(gameObject, 2f);
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
