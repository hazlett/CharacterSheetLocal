using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Item {

    [XmlAttribute]
    public string Name = "item";
    [XmlAttribute]
    public string Description = "item";

    public Item() { }
}
