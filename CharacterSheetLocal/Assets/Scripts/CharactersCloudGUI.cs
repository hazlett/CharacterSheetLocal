using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class CharactersCloudGUI : MonoBehaviour {
    private List<string> characterNames = new List<string>();
	void Start () {
        Refresh();
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    void OnGUI()
    {
        if (GUILayout.Button("MAIN MENU"))
        {
            Application.LoadLevel("Loading");
        }
        GUILayout.Space(10.0f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("REFRESH"))
        {
            Refresh();
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10.0f);
        if (GUILayout.Button("NEW CHARACTER"))
        {
            Global.Instance.CurrentCharacter = new Character();
            LoadSheet();
        }
        GUILayout.Space(5.0f);
        GUILayout.Label("<b>CLICK A NAME TO LOAD THAT CHARACTER</b>");
        GUILayout.BeginHorizontal();
        foreach (string name in characterNames)
        {
            if (GUILayout.Button(name))
            {
                LoadCharacter(name);
            }
        }
        GUILayout.EndHorizontal();
    }

    private void Refresh()
    {
        characterNames = new List<string>();
        StartCoroutine(RefreshCharactersList(new WWW("http://hazlett206.ddns.net/DND/GetCharacters.php")));
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
            Global.Instance.CurrentCharacter = obj;
            LoadSheet();

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    private void LoadSheet()
    {
        Global.Instance.DungeonMaster = false;
        Global.Instance.Local = false;
        Application.LoadLevel("CharacterSheet");
    }
}
