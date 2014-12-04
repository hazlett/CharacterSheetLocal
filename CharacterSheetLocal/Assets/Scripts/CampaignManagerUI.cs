using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CampaignManagerUI : MonoBehaviour {
    private string campaign = "";
    private List<Character> characters = new List<Character>();
    private List<Character> allCharacters = new List<Character>();
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
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("MENU"))
        {
            Application.LoadLevel("Menu");
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("CAMPAIGN:"); campaign = GUILayout.TextField(campaign);
        if (GUILayout.Button("SAVE CAMPAIGN"))
        {
            XmlHandler.Instance.Save("Campaigns//" + campaign + ".xml", typeof(List<Character>), characters);
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
