using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CampaignManagerUI : MonoBehaviour {
    private string campaignName = "";
    private List<Character> characters = new List<Character>();
    private List<Character> allCharacters = new List<Character>();

    private Vector2 leftScroll = new Vector2();
    private Rect leftRect = new Rect(0, 0, Screen.width * 0.5f, Screen.height);
    private Vector2 rightScroll = new Vector2();
    private Rect rightRect = new Rect(Screen.width * 0.5f, 0, Screen.width * 0.5f, Screen.height);

    private Campaign campaign;

	void Start () {
        Global.Instance.DungeonMaster = true;
	    string[] files = Directory.GetFiles("Characters");
        campaign = new Campaign();
        foreach (string file in files)
        {
            Character c = (Character)XmlHandler.Instance.Load(file, typeof(Character));
            if (c != null)
            {
                allCharacters.Add(c);
            }
        }
	}
	
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginArea(leftRect);
        leftScroll = GUILayout.BeginScrollView(leftScroll);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("MENU"))
        {
            Application.LoadLevel("Loading");
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("<b>CAMPAIGN: " + campaignName + "</b>");

        GUILayout.BeginHorizontal();
        GUILayout.Label("CAMPAIGN NAME:"); campaignName = GUILayout.TextField(campaignName);
        GUILayout.EndHorizontal();
#if !UNITY_WEBPLAYER

        if (GUILayout.Button("SAVE CAMPAIGN LOCALLY"))
        {
            SaveCampaignLocally();
        }
#endif
        if (GUILayout.Button("SAVE CAMPAIGN CLOUD"))
        {
            SaveCampaignToServer();
        }
#if !UNITY_WEBPLAYER
        GUILayout.Label("ALL LOCAL CHARACTERS. CLICK TO ADD TO CAMPAIGN");
        GUILayout.BeginHorizontal();
        foreach(Character character in allCharacters)
        {
            if (GUILayout.Button(character.Name))
            {
                characters.Add(character);
            }
        }
        GUILayout.EndHorizontal();
#endif
        GUILayout.Space(50.0f);
        GUILayout.Label("CHARACTERS IN CAMPAIGN. CLICK TO REMOVE FROM CAMPAIGN");
        GUILayout.BeginHorizontal();
        Character remove = null;
        foreach(Character character in characters)
        {
            if (GUILayout.Button(character.Name))
            {
                remove = character;
            }
        }
        if (remove != null)
        {
            characters.Remove(remove);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.BeginArea(rightRect);
        rightScroll = GUILayout.BeginScrollView(rightScroll);
        GUILayout.Label("ALL CLOUD CHARACTERS. CLICK TO ADD TO CAMPAIGN");

        GUILayout.EndScrollView();
        GUILayout.EndArea();

    }

    private void SaveCampaignLocally()
    {
        campaign.Name = campaignName;
        campaign.CharacterNames = new List<string>();
        foreach (Character c in characters)
        {
            campaign.CharacterNames.Add(c.Name);
        }
        XmlHandler.Instance.Save("Campaigns//" + campaignName + ".xml", typeof(Campaign), campaign);
    }

    private void SaveCampaignToServer()
    {
        throw new System.NotImplementedException();
    }

    private void AddFromCloudToCampaign()
    {
#if !UNITY_WEBPLAYER
        

#endif
    }
}
