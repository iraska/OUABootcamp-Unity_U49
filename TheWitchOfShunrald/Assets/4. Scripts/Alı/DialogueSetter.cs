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

    [Header("Level 4 Objects")]
    [SerializeField] GameObject bridgeToBeDestroyed;
    [SerializeField] GameObject doorToBeSlammed;
    [SerializeField] GameObject fakeAngels;
    [SerializeField] GameObject realAngels;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float timeElapsed = 0f;

    [Header("Level 5 Objects")]
    [SerializeField] GameObject fakeEnemies;
    [SerializeField] GameObject realEnemies;
    [SerializeField] GameObject fakeNecromancer;
    [SerializeField] GameObject realNecromancer;
    [SerializeField] GameObject necroHealthUI;


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
        
        level3Dialogue[3].text = "Oh… Run, you fool. Before they come, before he finds you… Run.";
        level3Dialogue[3].name = "The Old Man";
        level3Dialogue[3].icon = oldManSprite;
        level3Dialogue[3].audioClip = dialogues[3];
        
        level3Dialogue[4].text = "I am far from being helpless. In fact, he should have run through here just now. Which way did he go?";
        level3Dialogue[4].name = "The Witch of Shunrald";
        level3Dialogue[4].icon = witchSprite;
        level3Dialogue[4].audioClip = dialogues[4];
        
        level3Dialogue[5].text = "Run? No! He does not run… He moves and the shadows move with him. He is the devil, he must be. I cannot be saved, justs run away.";
        level3Dialogue[5].name = "The Old Man";
        level3Dialogue[5].icon = oldManSprite;
        level3Dialogue[5].audioClip = dialogues[5];
        
        level3Dialogue[6].text = "Let your fears subside, in soil they're bound. Clear your thoughts and express what's most profound.";
        level3Dialogue[6].name = "The Witch of Shunrald";
        level3Dialogue[6].icon = witchSprite;
        level3Dialogue[6].audioClip = dialogues[6];
        
        level3Dialogue[7].text = "… He owns the dead. …";
        level3Dialogue[7].name = "The Old Man";
        level3Dialogue[7].icon = oldManSprite;
        level3Dialogue[7].audioClip = dialogues[7];
        
        level3Dialogue[8].text = "Enough!";
        level3Dialogue[8].name = "The Necromancer";
        level3Dialogue[8].icon = necromancerSprite;
        level3Dialogue[8].audioClip = dialogues[8];

        UIManager.instance.DialogPanel(level3Dialogue);
    }

    public void StartLevel4Dialogue()
    {
        DialogSystem.DialogStruct[] level4Dialogue = new DialogSystem.DialogStruct[5];

        //Dialogues
        level4Dialogue[0].text = "Are you enjoying your stay my lady witch?";
        level4Dialogue[0].name = "The Necromancer";
        level4Dialogue[0].icon = necromancerSprite;
        level4Dialogue[0].audioClip = dialogues[0];
        level4Dialogue[0].dialogCaller = this.gameObject;

        level4Dialogue[1].text = "Justify yourself, do you truly think you'll slay me? With your limited knowledge, you seek the power of undead labour. I have seen such greater than you, rise and fall, far beyond your imagination.";
        level4Dialogue[1].name = "The Witch of Shunrald";
        level4Dialogue[1].icon = witchSprite;
        level4Dialogue[1].audioClip = dialogues[1];
        
        level4Dialogue[2].text = "Oh I know the limitations of my power and the extend of yours. You have quiet a name for yourself, a brand even. The Witch of Shunrald. You see, knowledge is on my side even tough you know so much more than me.";
        level4Dialogue[2].name = "The Necromancer";
        level4Dialogue[2].icon = necromancerSprite;
        level4Dialogue[2].audioClip = dialogues[2];
        
        level4Dialogue[3].text = "How typical. A young boy thinks he knows everything there is to know about me after hearing a bunch of stories.";
        level4Dialogue[3].name = "The Witch of Shunrald";
        level4Dialogue[3].icon = witchSprite;
        level4Dialogue[3].audioClip = dialogues[3];
        
        level4Dialogue[4].text = "Be at ease my lady witch. The time for realization is nigh. Rise my children. Rise!";
        level4Dialogue[4].name = "The Necromancer";
        level4Dialogue[4].icon = necromancerSprite;
        level4Dialogue[4].audioClip = dialogues[4];

        UIManager.instance.DialogPanel(level4Dialogue);
    }

    public void StartLevel5Dialogue()
    {
        DialogSystem.DialogStruct[] level5Dialogue = new DialogSystem.DialogStruct[6];

        //Dialogues
        level5Dialogue[0].text = "Many souls you have poured upon me Necromancer. The statues were not a boy’s toy. Unveil your name? Who tought you such spells that should have been forgotten many ages ago.";
        level5Dialogue[0].name = "The Witch of Shunrald";
        level5Dialogue[0].icon = witchSprite;
        level5Dialogue[0].audioClip = dialogues[0];
        level5Dialogue[0].dialogCaller = this.gameObject;
        
        level5Dialogue[1].text = "I am flattered my lady witch, but I won’t speak my name if it is all the same for you. Names are known to have powers. As for my arcane source of instructions, let’s say I have met with a ghost.";
        level5Dialogue[1].name = "The Necromancer";
        level5Dialogue[1].icon = necromancerSprite;
        level5Dialogue[1].audioClip = dialogues[1];
        
        level5Dialogue[2].text = "That would explain things. I cannot let you live with that kind of knowledge.";
        level5Dialogue[2].name = "The Witch of Shunrald";
        level5Dialogue[2].icon = witchSprite;
        level5Dialogue[2].audioClip = dialogues[2];
        
        level5Dialogue[3].text = "Obviously. I too cannot let you live. You are to be my greatest warrior. It is time for one of us to succeed.";
        level5Dialogue[3].name = "The Necromancer";
        level5Dialogue[3].icon = necromancerSprite;
        level5Dialogue[3].audioClip = dialogues[3];
        
        level5Dialogue[4].text = "You have already tried. You cannot kill me.";
        level5Dialogue[4].name = "The Witch of Shunrald";
        level5Dialogue[4].icon = witchSprite;
        level5Dialogue[4].audioClip = dialogues[4];

        level5Dialogue[5].text = "Well, you are already dead my lady witch. At least your hands are.";
        level5Dialogue[5].name = "The Necromancer";
        level5Dialogue[5].icon = necromancerSprite;
        level5Dialogue[5].audioClip = dialogues[5];


        UIManager.instance.DialogPanel(level5Dialogue);
    }

    public void StartLevel6Dialogue()
    {
        DialogSystem.DialogStruct[] level6Dialogue = new DialogSystem.DialogStruct[11];

        //Dialogues
        level6Dialogue[0].text = "That was clever of you. But you were not ready. Immpatience. The curse of young.";
        level6Dialogue[0].name = "The Witch of Shunrald";
        level6Dialogue[0].icon = witchSprite;
        level6Dialogue[0].audioClip = dialogues[0];
        level6Dialogue[0].dialogCaller = this.gameObject;
        
        level6Dialogue[1].text = "Henric! Help me!";
        level6Dialogue[1].name = "The Necromancer";
        level6Dialogue[1].icon = necromancerSprite;
        level6Dialogue[1].audioClip = dialogues[1];
        
        level6Dialogue[2].text = "Henric? You mean Henric the Inquisitor?";
        level6Dialogue[2].name = "The Witch of Shunrald";
        level6Dialogue[2].icon = witchSprite;
        level6Dialogue[2].audioClip = dialogues[2];
        
        level6Dialogue[3].text = "It is Henric the Ghost now.";
        level6Dialogue[3].name = "Henric";
        level6Dialogue[3].icon = henricSprite;
        level6Dialogue[3].audioClip = dialogues[3];
        
        level6Dialogue[4].text = "Bloody Moons! You are still chasing me!";
        level6Dialogue[4].name = "The Witch of Shunrald";
        level6Dialogue[4].icon = witchSprite;
        level6Dialogue[4].audioClip = dialogues[4];
        
        level6Dialogue[5].text = "I do, and I am not alone.";
        level6Dialogue[5].name = "Henric";
        level6Dialogue[5].icon = henricSprite;
        level6Dialogue[5].audioClip = dialogues[5];
        
        level6Dialogue[6].text = "You have killed innocent people to find me. You have damned their souls.";
        level6Dialogue[6].name = "The Witch of Shunrald";
        level6Dialogue[6].icon = witchSprite;
        level6Dialogue[6].audioClip = dialogues[6];
        
        level6Dialogue[7].text = "They have died for a greater good.";
        level6Dialogue[7].name = "Henric";
        level6Dialogue[7].icon = henricSprite;
        level6Dialogue[7].audioClip = dialogues[7];
        
        level6Dialogue[8].text = "Damn you and your greater good. The soil belove is my bed and the night above is my cushion. I am the child of this earth, and this country is my creation. You don’t belong here Henric the bloody inquisitor. I banish you from this world. Go back to your damnation.";
        level6Dialogue[8].name = "The Witch of Shunrald";
        level6Dialogue[8].icon = witchSprite;
        level6Dialogue[8].audioClip = dialogues[8];
        
        level6Dialogue[9].text = "I will abide by your craft. Yet I am not alone, witch. Expect trouble!";
        level6Dialogue[9].name = "Henric";
        level6Dialogue[9].icon = henricSprite;
        level6Dialogue[9].audioClip = dialogues[9];
        
        level6Dialogue[10].text = "Bloody fool, can’t even manage to die properly. As if a witch would be troubled by a dense ghost.";
        level6Dialogue[10].name = "The Witch of Shunrald";
        level6Dialogue[10].icon = witchSprite;
        level6Dialogue[10].audioClip = dialogues[10];


        UIManager.instance.DialogPanel(level6Dialogue);
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
            //bridge falls and fight begins, necromancer leaves and the door gets closed
            bridgeToBeDestroyed.SetActive(false);

            //necromancer goes out from the level
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            transform.LookAt(destination.position);
            StartCoroutine(WalkTowardsDestination());

            // angels start living
            fakeAngels.SetActive(false);
            realAngels.SetActive(true);

            //door gets slammed
            startRotation = Quaternion.Euler(-90f, 0f, -125f);
            targetRotation = Quaternion.Euler(-90f, 0f, 0f);
            StartCoroutine(RotateDoor());

            //spawner gets activated
            somethingToBeEnebled.SetActive(true);
        }
        else if (levelNo == 5)
        {
            fakeEnemies.SetActive(false);
            realEnemies.SetActive(true);

            realNecromancer.SetActive(true);
            necroHealthUI.SetActive(true);
            fakeNecromancer.SetActive(false);
        }

        else if (levelNo == 6)
        {
            StartCoroutine(WinTheGame());
        }

    }

    private IEnumerator WinTheGame()
    {
        //let the ghost dissappear
        yield return new WaitForSeconds(2f);
        GameManager.instance.Win();
    }

    public void LetFakeEnemiesWalk() 
    {
        StartCoroutine(WalkTheFakeEnemies());
    }

    private IEnumerator WalkTheFakeEnemies()
    {
        yield return new WaitForSeconds(2f);
        fakeEnemies.SetActive(true);
    }

    private IEnumerator RotateDoor()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            // Increment the time elapsed
            timeElapsed += Time.deltaTime;

            // Calculate the interpolation factor based on the current time elapsed and rotation duration
            float t = Mathf.Clamp01(timeElapsed / 0.95f);

            // Use Lerp to interpolate between the start rotation and target rotation
            Quaternion newRotation = Quaternion.Lerp(startRotation, targetRotation, t);

            // Apply the new rotation to the object
            doorToBeSlammed.transform.rotation = newRotation;

            // Complete rotation
            if (timeElapsed >= 2f)
            {
                Debug.Log(timeElapsed);
                yield break;
            }

            // Wait for the next frame
            yield return null;
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
