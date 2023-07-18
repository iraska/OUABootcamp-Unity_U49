using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CihanAkpýnar;
public class Level1BugFixer : MonoBehaviour
{
    private void Start()
    {
        LevelManager.instance.loadingCanvas.SetActive(false);
        LevelManager.instance.gameCanvas.SetActive(true);
        GameManager.instance.GameStart();
        if (SceneManager.GetActiveScene().buildIndex != 7)
        {
            AudioManager.Instance.PlayMusicWithFade(AudioManager.Instance.dungeonAtmosphereAudios);
        }
        else
        {
            AudioManager.Instance.PlayMusicWithFade(AudioManager.Instance.cemeteryAtmosphereAudios);
        }
    }
}
