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
    public Character curr;
    

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
                if (!characters.Contains(jsonData.Characters[rnd]) && jsonData.Characters[rnd].Name != "Samantha and Daniel")
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
        displayEmail();
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
        displayEmail();
    }
    public void accept()
    {
        if (curr.Name == "Sam" && curr.Progress == 1)
        {
            jsonData.Characters[jsonData.Characters.ToList().IndexOf(curr)].Name = "Samantha";
        }
        int prog = 0;
        prog = jsonData.Characters[jsonData.Characters.ToList().IndexOf(curr)].Progress;
        prog++;
        jsonData.Characters[jsonData.Characters.ToList().IndexOf(curr)].Progress = prog;

        if (prog == jsonData.Characters[jsonData.Characters.ToList().IndexOf(curr)].Emails.Length)
        {
            List<Character> c = jsonData.Characters.ToList();
            c.Remove(curr);

            jsonData.Characters = c.ToArray();
        }
        

        load();
    }
    void displayEmail()
    {    


        //int index = jsonData.Characters.ToList().IndexOf(curr);
        int index = currEmails.IndexOf(curr);
        Debug.Log("Index: " + index + "\nCurrent emails: " +
            currEmails.Count);

        System.Random random = new System.Random();
        //int progress = jsonData.Characters[index].Progress;
        int progress = currEmails[index].Progress;


        headline.text = curr.Emails[progress].Headline;
        fromline.text = new string ("From: " +curr.Name);
        body.text = curr.Emails[progress].Text;
        prompt.text = new string ("Please paint: \n" + curr.Prompts[progress]);
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
    void load()
    {
        currEmails = newEmails();
        if (currEmails.Count == 0)
        {
            finish();
            return;
        }
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
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            next();
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
        headline.text = "We are getting married!!";
        fromline.text = "Samantha and Daniel";
        body.text = "Hello!!! It’s Samantha (but also Daniel). \nI didn’t know Daniel had been writing with you too!?!? He always talked about how he really liked art, so I showed him the paintings you made for me, and he showed me the paintings you did for him - we were both so surprised, but it was so funny! Anyways, we have been great since last time we wrote. So great that we are actually GETTING MARRIED!! We are very excited and could not think of a better way to celebrate than commission another painting from you of both of us. We want a painting that symbolizes our new beginnings and happiness together. \nP.S you are invited to the wedding! (invitation arriving soon) \nP.P.S. my EVIL stepmother will NOT be attending!";
        prompt.text = "Wedding picture of a bride and a groom, happy, excited, love, flowers.";
        requirements.text = "";
        payment.text = "100 grams of parmesan";
    }


}
