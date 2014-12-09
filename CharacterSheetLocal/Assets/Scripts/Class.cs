using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Class {
    [XmlAttribute]
    public string Name = "class";
    [XmlAttribute]
    public string Level = "0";
    [XmlAttribute]
    public bool Prestige = false;

    public Class() { }

    public override string ToString()
    {
        return "CLASS: " + Name + " LEVEL: " + Level;
    }
}
