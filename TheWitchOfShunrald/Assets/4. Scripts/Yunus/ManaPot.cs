using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPot : MonoBehaviour
{
    [SerializeField] private float mana;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Shunrald"))
        {
            other.transform.parent.GetComponent<PlayerStats>().ManaPot(mana);
            Destroy(gameObject);
        }
    }
}
