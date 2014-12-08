using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

public class LocalGUI : MonoBehaviour {
    private Vector2 leftScroll = new Vector2();
    private Rect leftRect = new Rect(0, 20.0f, Screen.width * 0.45f, Screen.height);
    private Vector2 rightScroll = new Vector2();
    private Rect rightRect = new Rect(Screen.width * 0.55f, 20.0f, Screen.width * 0.45f, Screen.height);
    private string[] characterFiles = null, campaignFiles = null;
    private List<Character> characters, campaignCharacters;
    void Start()
    {
        Refresh();
    }

    void Update()
    {

    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("MENU"))
        {
            Application.LoadLevel("Loading");
        }
        if (GUILayout.Button("Campaign Manager"))
        {
            Global.Instance.Local = true;
            Application.LoadLevel("CampaignManager");
        }
        if (GUILayout.Button("Feat and skill manager"))
        {
            Global.Instance.Local = true;
            Application.LoadLevel("Manager");
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginArea(leftRect);
        GUILayout.BeginScrollView(leftScroll);
        GUILayout.Label("<b>CHARACTERS. CLICK TO LOAD</b>");
        if (GUILayout.Button("NEW CHARACTER"))
        {
            Global.Instance.DungeonMaster = false;
            Global.Instance.Local = true;
            Global.Instance.CharacterName = "";
            Application.LoadLevel("CharacterSheet");
        }
        GUILayout.Space(5.0f);
        foreach (Character c in characters)
        {
            if (GUILayout.Button(c.Name))
            {
                LoadCharacter(c.Name);
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
        GUILayout.BeginArea(rightRect);
        GUILayout.BeginScrollView(rightScroll);
        GUILayout.Label("<b>CAMPAIGNS. CLICK TO LOAD</b>");
        GUILayout.Space(5.0f);
        foreach (string name in campaignFiles)
        {
            if (GUILayout.Button(name))
            {
                LoadCampaign(name);
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();

    }
    private void Refresh()
    {
        characterFiles = Directory.GetFiles("Characters");
        characters = new List<Character>();
        foreach (string file in characterFiles)
        {
            Character c = (Character)XmlHandler.Instance.Load(file, typeof(Character));
            if (c != null)
            {
                characters.Add(c);
            }
        }
        campaignFiles = Directory.GetFiles("Campaigns");

    }
    private void LoadCharacter(string name)
    {
        Global.Instance.DungeonMaster = false;
        Global.Instance.Local = true;
        Global.Instance.CharacterName = name;
        Application.LoadLevel("CharacterSheet");
    }
    private void LoadCampaign(string name)
    {
        Global.Instance.DungeonMaster = true;
        Global.Instance.Local = false;
        Debug.Log("Loading Campaign");
        Debug.Log(name);


        Campaign campaign = (Campaign)XmlHandler.Instance.Load(name, typeof(Campaign));
        if (campaign == null)
        {
            campaign = new Campaign();
        }



        
        Global.Instance.CampaignName = name;
        campaignCharacters = new List<Character>();
        Debug.Log("Campaign Loaded");
        foreach (string file in campaign.CharacterNames)
        {
            Character c = (Character)XmlHandler.Instance.Load("Characters//" + file + ".xml", typeof(Character));
            campaignCharacters.Add(c);
        }

        Global.Instance.CampaignCharacters = campaignCharacters;
        Application.LoadLevel("CharacterSheet");        
    }
}
