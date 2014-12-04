using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;
public class PHPTest : MonoBehaviour {

    private List<string> characterNames;
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
    
    }
    private void Refresh()
    {
        characterNames = new List<string>();
        string url = "http://hazlett206.ddns.net/DND/GetCharacters.php";
        WWWForm form = new WWWForm();
        WWW www = new WWW(url);

        StartCoroutine(RefreshCharactersList(www));
    }

    private void LoadCharacter(string file)
    {
        string url = "http://hazlett206.ddns.net/DND/LoadCharacter.php";
        WWWForm form = new WWWForm();
        form.AddField("fileName", file);
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForCharacter(www));
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


        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
