using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ArenaRewardPanel : MonoBehaviour
{
    [SerializeField] private GameObject rewardPaper;

    public IEnumerator Start()
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, PlayerPrefs.GetInt("arena", 0) * 220f);
        for (int i = transform.childCount; i < PlayerPrefs.GetInt("arena", 0); ++i)
        {
            Transform reward = Instantiate(rewardPaper, transform).transform;
            reward.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("arena" + i.ToString(), "null");
            yield return null;
        }
    }


}
