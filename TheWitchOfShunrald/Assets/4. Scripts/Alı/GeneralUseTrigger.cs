using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneralUseTrigger : MonoBehaviour
{

    [SerializeField] UnityEvent generalUseTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shunrald"))
        {
            generalUseTriggerEvent.Invoke();
        }
    }
}
