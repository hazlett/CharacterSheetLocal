using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CampaignManagerUI : MonoBehaviour {
    private string campaign = "";
    private List<Character> characters = new List<Character>();
    private List<Character> allCharacters = new List<Character>();
    private Vector2 leftScroll = new Vector2();
    private Rect leftRect = new Rect(0, 0, Screen.width * 0.5f, Screen.height);
    private Vector2 rightScroll = new Vector2();
    private Rect rightRect = new Rect(Screen.width * 0.5f, 0, Screen.width * 0.5f, Screen.height);
	void Start () {
	    string[] files = Directory.GetFiles("Characters");
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
        GUILayout.Label("CAMPAIGN:"); campaign = GUILayout.TextField(campaign);
#if !UNITY_WEBPLAYER
        if (GUILayout.Button("SAVE CAMPAIGN LOCAL"))
        {
            XmlHandler.Instance.Save("Campaigns//" + campaign + ".xml", typeof(List<Character>), characters);
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
        
        GUILayout.EndScrollView();
        GUILayout.EndArea();

    }

    private void SaveCampaignToServer()
    {
        throw new System.NotImplementedException();
    }
}
