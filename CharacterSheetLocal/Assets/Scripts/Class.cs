﻿using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Class {
    [XmlAttribute]
    public string Name = "class";
    [XmlAttribute]
    public string Level = "0";
    public Class() { }

}
