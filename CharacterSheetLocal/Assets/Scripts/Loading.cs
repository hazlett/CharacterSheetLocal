using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loading : MonoBehaviour {

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            List<string> list = DataManager.Instance.Campaigns;

            Debug.Log("CAMPAIGNS");
            foreach (string name in list)
            {
                Debug.Log(name);
            }
            Debug.Log("END OF CAMPAIGNS");
        }
    }
    void OnGUI()
    {
#if !UNITY_WEBPLAYER
        DrawLocal();      
#endif
        DrawCloud();

    }
    private void DrawLocal()
    {
        if (GUILayout.Button("LOCAL"))
        {
            Global.Instance.Local = true;
            Application.LoadLevel("LocalMenu");
        }
    }
    private void DrawCloud()
    {
        GUILayout.Space(10.0f);
        if (GUILayout.Button("CHARACTER CLOUD"))
        {
            Global.Instance.Local = false;
            Application.LoadLevel("CharactersCloud");
        }
        GUILayout.Space(10.0f);
        if (GUILayout.Button("DM CLOUD"))
        {
            Global.Instance.Local = false;
            Application.LoadLevel("DMCloud");
        }
    }
}
