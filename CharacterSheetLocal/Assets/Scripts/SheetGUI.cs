using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class SheetGUI : MonoBehaviour {

    private string name = "name", owner = "owner", race = "race", gender = "gender",
        age = "age", eye = "eye", hair = "hair", scars = "scars", height = "height", 
        weight = "weight", speed = "speed";
    private List<string> feats, languages, inventory;
    private List<Class> classes;
    private List<Skill> skills;
    private List<int> levels;
    private string totalLevel = "0", experience = "0000", money = "0000",
        strength = "", dexterity = "", constitution = "", intelligence = "", wisdom = "", charisma = "",
        ac = "", hp = "", dr = "",
        fortitude = "", reflex = "", will = "", baseFortitude = "", baseReflex = "", baseWill = "",
        baseAttackBonus = "", initiative = "";

    //GUI stuff
    private Vector2 detailsScroll = new Vector2();
    private Rect detailsRect = new Rect(0, 0, Screen.width, Screen.height * 0.125f);
    private Vector2 skillsScroll = new Vector2();
    private Rect skillsRect = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f);
    private Vector2 statsScroll = new Vector2();
    private Rect statsRect = new Rect(0, Screen.height * 0.125f, Screen.width * 0.25f, Screen.height * 0.25f);
	void Start () {
        classes = new List<Class>();
        levels = new List<int>();
        feats = new List<string>();
        skills = new List<Skill>();
        languages = new List<string>();
        inventory = new List<string>();
        Skills.Instance.LoadBaseSkills();
        Skills.Instance.Initialize();
        skills = Skills.Instance.SkillsList;

	}

	void Update () {
        if (Input.GetKeyUp(KeyCode.L))
        {
            Skills.Instance.LoadBaseSkills();
        }
	}

    void OnGUI()
    {

        DrawCharacterDetails();
        DrawStats();
        DrawSkills(); 
    }

    private void DrawStats()
    {
        GUILayout.BeginArea(statsRect);
        statsScroll = GUILayout.BeginScrollView(statsScroll);
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Strength");
        strength = GUILayout.TextField(strength);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Dexterity");
        dexterity = GUILayout.TextField(dexterity);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Constitution");
        constitution = GUILayout.TextField(constitution);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Intelligence");
        intelligence = GUILayout.TextField(intelligence);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Wisdom");
        wisdom = GUILayout.TextField(wisdom);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Charisma");
        charisma = GUILayout.TextField(charisma);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    private void DrawCharacterDetails()
    {
        GUILayout.BeginArea(detailsRect);
        detailsScroll = GUILayout.BeginScrollView(detailsScroll);
        if (GUILayout.Button("SAVE CHARACTER"))
        {
            //Saves character
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name:"); name = GUILayout.TextField(name);
        GUILayout.Label("Owner:"); owner = GUILayout.TextField(owner);
        GUILayout.Label("Race:"); race = GUILayout.TextField(race);
        //GUILayout.EndHorizontal();
        //GUILayout.BeginHorizontal();
        GUILayout.Label("Gender:"); gender = GUILayout.TextField(gender);
        GUILayout.Label("Age:"); age = GUILayout.TextField(age);
        GUILayout.Label("Eye:"); eye = GUILayout.TextField(eye);
        GUILayout.Label("Hair:"); hair = GUILayout.TextField(hair);
        //GUILayout.EndHorizontal();
        //GUILayout.BeginHorizontal();
        GUILayout.Label("Scars/Tats:"); scars = GUILayout.TextField(scars);
        GUILayout.Label("Height:"); height = GUILayout.TextField(height);
        GUILayout.Label("Weight:"); weight = GUILayout.TextField(weight);
        GUILayout.Label("Speed:"); speed = GUILayout.TextField(speed);
        GUILayout.Label("Exp:"); experience = GUILayout.TextField(experience);
        GUILayout.Label("Money:"); money = GUILayout.TextField(money);
        GUILayout.EndHorizontal();

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    private void DrawSkills()
    {
        GUILayout.BeginArea(skillsRect);
        if (GUILayout.Button("ADD SKILL"))
        {
            skills.Add(new Skill());
        }
        if (GUILayout.Button("SAVE SKILLS"))
        {
            Skills.Instance.SkillsList = skills;
            Skills.Instance.SaveBaseSkills();
        }     
        skillsScroll = GUILayout.BeginScrollView(skillsScroll);
        foreach (Skill skill in skills)
        {
            GUILayout.BeginHorizontal();
            skill.Name = GUILayout.TextField(skill.Name);
            if (GUILayout.Button(skill.Stat.ToString()))
            {
                if ((int)skill.Stat == 5)
                {
                    skill.Stat = 0;
                }
                else
                {
                    skill.Stat++;
                }
                
            }
            if (skill.IsClassSkill)
            {
                GUILayout.Label("CLASS");
            }
            else
            {
                GUILayout.Label("CROSS");
            }
            skill.Ranks = int.Parse(GUILayout.TextField(skill.Ranks.ToString()));
            skill.Bonus = int.Parse(GUILayout.TextField(skill.Bonus.ToString()));
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void Save()
    {
        totalLevel = "0";
        foreach(int level in levels)
        {
            totalLevel += level;
        }
    }
}
