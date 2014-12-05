using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CampaignManagerUI : MonoBehaviour {
    private string campaignName = "";
    private List<Character> characters = new List<Character>();
    private List<Character> allCharacters = new List<Character>();
    private Campaign campaign;
	void Start () {
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
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("MENU"))
        {
            Application.LoadLevel("LocalMenu");
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("CAMPAIGN:"); campaignName = GUILayout.TextField(campaignName);
        if (GUILayout.Button("SAVE CAMPAIGN"))
        {
            campaign.Name = campaignName;
            campaign.CharacterNames = new List<string>();
            foreach(Character c in characters)
            {
                campaign.CharacterNames.Add(c.Name);
            }
            XmlHandler.Instance.Save("Campaigns//" + campaignName + ".xml", typeof(Campaign), campaign);
        }
        GUILayout.Label("ALL CHARACTERS. CLICK TO ADD TO CAMPAIGN");
        GUILayout.BeginHorizontal();
        foreach(Character character in allCharacters)
        {
            if (GUILayout.Button(character.Name))
            {
                characters.Add(character);
            }
        }
        GUILayout.EndHorizontal();
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
    }
}
