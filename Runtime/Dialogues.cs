using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePSXDialogueController
{
    [CreateAssetMenu(fileName = "New Dialogue container", menuName = "SimplePSXDialogueController/Dialogues")]
    public class Dialogues : ScriptableObject, IEnumerable<Dialogue>
    {
        [SerializeField] private Dialogue[] dialogues;
        
        public Dialogue[] GetDialogues()
        {
            return dialogues;
        }

        public int Length()
        {
            return dialogues.Length;
        }

        public Dialogue GetRandomDialogue()
        {
            return dialogues[Random.Range(0, Length())];
        }

        public IEnumerator<Dialogue> GetEnumerator()
        {
            foreach (Dialogue dialogue in dialogues)
            {
                yield return dialogue;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}