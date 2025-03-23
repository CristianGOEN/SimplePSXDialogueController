using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SimplePSXDialogueController
{
    public class DialogueTimedController : DialogueControllerBase
    {
        public static bool isDialogueActive = false;

        [Header("Dialogue panel GUI references")]
        [SerializeField] private TextMeshProUGUI speakerText;

        [Header("Text settings")]
        public float textTimer = 2f;
        public float secondsBetweenDialogues = 1f;

        public static DialogueTimedController instance;
        
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

        public void StartDialogues(Dialogues dialogues, float? timer = null)
        {
            if (isDialogueActive)
            {
                return;
            }

            StartCoroutine(DisplayDialogues(dialogues, timer ?? textTimer));
        }

        private IEnumerator DisplayDialogues(Dialogues dialogues, float timer)
        {
            foreach (Dialogue dialogue in dialogues)
            {
                isDialogueActive = true;

                OpenDialoguePanel();

                yield return StartCoroutine(DisplayDialogue(dialogue, timer));

                yield return new WaitForSeconds(secondsBetweenDialogues);
            }
        }

        public void StartDialogue(Dialogue dialogue, float? timer = null)
        {
            if (isDialogueActive)
            {
                return;
            }

            isDialogueActive = true;

            OpenDialoguePanel();

            StartCoroutine(DisplayDialogue(dialogue, timer ?? textTimer));
        }

        private IEnumerator DisplayDialogue(Dialogue dialogue, float timer)
        {
            foreach (Paragraph paragraph in dialogue.GetParagraphs())
            {
                speakerText.text = FormatText(paragraph);

                yield return new WaitForSeconds(timer);
            }

            isDialogueActive = false;

            CloseDialoguePanel();

            yield return null;
        }

        public void StartRandomParagraph(Dialogue dialogue, float? timer = null)
        {
            if (isDialogueActive)
            {
                return;
            }

            isDialogueActive = true;

            OpenDialoguePanel();

            StartCoroutine(DisplayRandomParagraph(dialogue, timer ?? textTimer));
        }

        private IEnumerator DisplayRandomParagraph(Dialogue dialogue, float timer)
        {
            speakerText.text = FormatText(dialogue.GetRandomParagraph());

            yield return new WaitForSeconds(timer);

            isDialogueActive = false;

            CloseDialoguePanel();

            yield return null;
        }

        public void StartRandomDialogue(Dialogues dialogues, float? timer = null)
        {
            if (isDialogueActive)
            {
                return;
            }

            isDialogueActive = true;

            OpenDialoguePanel();

            StartCoroutine(DisplayDialogue(dialogues.GetRandomDialogue(), timer ?? textTimer));
        }

        private string FormatText(Paragraph paragraph)
        {
            return TranslationController.instance.Translate(paragraph.GetSpeakerName()) + ": " + TranslationController.instance.Translate(paragraph.GetSpeakerText());
        }
    }
}