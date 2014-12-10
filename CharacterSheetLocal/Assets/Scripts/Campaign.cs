using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot]
public class Campaign {

    [XmlAttribute]
    public string Name = "default";

    [XmlElement]
    public List<string> CharacterNames = new List<string>();

    [XmlIgnore]
    public List<Character> Characters = new List<Character>();

    public Campaign()
    {

    }

    public Campaign(string campaign, List<string> campaignCharacterNames)
    {
        this.Name = campaign;
        this.CharacterNames = campaignCharacterNames;
    }
    public Campaign(string campaign, List<Character> characters)
    {
        this.Name = campaign;
        this.Characters = characters;
        CharacterNames = new List<string>();
        foreach (Character c in characters)
        {
            CharacterNames.Add(c.Name);
        }
    }
}