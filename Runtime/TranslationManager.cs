using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePSXDialogueController
{
    public class TranslatioController : MonoBehaviour
    {
        public string country; //Todo prolly move in to config environment variables
        public static TranslatioController instance;
        private Dictionary<string, string> dataDictionary = new();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(instance);
                return;
            }

            DontDestroyOnLoad(instance);

            LoadDictionaries();
        }

        private void LoadDictionaries()
        {
            TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("translations");

            foreach (TextAsset jsonFile in jsonFiles)
            {
                Dictionary<string, Dictionary<string, string>> tempData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonFile.text);
                foreach (var entry in tempData)
                {
                    dataDictionary[entry.Key] = entry.Value[country];
                }
            }
        }

        public string Translate(string key)
        {
            if (!dataDictionary.ContainsKey(key))
            {
                Debug.LogWarning("Could not find the key: " + key);

                return key;
            }

            return dataDictionary[key];
        }
    }
}