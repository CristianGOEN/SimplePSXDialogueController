# SIMPLE PSX DIALOGUE CONTROLLER

- A simple way to control dialogues, translations and answers from players

## Example usage


### Dialogue

- Just create a dialogue from create > Dialogue and set it as reference to send to DialogueController

```csharp
{
    DialogueController.instance.StartDialogue(dialogue);
}
```

- You can also group dialogues with the dialogues class

### Timed Dialogue

- A dialogue that is independant from a normal dialogue and shows on their own on a limited time (if time is nullable will get the default value set).
- Just create a dialogue from create > Dialogue and set it as reference to send to DialogueTimedController

```csharp
{
    DialogueTimedController.instance.StartDialogue(dialogue, 2f);
}
```

- You can send show a random message from a dialogue too

```csharp
{
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
