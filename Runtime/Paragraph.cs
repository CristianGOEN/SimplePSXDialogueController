using UnityEngine;

namespace SimplePSXDialogueController
{
    [System.Serializable]
    public class Paragraph
    {
        [SerializeField] private string speakerName;
        [SerializeField] private string speakerText;
        [SerializeField] private bool isQuestion = false;
        [SerializeField] private Answer[] answers;

        public string GetSpeakerName()
        {
            return speakerName;
        }

        public string GetSpeakerText()
        {
            return speakerText;
        }

        public bool IsQuestion()
        {
            return isQuestion;
        }

        public Answer[] GetAnswers()
        {
            return answers;
        }
    }
}
