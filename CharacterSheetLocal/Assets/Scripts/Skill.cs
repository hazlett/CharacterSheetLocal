using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Skill {

    private string name;
    private int ranks, bonus;
    private bool classSkill;
    private Stats stat;
    [XmlAttribute]
    public string Name { get; set; }
    [XmlIgnore]
    public int Ranks { get; set; }
    [XmlIgnore]
    public int Bonus { get; set; }
    [XmlAttribute]
    public Stats Stat { get; set; }
    [XmlIgnore]
    public bool IsClassSkill { get; set; }
    public Skill()
    {

    }
    public Skill(string name, Stats stat, int ranks = 0, int bonus = 0, bool classSkill = false)
    {
        this.name = name;
        this.ranks = ranks;
        this.stat = stat;
        this.bonus = bonus;
        this.classSkill = classSkill;
    }
    public void SetRanks(int ranks)
    {
        this.ranks = ranks;
    }
    public void SetBonus(int bonus)
    {
        this.bonus = bonus;
    }
    public void SetClassSkill(bool classSkill = true)
    {
        this.classSkill = classSkill;
    }
}
