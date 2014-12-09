using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot]
public class Campaign {

    [XmlAttribute]
    public string Name;

    [XmlElement]
    public List<string> CharacterNames;

    [XmlIgnore]
    public List<Character> Characters;

    [XmlIgnore]
    private bool loaded = false;
    public bool Loaded { get { return loaded; } } 

    public Campaign()
    {

    }

    public Campaign(string campaign, List<string> campaignCharacterNames)
    {
        this.Name = campaign;
        this.CharacterNames = campaignCharacterNames;
    }

}