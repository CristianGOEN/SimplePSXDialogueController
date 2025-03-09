using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SimplePSXDialogueController
{
    public class DialogueController : DialogueControllerBase
    {
        public static bool isDialogueActive = false;

        [Header("Dialogue panel GUI references")]
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private TextMeshProUGUI speakerText;
        [SerializeField] private GameObject dialogueContinueIcon;
        [SerializeField] private GameObject dialogueEndIcon;
        [SerializeField] private DialogueAnswerController dialogueAnswerController;

        [Header("Text settings")]
        public int textHeightBox = 70;
        public float textSpeed = 0.1f;
        private float currentTextSpeed;
        private Coroutine typeDialogueCoroutine;
        private bool isTyping = false;

        public static DialogueController instance;

        public delegate void OnDialogueStart();
        public static event OnDialogueStart onDialogueStart;

        public delegate void OnDialogueEnd();
        public static event OnDialogueEnd onDialogueEnd;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(instance);
                return;
            }

            DontDestroyOnLoad(instance);
        }

        public void StartDialogue(Dialogue dialogue)
        {
            if (isDialogueActive)
            {
                return;
            }

            isDialogueActive = true;

            OpenDialoguePanel();

            onDialogueStart?.Invoke();

            StartCoroutine(DisplayText(dialogue));
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump") && isTyping && isDialogueActive && currentTextSpeed != 0)
            {
                currentTextSpeed = 0;
            }
        }

        private IEnumerator DisplayText(Dialogue dialogue)
        {
            currentTextSpeed = textSpeed;

            for (int i = 0; i < dialogue.GetParagraphs().Length; i++)
            {
                Paragraph paragraph = dialogue.GetParagraphs()[i];
                speakerName.text = TranslationController.instance.Translate(paragraph.GetSpeakerName());
                speakerText.text = string.Empty;
                dialogueEndIcon.SetActive(false);
                dialogueContinueIcon.SetActive(false);

                string[] textSplit = TranslationController.instance.Translate(paragraph.GetSpeakerText()).Split(' ');
                isTyping = true;

                foreach (var word in textSplit)
                {
                    string temporalText = speakerText.text;
                    speakerText.text += word + ' ';

                    //Si el paragrafo es más largo que la caja lo parte en dos
                    if (speakerText.preferredHeight > textHeightBox)
                    {
                        speakerText.text = temporalText;
                        dialogueContinueIcon.SetActive(true);

                        yield return new WaitUntil(() => Input.GetButtonDown("Jump"));

                        dialogueContinueIcon.SetActive(false);
                        speakerText.text = word + ' ';
                        currentTextSpeed = textSpeed;
                    }

                    yield return new WaitForSeconds(currentTextSpeed);
                }

                currentTextSpeed = textSpeed;

                isTyping = false;

                if (paragraph.IsQuestion())
                {
                    bool answerReceived = false;

                    dialogueAnswerController.StartQuestion(paragraph, (selectedAnswer) =>
                    {
                        i = dialogue.GetIndexParagraphByKey(paragraph.GetAnswers()[selectedAnswer].GetParagraphIndex()) - 1;
                        answerReceived = true;
                    });

                    yield return new WaitUntil(() => answerReceived);
                    continue;
                }

                if (i >= dialogue.GetParagraphs().Length - 1)
                {
                    dialogueEndIcon.SetActive(true);
                }
                else
                {
                    dialogueContinueIcon.SetActive(true);
                }

                yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
            }

            isDialogueActive = false;

            CloseDialoguePanel();

            onDialogueEnd?.Invoke();

            yield return null;
        }
    }
}
