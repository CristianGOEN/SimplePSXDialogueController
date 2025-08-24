using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SimplePSXDialogueController
{
    public class DialogueClip : PlayableAsset
    {
        public override double duration => 400.0;
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<DialogueBehaviour>.Create(graph);
        }
    }
}
