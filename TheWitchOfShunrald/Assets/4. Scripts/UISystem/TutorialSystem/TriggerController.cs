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
            verge = "Verge", dash = "Dash", skill = "Skill", destructible = "Destructible", sword = "Sword";

        private string currentInfoText;

        private void OnTriggerEnter(Collider other)
        {


            if (other.gameObject.CompareTag(shunrald))
            {
                //TutorialManager.instance.GifTutorialPanel(gif);
                TutorialManager.instance.GifTutorialPanel(gameObject.tag);

                tutorialInfoText = FindObjectOfType<TutorialInfo>().gameObject;

                if (gameObject.CompareTag(verge)) 
                {
                    currentInfoText = "Use your mouse, left click and drag for attack. Brighter the staff, greater the damage will be.";
                }
                else if (gameObject.CompareTag(dash))
                {
                    currentInfoText = "You can dash by pressing the space key.";
                }
                else if (gameObject.CompareTag(skill)) 
                {
                    currentInfoText = "By pressing the E key, you can throw a skill with area damage.";
                }
                else if (gameObject.CompareTag(destructible))
                {
                    currentInfoText = "Right click and drag objects to distract your enemies and find yourself a cover.";
                }
                else if (gameObject.CompareTag(sword))
                {
                    currentInfoText = "Press Q for changing the weapon. You can use sword when you lack the mana for using the staff.";
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
