using UnityEngine;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine.Rendering;



public class jsonReader : MonoBehaviour
{

    private JsonData jsonData;

    void Start()
    {
        string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/test.json");
        jsonData = JsonUtility.FromJson<JsonData>(jsonString);
        /* example to display email
        headline.text = jsonData.Characters[0].Emails[0].Headline;
        body.text = jsonData.Characters[0].Emails[0].Text;
        prompt.text = jsonData.Characters[0].Prompts[0];
        string req = jsonData.Characters[0].Emails[0].Requirement;
        if (req != null) //sometimes null
        {
            requirements.text = req;
        }
        else
        {
            requirements.text = "no requirement";
        }
        payment.text = jsonData.Characters[0].Emails[0].Payment.ToString() + "grams of parmesan";
        */
    }

    [System.Serializable]
    public class Character
    {
        public string Name;
        public string[] Prompts;
        public Email[] Emails;
    }

    [System.Serializable]
    public class Email
    {
        public string Headline;
        public string Text;
        public float Payment;
        public string Requirement;
    }

    [System.Serializable]
    public class JsonData
    {
        public Character[] Characters;
    }
}
