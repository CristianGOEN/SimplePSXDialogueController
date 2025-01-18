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

        public void StartDialogue(Dialogue dialogue, float? timer = null)
        {
            if (isDialogueActive)
            {
                return;
            }

            isDialogueActive = true;

            OpenDialoguePanel();

            StartCoroutine(DisplayText(dialogue, timer ?? textTimer));
        }

        public void StartRandomDialogue(Dialogue dialogue, float? timer = null)
        {
            if (isDialogueActive)
            {
                return;
            }

            isDialogueActive = true;

            OpenDialoguePanel();

            StartCoroutine(DisplayRandomText(dialogue, timer ?? textTimer));
        }

        private IEnumerator DisplayText(Dialogue dialogue, float timer)
        {
            for (int i = 0; i < dialogue.GetParagraphs().Length; i++)
            {
                speakerText.text = FormatText(dialogue.GetParagraphs()[i]);

                yield return new WaitForSeconds(timer);
            }

            isDialogueActive = false;

            CloseDialoguePanel();

            yield return null;
        }

        private IEnumerator DisplayRandomText(Dialogue dialogue, float timer)
        {
            speakerText.text = FormatText(dialogue.GetParagraphs()[Random.Range(0, dialogue.Length())]);

            yield return new WaitForSeconds(timer);
           
            isDialogueActive = false;

            CloseDialoguePanel();

            yield return null;
        }

        private string FormatText(Paragraph paragraph)
        {
            return TranslationController.instance.Translate(paragraph.GetSpeakerName()) + ": " + TranslationController.instance.Translate(paragraph.GetSpeakerText());
        }
    }
}