using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : MonoBehaviour
{
    [SerializeField] private int health;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shunrald"))
        {
            other.transform.parent.GetComponent<PlayerStats>().HealthPot(health);
            Destroy(gameObject);
        }
    }
}
