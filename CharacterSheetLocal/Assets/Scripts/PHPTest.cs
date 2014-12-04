using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
public class PHPTest : MonoBehaviour {
    private string characterName = "";
    void Start()
    {

        
    }


    void OnGUI()
    {
        characterName = GUILayout.TextField(characterName);
        if (GUILayout.Button("LOAD CHARACTER"))
        {
            string url = "http://hazlett206.ddns.net/DND/LoadCharacter.php";
            WWWForm form = new WWWForm();
            form.AddField("characterName", characterName);
            WWW www = new WWW(url, form);

            StartCoroutine(WaitForRequest(www));
        }
    }

    IEnumerator WaitForRequest(WWW www)
 {
     yield return www;
    
     // check for errors
     if (www.error == null)
     {
         Debug.Log("WWW Ok!: " + www.text);
         XmlDocument doc = new XmlDocument();
         doc.LoadXml(www.text);

         Character obj = new Character();
         XmlSerializer serializer = new XmlSerializer(typeof(Character));
         XmlReader reader = new XmlNodeReader(doc);
             
         obj = serializer.Deserialize(reader) as Character;
         if (obj == null)
         {
             obj = new Character();
         }
         Debug.Log(obj.Name);

         
     } else {
         Debug.Log("WWW Error: "+ www.error);
     }    
 }  
}
