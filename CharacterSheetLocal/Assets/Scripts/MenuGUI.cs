using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

    private string characterName = "Test", campaignName = "TestCampaign";
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
       
        GUILayout.Label("Character Name:"); characterName = GUILayout.TextField(characterName);
        if (GUILayout.Button("Load Character"))
        {
            Global.Instance.CharacterName = characterName;
            Global.Instance.DungeonMaster = false;
            Application.LoadLevel("CharacterSheet");
        }
        GUILayout.Label("Campaign Name:"); campaignName = GUILayout.TextField(campaignName);
        if (GUILayout.Button("Load Campaign"))
        {
            Global.Instance.Campaign = campaignName;
            Global.Instance.DungeonMaster = true;
            Application.LoadLevel("CharacterSheet");
        }
        if (GUILayout.Button("Campaign Manager"))
        {
            Application.LoadLevel("CampaignManager");
        }
        if (GUILayout.Button("Feat and skill manager"))
        {
            Application.LoadLevel("Manager");
        }
    }
}
