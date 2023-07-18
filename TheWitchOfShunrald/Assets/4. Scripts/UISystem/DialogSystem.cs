using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using CihanAkpınar;

public class DialogSystem: MonoBehaviour
{
    
    public struct DialogStruct
    {
        public string name;
        public string text;
        public Sprite icon;
        public AudioClip audioClip;
        public GameObject dialogCaller;
    }
    private int dialogIndex = 0;
    private DialogSystem.DialogStruct[] currentDialog;
    [SerializeField] private Text dialogText;
    [SerializeField] private TextMeshProUGUI dialogNameText;
    [SerializeField] private Image dialogImage;
    private Tween tween;
    public void DialogNext()
    {
        tween.Kill();
        DialogText(currentDialog);
    }
    public void DialogText(DialogSystem.DialogStruct[] dialog)
    {
        currentDialog = dialog;
        if (dialogIndex < currentDialog.Length)
        {
            dialogText.text = "";
            tween = dialogText.DOText(currentDialog[dialogIndex].text, currentDialog[dialogIndex].text.Length / 40f);
            dialogNameText.text = currentDialog[dialogIndex].name;
            dialogImage.sprite = currentDialog[dialogIndex].icon;
            AudioManager.Instance.PlayDialogue(currentDialog[dialogIndex].audioClip, 100f);
            dialogIndex++;
        }
        else
        {
            dialogIndex = 0;
            currentDialog[0].dialogCaller.GetComponent<DialogueSetter>().TakeAction();
            UIManager.instance.GamePanel();
            GameManager.instance.GameState = GameManager.State.Playing;
        }
    }
}
