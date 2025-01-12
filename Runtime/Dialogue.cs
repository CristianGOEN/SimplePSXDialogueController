using UnityEngine;

namespace SimplePSXDialogueController
{
    [CreateAssetMenu(fileName = "New Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private Paragraph[] paragraphs;

        public Paragraph[] GetParagraphs()
        {
            return paragraphs;
        }

        public int Length()
        {
            return paragraphs.Length;
        }

        public int GetIndexParagraphByKey(string key)
        {
            for (int i = 0; i < paragraphs.Length; i++)
            {
                if (paragraphs[i].GetSpeakerText() == key)
                {
                    return i;
                }
            }

            return paragraphs.Length;
        }
    }
}