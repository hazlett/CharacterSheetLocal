using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
public class DMCloudGUI : MonoBehaviour {
    private List<string> campaignNames;
    private List<Character> characters = new List<Character>();
    private string message = "";
    private int loading = 0;
    private bool load = false;
    private bool autoStart = true;
	void Start () {
        Refresh();
	}
	void Update()
    {
        if ((autoStart) && (load) && (loading == 0))
        {
            StartCampaign();
        }
    }
    void OnGUI()
    {
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
        foreach (Character c in characters)
        {
            GUILayout.Label(c.Name);
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(5.0f);
        if (GUILayout.Button("START THIS CAMPAIGN"))
        {
            StartCampaign();
        }
    }
    private void StartCampaign()
    {
        Global.Instance.Local = false;
        Global.Instance.DungeonMaster = true;
        Global.Instance.Campaign = characters;
        Application.LoadLevel("CharacterSheet");
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
    private void Refresh()
    {
        campaignNames = new List<string>();
        StartCoroutine(RefreshCampaignsList(new WWW("http://hazlett206.ddns.net/DND/GetCampaigns.php")));
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

            List<string> obj = new List<string>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            XmlReader reader = new XmlNodeReader(doc);

            obj = serializer.Deserialize(reader) as List<string>;
            if (obj == null)
            {
                obj = new List<string>();
            }
            foreach (string name in obj)
            {
                loading++;
                string url = "http://hazlett206.ddns.net/DND/LoadCharacter.php";
                WWWForm form = new WWWForm();
                form.AddField("fileName", name);
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
