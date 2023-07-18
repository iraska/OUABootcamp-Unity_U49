using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IntroScene : MonoBehaviour
{
    [SerializeField] private float introDuration;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(introDuration);
        SkipIntro();
    }
    public void SkipIntro()
    {
        StartCoroutine(LevelManager.instance.LoadingGame(3));
    }
}
