using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace TutorialSystem
{
    public class TriggerController : MonoBehaviour
    {
        [SerializeField] private VideoClip gif;
    
        private GameObject tutorialInfoText;

        private const string shunrald = "Shunrald", 
            verge = "Verge", dash = "Dash", skill = "Skill", destructible = "Destructible";

        private string currentInfoText;

        private void OnTriggerEnter(Collider other)
        {
            TutorialManager.instance.GifTutorialPanel(gif);

            tutorialInfoText = FindObjectOfType<TutorialInfo>().gameObject;

            if (other.gameObject.CompareTag(shunrald))
            {
                if (gameObject.CompareTag(verge)) 
                {
                    currentInfoText = "You can attack the enemy by turning your wand around, as in the gif.";
                }
                else if (gameObject.CompareTag(dash))
                {
                    currentInfoText = "You can dash by pressing the space key as in the gif.";
                }
                else if (gameObject.CompareTag(skill)) 
                {
                    currentInfoText = "By pressing the 3 key, you can throw a skill as in the gif.";
                }
                else if (gameObject.CompareTag(destructible))
                {
                    currentInfoText = "Just like in the gif, you can put objects in front of the enemies by clicking the right button of your mouse and distract them.";
                }
            
                ChangeText(currentInfoText);
                Destroy(gameObject);
            }
        }

        private void ChangeText(string text)
        {
            if (tutorialInfoText != null)
            {
                tutorialInfoText.GetComponent<TextMeshProUGUI>().text = text;
            }
        }
    }
}
