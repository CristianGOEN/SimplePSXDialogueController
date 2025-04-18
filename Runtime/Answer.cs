using UnityEngine;

namespace SimplePSXDialogueController
{
    [System.Serializable]
    public class Answer
    {
        [SerializeField] private string text;
        [SerializeField] private Dialogue dialogueToJump;

        public string GetText()
        {
            return text;
        }

        public Dialogue GetDialogueToJump()
        {
            return dialogueToJump;
        }
    }
}
