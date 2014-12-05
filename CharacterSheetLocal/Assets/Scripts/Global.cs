using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Global {

    private static Global instance = new Global();
    public static Global Instance { get { return instance; } }

    public string CharacterName;
    public string CampaignName;
    public List<Character> CampaignCharacters;
    public bool DungeonMaster;
    public Character CurrentCharacter;
    public bool Local;
}
