using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Feat {

    [XmlAttribute]
    public string Name = "feat";
    [XmlAttribute]
    public string Description = "feat";

    public Feat() { }
}
