using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManDialogue : MonoBehaviour
{
    [SerializeField] GameObject dissapearParticle;
    [SerializeField] Sprite witchSprite;
    [SerializeField] Sprite oldManSprite;
    [SerializeField] Sprite necromancerSprite;
    [SerializeField] private List<AudioClip> dialogues = new List<AudioClip>();

    public void StartLevel1Dialogue()
    {
        DialogSystem.DialogStruct[] level1Dialogue = new DialogSystem.DialogStruct[9];

        //Dialogues
        level1Dialogue[0].text = "Can you hear me, old man?";
        level1Dialogue[0].name = "The Witch of Shunrald";
        level1Dialogue[0].icon = witchSprite;
        level1Dialogue[0].audioClip = dialogues[0];
        level1Dialogue[0].dialogCaller = this.gameObject;

        level1Dialogue[1].text = "Melinda?";
        level1Dialogue[1].name = "The Old Man";
        level1Dialogue[1].icon = oldManSprite;
        level1Dialogue[1].audioClip = dialogues[1];

        level1Dialogue[2].text = "No. I am afraid not.";
        level1Dialogue[2].name = "The Witch of Shunrald";
        level1Dialogue[2].icon = witchSprite;
        level1Dialogue[2].audioClip = dialogues[2];

        level1Dialogue[3].text = "Oh… Run, you fool. Before they come, before he finds you… Run.";
        level1Dialogue[3].name = "The Old Man";
        level1Dialogue[3].icon = oldManSprite;
        level1Dialogue[3].audioClip = dialogues[3];

        level1Dialogue[4].text = "I am far from being helpless. In fact, he should have run through here just now. Which way did he go?";
        level1Dialogue[4].name = "The Witch of Shunrald";
        level1Dialogue[4].icon = witchSprite;
        level1Dialogue[4].audioClip = dialogues[4];

        level1Dialogue[5].text = "Run? No! He does not run… He moves and the shadows move with him. He is the devil, he must be. I cannot be saved, justs run away.";
        level1Dialogue[5].name = "The Old Man";
        level1Dialogue[5].icon = oldManSprite;
        level1Dialogue[5].audioClip = dialogues[5];

        level1Dialogue[6].text = "Let your fears subside, in soil they're bound. Clear your thoughts and express what's most profound.";
        level1Dialogue[6].name = "The Witch of Shunrald";
        level1Dialogue[6].icon = witchSprite;
        level1Dialogue[6].audioClip = dialogues[6];

        level1Dialogue[7].text = "… He owns the dead. …";
        level1Dialogue[7].name = "The Old Man";
        level1Dialogue[7].icon = oldManSprite;
        level1Dialogue[7].audioClip = dialogues[7];

        level1Dialogue[8].text = "Enough!";
        level1Dialogue[8].name = "The Necromancer";
        level1Dialogue[8].icon = necromancerSprite;
        level1Dialogue[8].audioClip = dialogues[8];

        UIManager.instance.DialogPanel(level1Dialogue);
    }

    public void StartWalkingTowards()
    {
        gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        StartCoroutine(WalkTowardsDestination());
    }

    private IEnumerator WalkTowardsDestination()
    {
        while (false)
        {
            yield return null;
        }
    }
}
