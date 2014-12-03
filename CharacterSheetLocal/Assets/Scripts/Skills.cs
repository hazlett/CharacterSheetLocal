using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
public class Skills {
    [XmlIgnore]
    private Skills baseSkills;
    [XmlElement]
    public List<Skill> SkillsList { get; set; }
    [XmlIgnore]
    private static Skills instance = new Skills();
    public static Skills Instance { get { return instance; } }

    public Skills()
    {
        
    }
    public void Initialize()
    {
        SkillsList = baseSkills.SkillsList;
    }
    public void SaveBaseSkills()
    {
        Debug.Log("Saving skills");
        XmlSerializer xmls = new XmlSerializer(typeof(Skills));
        using (FileStream stream = new FileStream("skills.xml", FileMode.Create))
        {
            xmls.Serialize(stream, this);
        }
    }
    public void SaveBaseSkills(List<Skill> skills)
    {
        this.SkillsList = skills;
        Debug.Log("Saving skills");
        XmlSerializer xmls = new XmlSerializer(typeof(Skills));
        using (FileStream stream = new FileStream("skills.xml", FileMode.Create))
        {
            xmls.Serialize(stream, this);
        }
    }
    public List<Skill> LoadBaseSkills()
    {
        string path = "skills.xml";
        Debug.Log("Loading base skills");
        XmlSerializer serializer = new XmlSerializer(typeof(Skills));
        if (File.Exists(path))
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                baseSkills = serializer.Deserialize(stream) as Skills;
            }
        }

        //foreach (Skill skill in baseSkills.SkillsList)
        //{
        //    Debug.Log(skill.Name);
        //}
        Debug.Log("Skills loaded");
        return baseSkills.SkillsList;
    }
}
