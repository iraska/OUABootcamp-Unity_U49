using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{

    [SerializeField] int upgradePoints;

    private void Start()
    {
        GameManager.instance.CanGoTheNextLevel = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shunrald"))
        {
            GameManager.instance.CanGoTheNextLevel = true;
            if (GameManager.instance.CanGoTheNextLevel)
            {
                GameManager.instance.Upgrade(upgradePoints);
            }
        }
    }
}
