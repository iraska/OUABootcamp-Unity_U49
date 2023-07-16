using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetectorScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(Vector3.zero, 10000);
        }
        else if (other.gameObject.layer == 12)
        {
            GameManager.instance.Player.GetComponent<PlayerStats>().TakeDamage(1000);
        }
    }
}
