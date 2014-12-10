using UnityEngine;
using System.Collections;

public class CampaignSelection : MonoBehaviour {

    private Vector2 cloudScroll, localScroll, cloudCharactersScroll, localCharactersScroll, topScroll;
    private Rect cloudRect, localRect, cloudCharactersRect, localCharactersRect, topRect;
    private Campaign current;
    void Start()
    {
        Global.Instance.DungeonMaster = true;
        current = new Campaign();
        AdjustScreen();
    }
    private void AdjustScreen()
    {
#if !UNTIY_WEBPLAYER
        topScroll = new Vector2(); topRect = new Rect(0, 0, Screen.width, Screen.height * 0.2f);
        cloudScroll = new Vector2(); cloudRect = new Rect(0, Screen.height * 0.2f, Screen.width * 0.5f, Screen.height * 0.4f);
        localScroll = new Vector2(); localRect = new Rect(Screen.width * 0.5f, Screen.height * 0.2f, Screen.width * 0.5f, Screen.height * 0.4f);
        cloudCharactersScroll = new Vector2(); cloudCharactersRect = new Rect(0, Screen.height * 0.6f, Screen.width * 0.5f, Screen.height * 0.4f);
        localCharactersScroll = new Vector2(); localCharactersRect = new Rect(Screen.width * 0.5f, Screen.height * 0.6f, Screen.width * 0.5f, Screen.height * 0.4f);        
#else
        topScroll = new Vector2(); topRect = new Rect(0, 0, Screen.width, Screen.height * 0.2f);
        cloudScroll = new Vector2(); cloudRect = new Rect(0, Screen.height * 0.2f, Screen.width, Screen.height * 0.4f);
        cloudCharactersScroll = new Vector2(); cloudCharactersRect = new Rect(0, Screen.height * 0.6f, Screen.width, Screen.height * 0.4f);
#endif
    }
    void OnGUI()
    {
        GUILayout.BeginArea(topRect);
        topScroll = GUILayout.BeginScrollView(topScroll);
        if (DataManager.Instance.Loading)
        {
            string loading = "<b>...LOADING...</b>";
            if (DataManager.Instance.ConnectionError)
            {
                loading += "\n<b>NETWORK ERROR\nNO INTERNET CONNECTION</b>";
            }
            else if (DataManager.Instance.ServerError)
            {
                loading += "\n<b>SERVER ERROR\nCANNOT CONNECT TO SERVER</b>";
            }
            GUILayout.Box(loading);
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("MENU"))
        {
            Application.LoadLevel("SelectionMenu");
        }
        if (GUILayout.Button("REFRESH"))
        {
            DataManager.Instance.Refresh();
        }
        if (GUILayout.Button("QUIT"))
        {
            Application.Quit();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("SAVE AND START"))
        {
            Global.Instance.CampaignCharacters = current.Characters;
            Global.Instance.CampaignName = current.Name;
            DataManager.Instance.SyncToServer(current);
            DataManager.Instance.SendCampaign(current);
#if UNITY_STANDALONE
            foreach (Character c in current.Characters)
            {
                c.Save();
            }
            XmlHandler.Instance.Save(DataManager.Instance.campaignDirectory + current.Name + ".xml", typeof(Campaign), current);
#endif
            Application.LoadLevel("CharacterSheet");
        }
        if (GUILayout.Button("START WITHOUT SAVING"))
        {
            Global.Instance.CampaignCharacters = current.Characters;
            Global.Instance.CampaignName = current.Name;
        }
        if (GUILayout.Button("SAVE/SYNC TO CLOUD"))
        {
            DataManager.Instance.SyncToServer(current);
            DataManager.Instance.SendCampaign(current);
        }
#if UNITY_STANDALONE
        if (GUILayout.Button("SAVE/SYNC LOCAL"))
        {
            foreach (Character c in current.Characters)
            {
                c.Save();
            }
            XmlHandler.Instance.Save(DataManager.Instance.campaignDirectory + current.Name + ".xml", typeof(Campaign), current);
        }
#endif
        GUILayout.EndHorizontal();

        GUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.BeginArea(cloudRect);
        cloudScroll = GUILayout.BeginScrollView(cloudScroll);
        GUILayout.Label("<b>CLOUD CAMPAIGNS</b>\nCLICK TO LOAD");
        foreach (Campaign c in DataManager.Instance.AllCloudCampaigns)
        {
            if (GUILayout.Button(c.Name))
            {
                Global.Instance.Local = false;
                current = c;
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.BeginArea(cloudCharactersRect);
        cloudCharactersScroll = GUILayout.BeginScrollView(cloudCharactersScroll);
        GUILayout.Label("<b>CLOUD CHARACTERS</b>\nCLICK TO ADD TO CAMPAIGN");
        foreach (Character c in DataManager.Instance.AllCloudCharacters)
        {
            if (GUILayout.Button(c.Name))
            {
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();

#if !UNITY_WEBPLAYER
        GUILayout.BeginArea(localRect);
        localScroll = GUILayout.BeginScrollView(localScroll);
        GUILayout.Label("<b>LOCAL CAMPAIGNS</b>\nCLICK TO LOAD");
        foreach (string name in DataManager.Instance.LocalCampaigns)
        {
            if (GUILayout.Button(name))
            {
                DataManager.Instance.LoadCampaign(name);
                Global.Instance.Local = true;
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.BeginArea(localCharactersRect);
        localCharactersScroll = GUILayout.BeginScrollView(localCharactersScroll);
        GUILayout.Label("<b>LOCAL CHARACTERS</b>\nCLICK TO ADD TO CAMPAIGN");
        foreach (Character c in DataManager.Instance.AllLocalCharacters)
        {
            if (GUILayout.Button(c.Name))
            {
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
#endif


    }

}
