using UnityEngine;
using System.Collections;

public class Global {

    private static Global instance = new Global();
    public static Global Instance { get { return instance; } }

    public string CharacterName;
}
