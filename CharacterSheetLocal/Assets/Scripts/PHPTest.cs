using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;
public class PHPTest : MonoBehaviour {

    private List<string> characterNames, campaignNames;
    private List<Character> characters = new List<Character>();
    void Start()
    {
        Refresh();
        
    }


    void OnGUI()
    {
        if (GUILayout.Button("REFRESH"))
        {
            Refresh();
        }
        foreach(string name in characterNames)
        {
            if (GUILayout.Button(name))
            {
                LoadCharacter(name);
            }
        }
        foreach(string name in campaignNames)
        {
            if(GUILayout.Button(name))
            {
                LoadCampaign(name);
            }
        }
        GUILayout.Space(50.0f);
        GUILayout.Label("<b>CAMPAIGN CHARACTERS</b>");
        foreach(Character c in characters)
        {
            GUILayout.Label(c.Name);
        }
        if (GUILayout.Button("LOAD CAMPAIGN"))
        {
            Global.Instance.DungeonMaster = true;
            Global.Instance.CampaignCharacters = characters;
            Application.LoadLevel("CharacterSheet");
        }
    }
    private void Refresh()
    {
        characterNames = new List<string>();
        campaignNames = new List<string>();
        string url = "http://hazlett206.ddns.net/DND/GetCharacters.php";
        WWW www = new WWW(url);
        StartCoroutine(RefreshCharactersList(www));
        StartCoroutine(RefreshCampaignsList(new WWW("http://hazlett206.ddns.net/DND/GetCampaigns.php")));
    }

    private void LoadCharacter(string file)
    {
        string url = "http://hazlett206.ddns.net/DND/LoadCharacter.php";
        WWWForm form = new WWWForm();
        form.AddField("fileName", file);
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForCharacter(www));
    }
    private void LoadCampaign(string file)
    {
        string url = "http://hazlett206.ddns.net/DND/LoadCampaign.php";
        WWWForm form = new WWWForm();
        form.AddField("fileName", file);
        WWW www = new WWW(url, form);
        characters = new List<Character>();
        StartCoroutine(WaitForCampaign(www));
    }

    IEnumerator WaitForCampaign(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(www.text);

            List<string> obj = new List<string>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            XmlReader reader = new XmlNodeReader(doc);

            obj = serializer.Deserialize(reader) as List<string>;
            if (obj == null)
            {
                obj = new List<string>();
            }
            foreach(string name in obj)
            {
                string url = "http://hazlett206.ddns.net/DND/LoadCharacter.php";
                WWWForm form = new WWWForm();
                form.AddField("fileName", name);
                WWW www2 = new WWW(url, form);

                StartCoroutine(AddCharacterToCampaign(www2));
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    IEnumerator AddCharacterToCampaign(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(www.text);

            Character obj = new Character();
            XmlSerializer serializer = new XmlSerializer(typeof(Character));
            XmlReader reader = new XmlNodeReader(doc);

            obj = serializer.Deserialize(reader) as Character;
            if (obj == null)
            {
                obj = new Character();
            }
            characters.Add(obj);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    IEnumerator RefreshCampaignsList(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(www.text);


            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            XmlReader reader = new XmlNodeReader(doc);

            campaignNames = serializer.Deserialize(reader) as List<string>;
            if (campaignNames == null)
            {
                Debug.Log("NULL NAMES");
                campaignNames = new List<string>();
            }
            Debug.Log(campaignNames.Count);
            Debug.Log("Campaign names loaded");
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }  

    IEnumerator RefreshCharactersList(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(www.text);

           
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            XmlReader reader = new XmlNodeReader(doc);

            characterNames = serializer.Deserialize(reader) as List<string>;
            if (characterNames == null)
            {
                Debug.Log("NULL NAMES");
                characterNames = new List<string>();
            }
            Debug.Log(characterNames.Count);
            Debug.Log("Character names loaded");
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }  
    IEnumerator WaitForCharacter(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(www.text);

            Character obj = new Character();
            XmlSerializer serializer = new XmlSerializer(typeof(Character));
            XmlReader reader = new XmlNodeReader(doc);

            obj = serializer.Deserialize(reader) as Character;
            if (obj == null)
            {
                obj = new Character();
            }
            Debug.Log(obj.Name);
            Global.Instance.CurrentCharacter = obj;
            Global.Instance.DungeonMaster = false;
            Global.Instance.Local = false;
            Application.LoadLevel("CharacterSheet");

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
