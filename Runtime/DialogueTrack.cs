using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


namespace SimplePSXDialogueController
{
    [TrackBindingType(typeof(Dialogue))]
    [TrackClipType(typeof(DialogueClip))]
    public class DialogueTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<DialogueTrackMixer>.Create(graph, inputCount);
        }
    }
}