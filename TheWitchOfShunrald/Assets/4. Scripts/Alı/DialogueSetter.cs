using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSetter : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int levelNo;
    [SerializeField] GameObject dissapearParticle;
    [SerializeField] GameObject somethingToBeEnebled;
    [SerializeField] Transform destination;
    [SerializeField] Sprite witchSprite;
    [SerializeField] Sprite oldManSprite;
    [SerializeField] Sprite henricSprite;
    [SerializeField] Sprite necromancerSprite;

    [SerializeField] private List<AudioClip> dialogues = new List<AudioClip>();

    public void StartLevel1Dialogue()
    {
        DialogSystem.DialogStruct [] level1Dialogue = new DialogSystem.DialogStruct[7];
        
        //Dialogues
        level1Dialogue[0].text = "Aah.. The Witch of Shunrald, what a pleasent company to have. Welcome to my home my lady.";
        level1Dialogue[0].name = "The Necromancer";
        level1Dialogue[0].icon = necromancerSprite;
        level1Dialogue[0].audioClip = dialogues[0];
        level1Dialogue[0].dialogCaller = this.gameObject;

        level1Dialogue[1].text = "Aren�t you a charming boy. I have to say, it�s a very unique place you have. A bit too crowded with the dead, but otherwise bearable.";
        level1Dialogue[1].name = "The Witch of Shunrald";
        level1Dialogue[1].icon = witchSprite;
        level1Dialogue[1].audioClip = dialogues[1];

        level1Dialogue[2].text = "Well, I always wanted a big family. Who would have known� My family was under my foot this whole time.";
        level1Dialogue[2].name = "The Necromancer";
        level1Dialogue[2].icon = necromancerSprite;
        level1Dialogue[2].audioClip = dialogues[2];

        level1Dialogue[3].text = "It's a pity to be distant from loved ones, don't you agree? I can assist in reuniting you, just wait and see...";
        level1Dialogue[3].name = "The Witch of Shunrald";
        level1Dialogue[3].icon = witchSprite;
        level1Dialogue[3].audioClip = dialogues[3];

        level1Dialogue[4].text = "Witch! Your wicked mind tricks won�t affect me.";
        level1Dialogue[4].name = "The Necromancer";
        level1Dialogue[4].icon = necromancerSprite;
        level1Dialogue[4].audioClip = dialogues[4];

        level1Dialogue[5].text = "Interesting, you know who I am, yet you speak to me so freely. I am here to hunt you down little boy.";
        level1Dialogue[5].name = "The Witch of Shunrald";
        level1Dialogue[5].icon = witchSprite;
        level1Dialogue[5].audioClip = dialogues[5];

        level1Dialogue[6].text = "Come then. I am planning to be a nice host. I have prepared some entertainment for you.";
        level1Dialogue[6].name = "The Necromancer";
        level1Dialogue[6].icon = necromancerSprite;
        level1Dialogue[6].audioClip = dialogues[6];

        UIManager.instance.DialogPanel(level1Dialogue);
    }

    public void StartLevel3Dialogue()
    {
        DialogSystem.DialogStruct[] level3Dialogue = new DialogSystem.DialogStruct[9];

        //Dialogues
        level3Dialogue[0].text = "Can you hear me, old man?";
        level3Dialogue[0].name = "The Witch of Shunrald";
        level3Dialogue[0].icon = witchSprite;
        level3Dialogue[0].audioClip = dialogues[0];
        level3Dialogue[0].dialogCaller = this.gameObject;
        
        level3Dialogue[1].text = "Melinda?";
        level3Dialogue[1].name = "The Old Man";
        level3Dialogue[1].icon = oldManSprite;
        level3Dialogue[1].audioClip = dialogues[1];
        
        level3Dialogue[2].text = "No. I am afraid not.";
        level3Dialogue[2].name = "The Witch of Shunrald";
        level3Dialogue[2].icon = witchSprite;
        level3Dialogue[2].audioClip = dialogues[2];
        
        level3Dialogue[3].text = "Oh� Run, you fool. Before they come, before he finds you� Run.";
        level3Dialogue[3].name = "The Old Man";
        level3Dialogue[3].icon = oldManSprite;
        level3Dialogue[3].audioClip = dialogues[3];
        
        level3Dialogue[4].text = "I am far from being helpless. In fact, he should have run through here just now. Which way did he go?";
        level3Dialogue[4].name = "The Witch of Shunrald";
        level3Dialogue[4].icon = witchSprite;
        level3Dialogue[4].audioClip = dialogues[4];
        
        level3Dialogue[5].text = "Run? No! He does not run� He moves and the shadows move with him. He is the devil, he must be. I cannot be saved, justs run away.";
        level3Dialogue[5].name = "The Old Man";
        level3Dialogue[5].icon = oldManSprite;
        level3Dialogue[5].audioClip = dialogues[5];
        
        level3Dialogue[6].text = "Let your fears subside, in soil they're bound. Clear your thoughts and express what's most profound.";
        level3Dialogue[6].name = "The Witch of Shunrald";
        level3Dialogue[6].icon = witchSprite;
        level3Dialogue[6].audioClip = dialogues[6];
        
        level3Dialogue[7].text = "� He owns the dead. �";
        level3Dialogue[7].name = "The Old Man";
        level3Dialogue[7].icon = oldManSprite;
        level3Dialogue[7].audioClip = dialogues[7];
        
        level3Dialogue[8].text = "Enough!";
        level3Dialogue[8].name = "The Necromancer";
        level3Dialogue[8].icon = necromancerSprite;
        level3Dialogue[8].audioClip = dialogues[8];

        UIManager.instance.DialogPanel(level3Dialogue);
    }

    public void TakeAction()
    {
        if (levelNo == 1)
        {
            //necromancer goes out from the level
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            transform.LookAt(destination.position);
            StartCoroutine(WalkTowardsDestination());
        }
        else if (levelNo == 3)
        {
            //old man dies and fight begins
            Vector3 newRotation = new Vector3(10.509f, 118.873f, 0);
            Vector3 newPosition = new Vector3(-7.264f, 1.042f, -1.401f);
            this.transform.rotation = Quaternion.Euler(newRotation);
            this.transform.position = newPosition;
            GetComponent<Animator>().SetTrigger("startDeath");
            somethingToBeEnebled.SetActive(true);
        }
        else if (levelNo == 4)
        {
            //bridge falls and fight begins, necromancer leaves
        }
        else if (levelNo == 5)
        {
            //necromancer holds witch up, minions spawn and attack
        }

    }

    private IEnumerator WalkTowardsDestination()
    {
        while ((destination.position - transform.position).magnitude > 0.1f)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step);
            if ((destination.position - transform.position).magnitude <= 0.1f)
            {
                GameObject particle = Instantiate(dissapearParticle);
                particle.transform.position = destination.position;
                //destroy gameobject
                Destroy(gameObject);
            }
            yield return null;
        }
    }


    
}
