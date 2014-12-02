using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Skill {

    
    [XmlAttribute]
    public string Name { get; set; }
    [XmlAttribute]
    public Stats Stat { get; set; }
    [XmlAttribute]
    public int Ranks { get; set; }
    [XmlAttribute]
    public int Bonus { get; set; }
    [XmlAttribute]
    public bool IsClassSkill { get; set; }
    public Skill()
    {
        Name = "";
        //Stat = Stats.STR;
        //Ranks = 0;
        //Bonus = 0;
        //IsClassSkill = false;
    }
    public Skill(string name, Stats stat, int ranks = 0, int bonus = 0, bool classSkill = false)
    {
        Name = name;
        Ranks = ranks;
        Stat = stat;
        Bonus = bonus;
        IsClassSkill = classSkill;
    }
    public void SetRanks(int ranks)
    {
        Ranks = ranks;
    }
    public void SetBonus(int bonus)
    {
        Bonus = bonus;
    }
    public void SetClassSkill(bool classSkill = true)
    {
        IsClassSkill = classSkill;
    }
}
