using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class DialogSystem: MonoBehaviour
{
    public struct DialogStruct
    {
        public string name;
        public string text;
        public Sprite icon;
    }
    private int dialogIndex = 0;
    private DialogSystem.DialogStruct[] currentDialog;
    [SerializeField] private Text dialogText;
    [SerializeField] private TextMeshProUGUI dialogNameText;
    [SerializeField] private Image dialogImage;
    public void DialogNext()
    {
        DialogText(currentDialog);
    }
    public void DialogText(DialogSystem.DialogStruct[] dialog)
    {
        currentDialog = dialog;
        if (dialogIndex < currentDialog.Length)
        {
            dialogText.text = "";
            dialogText.DOText(currentDialog[dialogIndex].text, currentDialog[dialogIndex].text.Length / 40f);
            dialogNameText.text = currentDialog[dialogIndex].name;
            dialogImage.sprite = currentDialog[dialogIndex].icon;
            dialogIndex++;
        }
        else
        {
            dialogIndex = 0;
            UIManager.instance.GamePanel();
            GameManager.instance.GameState = GameManager.State.Playing;
        }
    }
}
