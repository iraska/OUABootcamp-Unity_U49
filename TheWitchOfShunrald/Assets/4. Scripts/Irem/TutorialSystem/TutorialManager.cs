using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace TutorialSystem
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager instance;

        [SerializeField] private GameObject transparentPanel, wasdImg, gif, tutorialText;
        [SerializeField] private RectTransform tutorialPanel, hiddenTutorialPanel, okButton, hiddenTextPanel, textPanel;

        private Vector3 initialOkButtonScale;

        private GameObject ui;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            ui = FindObjectOfType<UIManager>().gameObject;
        }

        public IEnumerator InitialTutorialPanel(float waitTime)
        {
            ToggleUIPanel(false);
            yield return new WaitForSeconds(waitTime);

            GameManager.instance.GameState = GameManager.State.Tutorial;

            if (gif.activeSelf) { gif.SetActive(false); }

            tutorialText.GetComponent<TextMeshProUGUI>().text = "Please use \"W A S D\" keys to move.";

            wasdImg.SetActive(true);
            transparentPanel.SetActive(true);

            TutorialTween();
        }

        public void GifTutorialPanel(VideoClip gifClip)
        {
            GameManager.instance.GameState = GameManager.State.Tutorial;
            ToggleUIPanel(false);

            if (wasdImg.activeSelf) { wasdImg.SetActive(false); }

            gif.GetComponent<VideoPlayer>().clip = gifClip;
            gif.GetComponent<VideoPlayer>().Play();

            gif.SetActive(true);
            transparentPanel.SetActive(true);

            TutorialTween();
        }

        public void ResumeGame()
        {
            GameManager.instance.GameState = GameManager.State.Playing;

            ToggleUIPanel(true);
        }

        public void ToggleUIPanel(bool toggle)
        {
            ui.SetActive(toggle);
        }

        private void TutorialTween()
        {
            DGMove(hiddenTutorialPanel, tutorialPanel, 1f);
            DGMove(hiddenTextPanel, textPanel, 1.25f);

            DGScale(initialOkButtonScale, okButton, 1.1f, 1.3f);
        }

        private void DGMove(Transform _hiddenTransform, Transform _transform, float _duration)
        {
            _transform.transform.DOMove(_hiddenTransform.transform.position, _duration).SetEase(Ease.InOutBack);
        }

        private void DGScale(Vector3 _initialScale, RectTransform _transform, float _scaleFact, float _duration)
        {
            _initialScale = _transform.localScale;
            _transform.DOScale(_initialScale * _scaleFact, _duration).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
