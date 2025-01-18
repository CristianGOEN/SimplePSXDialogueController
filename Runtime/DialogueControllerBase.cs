using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePSXDialogueController
{
    public abstract class DialogueControllerBase : MonoBehaviour
    {
        [SerializeField] protected GameObject dialoguePanelContainerUI;

        protected virtual void CloseDialoguePanel()
        {
            dialoguePanelContainerUI.SetActive(false);
        }

        protected virtual void OpenDialoguePanel()
        {
            dialoguePanelContainerUI.SetActive(true);
        }
    }
}