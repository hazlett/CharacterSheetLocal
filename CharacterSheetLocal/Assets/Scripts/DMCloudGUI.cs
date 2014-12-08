using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
public class DMCloudGUI : MonoBehaviour {
    private List<string> campaignNames, characterNames;
    private List<Character> characters = new List<Character>();
    private string message = "";
    private int loading = 0;
    private bool load = false;
    private bool autoStart = false;
    private string campaign = "";
    private Rect rightRect = new Rect(Screen.width * 0.55f, 0, Screen.width * 0.45f, Screen.height);
	void Start () {
        Refresh();
	}
	void Update() {
        if ((autoStart) && (load) && (loading == 0))
        {
            StartCampaign();
        }
    }
    void OnGUI()
    {
        GUILayout.Label("<b>CAMPAIGN NAME:</b>");
        GUILayout.Label("<b>" + campaign + "</b>");
        GUILayout.Label(message);
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
        autoStart = GUILayout.Toggle(autoStart, "AUTO START");
        GUILayout.EndHorizontal();
        GUILayout.Space(10.0f);
        if (GUILayout.Button("CREATE NEW CAMPAIGN"))
        {
            Application.LoadLevel("CampaignManager");
        }
        GUILayout.Space(10.0f);
        GUILayout.Label("<b>CLICK A NAME TO LOAD THAT CAMPAIGN</b>");
        GUILayout.BeginHorizontal();
        foreach (string name in campaignNames)
        {
            if (GUILayout.Button(name))
            {
                LoadCampaign(name);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10.0f);
        GUILayout.Label("<b>LIST OF CHARACTERS IN CAMPAIGN</b>");
        GUILayout.BeginHorizontal();
        Character remove = null;
        foreach (Character c in characters)
        {
            if (GUILayout.Button(c.Name))
            {
                remove = c;
            }
        }
        if (remove != null)
        {
            characters.Remove(remove);
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(5.0f);
        if (GUILayout.Button("SAVE AND START THIS CAMPAIGN"))
        {
            StartCampaign();
        }
        GUILayout.Space(5.0f);
        if (GUILayout.Button("SAVE CAMPAIGN"))
        {
            SaveCampaign();
        }
        GUILayout.BeginArea(rightRect);
        foreach (string name in characterNames)
        {
            if (GUILayout.Button(name))
            {
                string url = "http://hazlett206.ddns.net/DND/LoadCharacter.php";
                WWWForm form = new WWWForm();
                form.AddField("fileName", name);
                WWW www2 = new WWW(url, form);

                StartCoroutine(AddCharacterToCampaign(www2));
            }
        }

        GUILayout.EndArea();
    }
    private void StartCampaign()
    {
        Global.Instance.Local = false;
        Global.Instance.DungeonMaster = true;
        Global.Instance.CampaignCharacters = characters;
        Global.Instance.CampaignName = campaign;
        Application.LoadLevel("CharacterSheet");
    }
    private void SaveCampaign()
    {
        Debug.Log("Saving character to server");
        XmlSerializer xmls = new XmlSerializer(typeof(Campaign));
        StringWriter writer = new StringWriter();
        List<string> campaignCharacterNames = new List<string>();
        foreach(Character c in characters)
        {
            campaignCharacterNames.Add(c.Name);
        }
        Campaign camp = new Campaign(campaign, campaignCharacterNames);
        xmls.Serialize(writer, camp);
        writer.Close();
        WWWForm form = new WWWForm();
        form.AddField("name", campaign);
        form.AddField("file", writer.ToString());
        WWW www = new WWW("http://hazlett206.ddns.net/DND/SaveCampaign.php", form);
        StartCoroutine(UploadToServer(www));
    }
    IEnumerator UploadToServer(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("Save successful");
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    private void LoadCampaign(string file)
    {
        campaign = file.Replace(".xml", "");
        string url = "http://hazlett206.ddns.net/DND/LoadCampaign.php";
        WWWForm form = new WWWForm();
        form.AddField("fileName", file);
        WWW www = new WWW(url, form);
        characters = new List<Character>();
        StartCoroutine(WaitForCampaign(www));
    }
    private void Refresh()
    {
        campaignNames = new List<string>();
        StartCoroutine(RefreshCampaignsList(new WWW("http://hazlett206.ddns.net/DND/GetCampaigns.php")));
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
            characters.Add(obj);

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    IEnumerator WaitForCampaign(WWW www)
    {
        loading = 0;
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(www.text);

            Campaign obj = new Campaign();
            XmlSerializer serializer = new XmlSerializer(typeof(Campaign));
            XmlReader reader = new XmlNodeReader(doc);

            obj = serializer.Deserialize(reader) as Campaign;
            if (obj == null)
            {
                obj = new Campaign();
            }
            foreach (string name in obj.CharacterNames)
            {
                loading++;
                string url = "http://hazlett206.ddns.net/DND/LoadCharacter.php";
                WWWForm form = new WWWForm();
                form.AddField("fileName", name + ".xml");
                WWW www2 = new WWW(url, form);

                StartCoroutine(AddCharacterToCampaign(www2));
                
            }
            load = true;
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
        loading--;
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

}
