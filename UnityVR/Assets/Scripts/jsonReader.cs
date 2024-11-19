using UnityEngine;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

public class jsonReader : MonoBehaviour
{
    public int emailsDaily;
    private JsonData jsonData;
    List<Character> currEmails;
    public TMP_Text headline,fromline,body,prompt,requirements,payment;
    Character curr;
    
    List<Character> newEmails()
    {
        if (jsonData.Characters.Length==0)
        {
            return new List<Character>();
        }

        int emails = 0;
        List<Character> characters = new List<Character>() ;
        System.Random random = new System.Random();
        if (jsonData.Characters.Length >= emailsDaily)
        {
            do
            {
                int rnd = random.Next(jsonData.Characters.Length);
                if (!characters.Contains(jsonData.Characters[rnd]))
                {
                    characters.Add(jsonData.Characters[rnd]);
                    emails++;
                }
            } while (emails < emailsDaily);
        }
        else
        {
            characters = jsonData.Characters.ToList();
        }

        return characters;
    }
    public void next()
    {
        int index = currEmails.IndexOf(curr);
        if (index == currEmails.Count-1 )
        {
            curr = currEmails[0];
        }
        else
        {
            curr =  currEmails[index+1];
        }

    }
    public void prev()
    {
        int index = currEmails.IndexOf(curr);
        if (index == 0)
        {
            curr = currEmails[currEmails.Count - 1];
        }
        else
        {
            curr = currEmails[index - 1];
        }
    }
    void displayEmail()
    {
        
        int index = jsonData.Characters.ToList().IndexOf(curr);
        System.Random random = new System.Random();
        int progress = jsonData.Characters[index].Progress;

        headline.text = curr.Emails[progress].Headline;
        fromline.text = curr.Name;
        body.text = curr.Emails[progress].Text;
        prompt.text = curr.Prompts[progress];
        string req = curr.Emails[progress].Requirement;
        if (req != null) //sometimes null
        {
            requirements.text = req;
        }
        else
        {
            requirements.text = "no requirement";
        }
        payment.text = curr.Emails[progress].Payment.ToString() + " grams of parmesan";
    }
    public void accept()
    {
        int prog = jsonData.Characters[jsonData.Characters.ToList().IndexOf(curr)].Progress;
        prog++;
        jsonData.Characters[jsonData.Characters.ToList().IndexOf(curr)].Progress = prog;
        if (prog == jsonData.Characters[jsonData.Characters.ToList().IndexOf(curr)].Emails.Length)
        {
            List<Character> c = jsonData.Characters.ToList();
            c.Remove(curr);
            foreach (Character ch in c)
            {
                Debug.Log(ch.Name);
            }
            jsonData.Characters = c.ToArray();
        }
        
        load();
    }
    void load()
    {
        currEmails = newEmails();
        if (currEmails.Count == 0)
        {
            finish();
            return;
        }
        System.Random random = new System.Random();

        curr = currEmails[0];
        displayEmail();

    }

    void Start()
    {
        string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/test.json");

        jsonData = JsonUtility.FromJson<JsonData>(jsonString);

        load();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            prev();
            Debug.Log("prev");
            displayEmail();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            next();
            Debug.Log("next");
            displayEmail();
        }
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            accept();
        }
    }

    [System.Serializable]
    public class Character
    {
        public string Name;
        public int Progress;
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
    void finish()
    {
        headline.text = "finish";
        fromline.text = "finish";
        body.text = "finish";
        prompt.text = "finish";
        requirements.text = "finish";
        payment.text = "finish";
    }


}
