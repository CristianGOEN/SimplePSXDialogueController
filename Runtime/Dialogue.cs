using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimplePSXDialogueController
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "SimplePSXDialogueController/Dialogue")]
    public class Dialogue : ScriptableObject, IEnumerable<Paragraph>
    {
        [SerializeField] private Paragraph[] paragraphs;

        public Paragraph[] GetParagraphs()
        {
            return paragraphs;
        }

        public Paragraph GetRandomParagraph()
        {
            return paragraphs[Random.Range(0, Length())];
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

        public IEnumerator<Paragraph> GetEnumerator()
        {
            foreach (Paragraph paragraph in paragraphs)
            {
                yield return paragraph;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}