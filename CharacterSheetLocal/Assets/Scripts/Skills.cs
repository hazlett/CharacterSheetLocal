using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot]
public class Skills {
    [XmlIgnore]
    private List<Skill> baseSkills = new List<Skill>
    {
        new Skill("Appraise", Stats.WIS),
        new Skill("Balance*", Stats.DEX),
        new Skill("Bluff", Stats.CHA),
        new Skill("Climb*", Stats.STR),
        new Skill("Computer Use", Stats.INT),
        new Skill("Concentrate", Stats.CON),
        new Skill("Decipher Script", Stats.INT),
        new Skill("Diplomacy", Stats.CHA),
        new Skill("Disguise", Stats.CHA),
        new Skill("Doctor", Stats.INT)
    };
    [XmlElement]
    public List<Skill> BaseSkills { get; set; }
    [XmlIgnore]
    private static Skills instance = new Skills();
    public static Skills Instance { get { return instance; } }

    public Skills()
    {
        BaseSkills = baseSkills;
    }
    public void SaveSkills()
    {
        Debug.Log("Saving skills");
        XmlSerializer xmls = new XmlSerializer(typeof(Skills));
        using (FileStream stream = new FileStream("skills.xml", FileMode.Create))
        {
            xmls.Serialize(stream, this);
        }
    }
    public void Load()
    {
        string path = "skills.xml";
        Debug.Log("Loading skills");
        XmlSerializer serializer = new XmlSerializer(typeof(Skills));
        if (File.Exists(path))
        {
            Debug.Log(path);
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                instance = serializer.Deserialize(stream) as Skills;
            }
        }
    }

}
