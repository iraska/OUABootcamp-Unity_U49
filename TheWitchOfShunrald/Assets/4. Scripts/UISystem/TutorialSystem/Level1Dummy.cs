using System.Collections;
using UnityEngine;

namespace TutorialSystem
{
    // The only task of this script is to run wasd tutorial on lv1
    public class Level1Dummy : MonoBehaviour
    {
        private void Start()
        {
            TutorialManager.instance.InvokeItialTutorial();
            StartCoroutine(destroyAfterLoad());
        }

        // Tutorial Canvas Ok button call this function
        public void DestroyThisObject()
        {
            
        }

        private IEnumerator destroyAfterLoad()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}
