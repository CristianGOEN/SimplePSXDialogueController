using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePSXDialogueController
{
    public class TranslationController : MonoBehaviour
    {
        public string country; //Todo prolly move in to config environment variables
        private Dictionary<string, string> dataDictionary = new();

        public static TranslationController instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);

                return;
            }

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