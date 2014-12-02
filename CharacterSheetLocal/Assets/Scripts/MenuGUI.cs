using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

    private string characterName = "";
	
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
            Application.LoadLevel("CharacterSheet");
        }
    }
}
