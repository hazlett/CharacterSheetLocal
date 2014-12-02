using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Armor {

    [XmlAttribute]
    public string Name = "armor";
    [XmlAttribute]
    public string ACBonus = "0";
    [XmlAttribute]
    public string MaxDex = "0";
    [XmlAttribute]
    public string ArmorCheckPenalty = "0";
    [XmlAttribute]
    public string DR = "0";
    [XmlAttribute]
    public string Description = "armor";

    public Armor() { }
}
