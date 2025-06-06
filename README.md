# SIMPLE PSX DIALOGUE CONTROLLER

- A simple way to control dialogues, translations and answers from players

## Example usage


### Dialogue

- Just create a dialogue from create > Dialogue and set it as reference to send to DialogueController

```csharp
{
    DialogueController.instance.StartDialogue(dialogue);
    DialogueController.instance.StartRandomDialogue(dialogue);
}
```

- You can also group dialogues with the dialogues class from create > Dialogues

- There are two events to track when a dialogue starts (onDialogueStart) or ends (onDialogueEnd)
- There are two events to track when a paragraph starts (onParagraphStart) or ends (onParagraphEnd):
- You can also track the answer clicked with the event onAnswerSelected


### Timed Dialogue

- A dialogue that is independant from a normal dialogue and shows on their own on a limited time (if time is nullable will get the default value set).
- Just create a dialogue from create > Dialogue and set it as reference to send to DialogueTimedController

```csharp
{
    DialogueTimedController.instance.StartDialogue(dialogue, 2f);
}
```

- You can send show a random paragraph from a dialogue too

```csharp
{
    DialogueTimedController.instance.StartRandomParagraph(dialogue);
}
```

- You can call a random dialogue from dialogues or normal one like that:

```csharp
{
    DialogueTimedController.instance.StartDialogues(dialogue);
    DialogueTimedController.instance.StartRandomDialogue(dialogue);
}
```

### Translations

- Create a translations folder in to resources/translations.
- Json files must have this structure:

```json
{
  "player_1": {
    "es": "Alex",
    "en": "Alex",
    "de": "Alex",
    "fr": "Alex"
  },
  "player_2": {
    "es": "Marcos",
    "en": "Marcos",
    "de": "Marcos",
    "fr": "Marcos"
  }
}
```

- If you want to use translation controller for anything else, you can use it like that:

```csharp
{
    TranslatioController.instance.Translate("player_1");
}
```

### Timeline

- You can add dialogues to timeline by just right clicking your timeline and adding a Dialogue track
- For this to work just use the DialogueTimelineController script