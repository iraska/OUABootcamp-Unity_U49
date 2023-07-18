using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GeneralUseTrigger : MonoBehaviour
{

    [SerializeField] UnityEvent generalUseTriggerEvent;
    bool isInfoGiven;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shunrald"))
        {
            isInfoGiven = true;
            generalUseTriggerEvent.Invoke();
            if (GetComponent<SphereCollider>() != null)
            {
                GetComponent<SphereCollider>().radius += 3;
            }
        }
    }

    public void infoStarterForNecroDeath()
    {
        StartCoroutine(infoTextForEnd());
    }
    private IEnumerator infoTextForEnd()
    {
        yield return new WaitForSeconds(5f);
        isInfoGiven = false;
        UIManager.instance.InfoEnable("Face your enemy!");

        while (!isInfoGiven)
        {
            yield return new WaitForSeconds(0.5f);
        }
        UIManager.instance.InfoDisable();
    }


}
