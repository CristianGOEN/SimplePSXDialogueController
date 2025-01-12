using UnityEngine;

namespace SimplePSXDialogueController
{
    [System.Serializable]
    public class Answer
    {
        [SerializeField] private string text;
        [SerializeField] private string paragraphIndexToJump;

        public string GetText()
        {
            return text;
        }

        public string GetParagraphIndex()
        {
            return paragraphIndexToJump;
        }
    }
}
