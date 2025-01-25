using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePSXDialogueController
{
    [CreateAssetMenu(fileName = "New Dialogue container", menuName = "SimplePSXDialogueController/Dialogues")]
    public class Dialogues : ScriptableObject
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
            return dialogues[Random.Range(0, dialogues.Length)];
        }
    }
}