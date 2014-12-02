using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Weapon {
    [XmlAttribute]
    public string Name = "weapon";
    [XmlAttribute]
    public string Damage = "0";
    [XmlAttribute]
    public string HitBonus = "0";
    [XmlAttribute]
    public string Description = "weapon";
    [XmlAttribute]
    public bool Ranged = false;
    public Weapon() { }
}
