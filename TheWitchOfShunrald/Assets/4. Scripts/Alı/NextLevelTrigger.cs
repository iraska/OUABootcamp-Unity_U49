using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{

    [SerializeField] int upgradePoints;
    [SerializeField] int levelNumber;
    [SerializeField] GameObject firstSpawnerToCheck;
    [SerializeField] GameObject secondSpawnerToCheck;
    [SerializeField] GameObject firstAngel;
    [SerializeField] GameObject secondAngel;
    [SerializeField] GameObject doorToBeOpenned;

    private void Start()
    {
        GameManager.instance.CanGoTheNextLevel = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shunrald"))
        {
            if (levelNumber == 1)
            {
                if (firstSpawnerToCheck != null)
                {
                    UIManager.instance.InfoEnable("Destroy all enemies before continuing!");
                    StartCoroutine(DisableInfoText());
                }
                else
                {
                    GameManager.instance.CanGoTheNextLevel = true;
                }
            }
            else if (levelNumber == 2)
            {
                if (firstSpawnerToCheck != null)
                {
                    UIManager.instance.InfoEnable("Destroy all enemies before continuing!");
                    StartCoroutine(DisableInfoText());
                }
                else
                {
                    GameManager.instance.CanGoTheNextLevel = true;
                }
            }
            else if (levelNumber == 3)
            {
                if (firstSpawnerToCheck != null)
                {
                    UIManager.instance.InfoEnable("Destroy all enemies before continuing!");
                    StartCoroutine(DisableInfoText());
                }
                else
                {
                    GameManager.instance.CanGoTheNextLevel = true;
                }
            }
            else if (levelNumber == 4)
            {
                if (firstSpawnerToCheck != null || firstAngel != null || secondAngel != null)
                {
                    UIManager.instance.InfoEnable("Destroy all enemies before continuing!");
                    StartCoroutine(DisableInfoText());
                }
                else
                {
                    StartCoroutine(OpenTheDoor());
                    
                }
            }

            
            if (GameManager.instance.CanGoTheNextLevel)
            {
                GameManager.instance.Upgrade(upgradePoints);
            }
        }
    }

    private IEnumerator DisableInfoText()
    {
        yield return new WaitForSeconds(3f);
        UIManager.instance.InfoDisable();
    }

    private IEnumerator OpenTheDoor()
    {
        float timeElapsed = 0;
        Quaternion startRotation = Quaternion.Euler(-90f, 0, 0);
        Quaternion targetRotation = Quaternion.Euler(-90f, 0, -125f);
        yield return new WaitForSeconds(1f);
        bool isOpenning = true;
        while (isOpenning)
        {
            // Increment the time elapsed
            timeElapsed += Time.deltaTime;

            // Calculate the interpolation factor based on the current time elapsed and rotation duration
            float t = Mathf.Clamp01(timeElapsed / 0.95f);

            // Use Lerp to interpolate between the start rotation and target rotation
            Quaternion newRotation = Quaternion.Lerp(startRotation, targetRotation, t);

            // Apply the new rotation to the object
            doorToBeOpenned.transform.rotation = newRotation;

            // Complete rotation
            if (timeElapsed >= 2f)
            {
                Debug.Log(timeElapsed);
                isOpenning = false;
            }

            // Wait for the next frame
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.CanGoTheNextLevel = true;
        GameManager.instance.Upgrade(upgradePoints);

    }
}
