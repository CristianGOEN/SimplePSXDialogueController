using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SimplePSXDialogueController
{
    public class DialogueTimelineController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private TextMeshProUGUI speakerText;
        [SerializeField] private GameObject container;
        private int currentIndex = -1;
        public float textSpeed = 0.1f;
        private Coroutine typeDialogueCoroutine = null;

        public static DialogueTimelineController instance;
        public static event System.Action<Paragraph> onParagraphStart;
        public static event System.Action<Paragraph> onParagraphEnd;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void StartDialogue(Paragraph paragraph, int index)
        {
            if (currentIndex == index)
            {
                return;
            }

            if (typeDialogueCoroutine != null)
            {
                return;
            }

            if (!container.activeSelf)
            {
                container.SetActive(true);
            }

            currentIndex = index;
            typeDialogueCoroutine = StartCoroutine(DisplayText(paragraph));
        }

        IEnumerator DisplayText(Paragraph paragraph)
        {
            onParagraphStart?.Invoke(paragraph);

            speakerName.text = TranslationController.instance.Translate(paragraph.GetSpeakerName());
            speakerText.text = string.Empty;

            string[] textSplit = TranslationController.instance.Translate(paragraph.GetSpeakerText()).Split(' ');

            foreach (var word in textSplit)
            {
                string temporalText = speakerText.text;
                speakerText.text += word + ' ';

                yield return new WaitForSeconds(textSpeed);
            }

            onParagraphEnd?.Invoke(paragraph);

            typeDialogueCoroutine = null;
            yield return null;
        }

        public void CloseDialogue()
        {
            container.SetActive(false);
            speakerName.text = string.Empty;
            speakerText.text = string.Empty;
            currentIndex = -1;
        }
    }
}