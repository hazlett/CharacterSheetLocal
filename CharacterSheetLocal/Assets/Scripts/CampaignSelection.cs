using UnityEngine;
using System.Collections;

public class CampaignSelection : MonoBehaviour {

    private Vector2 scroll = new Vector2();
    private Rect rect = new Rect(0, 0, Screen.width * 0.5f, Screen.height);

    void OnGUI()
    {
        GUILayout.BeginArea(rect);
        scroll = GUILayout.BeginScrollView(scroll);
        GUILayout.Label("<b>CLOUD CAMPAIGNS</b>");
        foreach (string name in DataManager.Instance.Campaigns)
        {
            if (GUILayout.Button(name))
            {
                Global.Instance.DungeonMaster = true;
                Global.Instance.Local = false;
                Application.LoadLevel("CharacterSheet");
            }
        }
#if !UNITY_WEBPLAYER
        GUILayout.Label("<b>LOCAL CAMPAIGNS</b>");
        foreach (string name in DataManager.Instance.LocalCampaigns)
        {
            if (GUILayout.Button(name))
            {
                Global.Instance.DungeonMaster = true;
                Global.Instance.Local = true;
                Application.LoadLevel("CampaignManager");
            }
        }
#endif
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
}
