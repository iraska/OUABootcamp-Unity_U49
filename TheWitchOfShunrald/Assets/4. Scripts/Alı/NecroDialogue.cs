using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroDialogue : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject dissapearParticle;
    [SerializeField] Transform destination;
    [SerializeField] Sprite witchSprite;
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

        level1Dialogue[1].text = "Aren’t you a charming boy. I have to say, it’s a very unique place you have. A bit too crowded with the dead, but otherwise bearable.";
        level1Dialogue[1].name = "The Witch of Shunrald";
        level1Dialogue[1].icon = witchSprite;
        level1Dialogue[1].audioClip = dialogues[1];

        level1Dialogue[2].text = "Well, I always wanted a big family. Who would have known… My family was under my foot this whole time.";
        level1Dialogue[2].name = "The Necromancer";
        level1Dialogue[2].icon = necromancerSprite;
        level1Dialogue[2].audioClip = dialogues[2];

        level1Dialogue[3].text = "It's a pity to be distant from loved ones, don't you agree? I can assist in reuniting you, just wait and see...";
        level1Dialogue[3].name = "The Witch of Shunrald";
        level1Dialogue[3].icon = witchSprite;
        level1Dialogue[3].audioClip = dialogues[3];

        level1Dialogue[4].text = "Witch! Your wicked mind tricks won’t affect me.";
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

    public void StartWalkingTowards()
    {
        gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        transform.LookAt(destination.position);
        StartCoroutine(WalkTowardsDestination());
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
