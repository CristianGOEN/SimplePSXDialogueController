using UnityEngine;

namespace SimplePSXDialogueController
{
    [System.Serializable]
    public class Answer
    {
        [SerializeField] private string text;
        [SerializeField] private Dialogue dialogueToJump;
        [SerializeField] private bool disabled = false;

        public string GetText()
        {
            return text;
        }

        public Dialogue GetDialogueToJump()
        {
            return dialogueToJump;
        }

        public bool IsDisabled()
        {
            return disabled;
        }

        public void SetDisabled(bool value)
        {
            disabled = value;
        }
    }
}
