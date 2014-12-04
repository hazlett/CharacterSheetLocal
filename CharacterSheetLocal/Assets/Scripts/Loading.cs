using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {


	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnGUI()
    {


#if UNITY_WEBPLAYER
        Global.Instance.Local = false;
        DrawCloud();
#else
        DrawLocal();
        DrawCloud();
#endif

    }
    private void DrawLocal()
    {
        if (GUILayout.Button("LOCAL"))
        {
            Global.Instance.Local = true;
            Application.LoadLevel("Menu");
        }
    }
    private void DrawCloud()
    {
        GUILayout.Space(10.0f);
        if (GUILayout.Button("CHARACTER CLOUD"))
        {
            Application.LoadLevel("CharactersCloud");
        }
        GUILayout.Space(10.0f);
        if (GUILayout.Button("DM CLOUD"))
        {
            Application.LoadLevel("DMCloud");
        }
    }
}
