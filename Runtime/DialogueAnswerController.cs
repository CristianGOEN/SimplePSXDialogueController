using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SimplePSXDialogueController
{
    public class DialogueAnswerController : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueAnswerChildPanelUI;
        [SerializeField] private Button[] answerButtons;

        private System.Action<int> onAnswerSelected;

        private void Start()
        {
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].onClick.AddListener(delegate { AnswereClicked(i); });
            }            
        }

        public void StartQuestion(Paragraph paragraph, System.Action<int> callback)
        {
            foreach (var answerButton in answerButtons)
            {
                answerButton.gameObject.SetActive(false);
            }

            if (paragraph.GetAnswers().Length <= 0)
            {
                return;
            }

            for (int i = 0; i < paragraph.GetAnswers().Length; i++)
            {
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = TranslationController.instance.Translate(paragraph.GetAnswers()[i].GetText());
                answerButtons[i].gameObject.SetActive(true);

                if (paragraph.GetAnswers()[i].IsDisabled())
                {
                    answerButtons[i].interactable = false;

                    continue;
                }

                answerButtons[i].interactable = true;
            }

            //answerButtons[0].Select();
            onAnswerSelected = callback;

            OpenPanel();
        }

        private void OpenPanel()
        {
            dialogueAnswerChildPanelUI.SetActive(true);
        }

        private void ClosePanel()
        {
            dialogueAnswerChildPanelUI.SetActive(false);
        }

        public void AnswereClicked(int index)
        {
            onAnswerSelected?.Invoke(index);
            ClosePanel();
        }
    }
}
