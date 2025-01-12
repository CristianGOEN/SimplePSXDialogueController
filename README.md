# SIMPLE PSX DIALOGUE CONTROLLER

- A simple way to control dialogues, translations and answers from players

## Example usage


### Dialogues

- Just create a dialogue from create > Dialogue and set it as reference to send to DialogueController

```csharp
{
    DialogueController.instance.StartDialogue(dialogue);
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
