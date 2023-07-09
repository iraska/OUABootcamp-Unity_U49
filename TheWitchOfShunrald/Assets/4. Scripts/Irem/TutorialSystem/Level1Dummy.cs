using System.Collections;
using UnityEngine;

namespace TutorialSystem
{
    // The only task of this script is to run wasd tutorial on lv1
    public class Level1Dummy : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(TutorialManager.instance.InitialTutorialPanel(2f));
        }

        // Tutorial Canvas Ok button call this function
        public void DestroyThisObject()
        {
            Destroy(gameObject);
        }
    }
}
