using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;
using System.Xml;

[XmlRoot]
public class Character {
    
    [XmlAttribute]
    public string Name = "name";
    [XmlAttribute]
    public string Owner = "owner";
    [XmlAttribute]
    public string Race = "race";
    [XmlAttribute]
    public string Gender = "gender";
    [XmlAttribute]
    public string Age = "age";
    [XmlAttribute]
    public string Alignment = "true neutral";
    [XmlAttribute]
    public string Eye = "eye";
    [XmlAttribute]
    public string Hair = "hair";
    [XmlAttribute]
    public string Scars = "scars";
    [XmlAttribute]
    public string Height = "height";
    [XmlAttribute]
    public string Weight = "weight";
    [XmlAttribute]
    public string Speed = "speed";
    [XmlAttribute]
    public string Experience = "0";
    [XmlAttribute]
    public string Money = "0";
    [XmlAttribute]
    public string Strength = "0";
    [XmlAttribute]
    public string Dexterity = "0";
    [XmlAttribute]
    public string Constitution = "0";
    [XmlAttribute]
    public string Intelligence = "0";
    [XmlAttribute]
    public string Wisdom = "0";
    [XmlAttribute]
    public string Charisma = "0";
    [XmlAttribute]
    public string CurrentHP = "0";
    [XmlAttribute]
    public string MaxHP = "0";
    [XmlAttribute]
    public string AC = "10";
    [XmlAttribute]
    public string ACMiscBonus = "0";
    [XmlAttribute]
    public string ShieldBonus = "0";
    [XmlAttribute]
    public string DR = "0";
    [XmlAttribute]
    public string InitiativeBonus = "0";
    [XmlAttribute]
    public string BaseFortitude = "0";
    [XmlAttribute]
    public string BaseReflex = "0";
    [XmlAttribute]
    public string BaseWill = "0";
    [XmlAttribute]
    public string BonusFortitude = "0";
    [XmlAttribute]
    public string BonusReflex = "0";
    [XmlAttribute]
    public string BonusWill = "0";
    [XmlAttribute]
    public string GrappleBonus = "0";

    [XmlElement]
    public List<Skill> Skills = new List<Skill>();
    [XmlElement]
    public List<Feat> Feats = new List<Feat>();
    [XmlElement]
    public List<Item> Inventory = new List<Item>();
    [XmlElement]
    public List<Class> Classes = new List<Class>();
    [XmlElement]
    public List<string> Languages = new List<string>();
    [XmlElement]
    public List<Weapon> Weapons = new List<Weapon>();
    [XmlElement]
    public Armor EquippedArmor = new Armor();

    [XmlElement]
    public string Notes = "Some notes here";
    public Character()
    {
        
    }
    public string TotalLevel()
    {
        int level = 0;
        foreach (Class c in Classes)
        {
            level += int.Parse(c.Level);
        }
        return level.ToString();
    }
    public string ClassesToString()
    {
        string value = "LEVEL: ", classes = "";
        int level = 0;
        for (int i = 0; i < Classes.Count; i++)
        {
            classes += Classes[i].Name;
            if (i != Classes.Count - 1)
            {
                classes += " | ";
            }
            level += int.Parse(Classes[i].Level);
        }
        value += level.ToString() + "\n" + classes;
        return value;
    }
    public string Save()
    {
        try
        {
            Debug.Log("Saving character locally");
            XmlSerializer xmls = new XmlSerializer(typeof(Character));
            using (FileStream stream = new FileStream(DataManager.Instance.characterDirectory + Name + ".xml", FileMode.Create))
            {
                xmls.Serialize(stream, this);
            }
            return "Saved successfully";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

}
