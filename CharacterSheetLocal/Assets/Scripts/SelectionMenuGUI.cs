using UnityEngine;
using System.Collections;

public class SelectionMenuGUI : MonoBehaviour {

    private Vector2 scroll = new Vector2();
    private Rect rect = new Rect(0, 0, Screen.width, Screen.height);

    void OnGUI()
    {
        if (DataManager.Instance.Loading)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 150.0f, Screen.height * 0.5f - 100.0f, 300.0f, 100.0f), "<b>...LOADING...</b>");
        }
        GUILayout.BeginArea(rect);
        scroll = GUILayout.BeginScrollView(scroll);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("REFRESH"))
        {
            DataManager.Instance.Refresh();
        }
        if (GUILayout.Button("QUIT"))
        {
            Application.Quit();
        }
        GUILayout.EndHorizontal();
     
#if !UNITY_PHONE
        if (GUILayout.Button("CAMPAIGN MANAGEMENT"))
        {
            Application.LoadLevel("CampaignManager");
        }
#endif
        GUILayout.Label("<b>CLOUD CHARACTERS</b>");
        foreach (Character c in DataManager.Instance.AllCloudCharacters)
        {
            if (GUILayout.Button(c.Name + "\n" + c.ClassesToString()))
            {
                Global.Instance.CurrentCharacter = c;
                Global.Instance.DungeonMaster = false;
                Global.Instance.Local = false;
                Application.LoadLevel("CharacterSheet");
            }
        }
#if !UNITY_WEBPLAYER
        GUILayout.Label("<b>LOCAL CHARACTERS</b>");
        foreach (Character c in DataManager.Instance.AllLocalCharacters)
        {
            if (GUILayout.Button(c.Name + "\n" + c.ClassesToString()))
            {
                Global.Instance.CurrentCharacter = c;
                Global.Instance.DungeonMaster = false;
                Global.Instance.Local = true;
                Application.LoadLevel("CharacterSheet");
            }
        }
        GUILayout.Space(15.0f);
        if (GUILayout.Button("<b>OVERWRITE LOCAL WITH CLOUD</b>\n(Downloads all chracters and overwrites any local character with the same name"))
        {
            DataManager.Instance.SyncWithServer(true);
        }
        if (GUILayout.Button("<b>SYNC CLOUD TO LOCAL</b>\n(Downloads all characters and saves them locally. Does not overwrite local characters)"))
        {
            DataManager.Instance.SyncWithServer();
        }
        if (GUILayout.Button("<b>SYNC LOCAL TO CLOUD</b>\n(Uploads characters to the cloud)"))
        {
            DataManager.Instance.SyncToServer();
        }
#endif


        GUILayout.EndScrollView();
        GUILayout.EndArea();


    }
}
