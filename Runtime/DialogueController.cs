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

        public static event System.Action<Dialogue> onDialogueStart;
        public static event System.Action<Dialogue> onDialogueEnd;
        public static event System.Action<Paragraph> onParagraphStart;
        public static event System.Action<Paragraph> onParagraphEnd;
        public static event System.Action<string> onAnswerSelected;

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
        }

        public void StartDialogue(Dialogue dialogue)
        {
            if (isDialogueActive)
            {
                return;
            }

            isDialogueActive = true;

            OpenDialoguePanel();

            StartCoroutine(DisplayText(dialogue));
        }

        public void StartRandomDialogue(Dialogues dialogues)
        {
            if (isDialogueActive)
            {
                return;
            }

            isDialogueActive = true;

            OpenDialoguePanel();

            StartCoroutine(DisplayText(dialogues.GetRandomDialogue()));
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
            onDialogueStart?.Invoke(dialogue);

            currentTextSpeed = textSpeed;
            Dialogue dialogueToJump = null;

            for (int i = 0; i < dialogue.GetParagraphs().Length; i++)
            {
                Paragraph paragraph = dialogue.GetParagraphs()[i];
                speakerName.text = TranslationController.instance.Translate(paragraph.GetSpeakerName());
                speakerText.text = string.Empty;
                dialogueEndIcon.SetActive(false);
                dialogueContinueIcon.SetActive(false);

                string[] textSplit = TranslationController.instance.Translate(paragraph.GetSpeakerText()).Split(' ');
                isTyping = true;

                onParagraphStart?.Invoke(paragraph);

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
                        dialogueToJump = paragraph.GetAnswers()[selectedAnswer].GetDialogueToJump();
                        answerReceived = true;
                        onAnswerSelected?.Invoke(paragraph.GetAnswers()[selectedAnswer].GetText());
                    });

                    yield return new WaitUntil(() => answerReceived);
                    continue;
                }

                ShowDialogueButtons(i, dialogue);

                yield return new WaitUntil(() => Input.GetButtonDown("Jump"));

                onParagraphEnd?.Invoke(paragraph);
            }

            if (dialogueToJump)
            {
                StartCoroutine(DisplayText(dialogueToJump));
                yield break;
            }

            isDialogueActive = false;

            CloseDialoguePanel();

            onDialogueEnd?.Invoke(dialogue);

            yield return null;
        }

        private void ShowDialogueButtons(int index, Dialogue currentDialogue)
        {
            if (index >= currentDialogue.GetParagraphs().Length - 1)
            {
                dialogueEndIcon.SetActive(true);

                return;
            }

            dialogueContinueIcon.SetActive(true);
        }
    }
}
