using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SimplePSXDialogueController
{
    public class DialogueTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Dialogue dialogue = playerData as Dialogue;
            Paragraph paragraph = null;
            float currentActive = 0f;
            int currentIndex = 0;

            if (!dialogue)
            {
                return;
            }

            if (!DialogueTimelineController.instance)
            {
                return;
            }

            for (int i = 0; i < playable.GetInputCount(); i++)
            {
                Debug.Log(playable.GetInputCount());
                float inputWeight = playable.GetInputWeight(i);

                if (inputWeight > 0f)
                {
                    paragraph = dialogue.GetParagraphs()[i];
                    currentActive = inputWeight;
                    currentIndex = i;
                }
            }

            if (currentActive <= 0f)
            {
                DialogueTimelineController.instance.CloseDialogue();

                return;
            }

            DialogueTimelineController.instance.StartDialogue(paragraph, currentIndex);
        }
    }
}