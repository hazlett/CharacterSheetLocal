using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class SheetGUI : MonoBehaviour {
    //serialize. could all be in Character
    //private string name = "name", owner = "owner", race = "race", gender = "gender",
    //    age = "age", eye = "eye", hair = "hair", scars = "scars", height = "height", 
    //    weight = "weight", speed = "speed";
    //private List<string> languages;
    //private List<Feat> feats;
    //private List<Item> inventory;
    //private List<Class> classes;
    //private List<Skill> skills;
    //private string experience = "0000", money = "0000",
    //    strength = "", dexterity = "", constitution = "", intelligence = "", wisdom = "", charisma = "",
    //    ac = "", hp = "", dr = "", baseFortitude = "", baseReflex = "", baseWill = "", initiativeBonus = "";
    //dont serialize. private and/or calculated on fly
    private Character character = new Character();
    private string totalLevel = "0", fortitude = "0", reflex = "0", will = "0", totalInitiative = "0", baseAttackBonus = "0";
    private int strMod, dexMod, conMod, intMod, wisMod, chaMod;
    private string strTemp = "0", dexTemp = "0", conTemp = "0", intTemp = "0", wisTemp = "0", chaTemp = "0";
    private int strTempMod, dexTempMod, conTempMod, intTempMod, wisTempMod, chaTempMod;

    //GUI stuff
    private Vector2 detailsScroll = new Vector2();
    private Rect detailsRect = new Rect(0, 0, Screen.width, Screen.height * 0.125f);
    private Vector2 skillsScroll = new Vector2();
    private Rect skillsRect = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f);
    private Vector2 statsScroll = new Vector2();
    private Rect statsRect = new Rect(0, Screen.height * 0.125f, Screen.width * 0.25f, Screen.height * 0.25f);
	void Start () {
        character = (Character)XmlHandler.Instance.Load("Characters//" + Global.Instance.CharacterName + ".xml", typeof(Character));
        if (character == null)
        {
            character = new Character();
            Debug.Log("No character to load");
            character.Name = Global.Instance.CharacterName;
        }
	}

	void Update () {
        if (Input.GetKeyUp(KeyCode.L))
        {
            Skills.Instance.LoadBaseSkills();
        }
        CalculateModifiers();
        CalculateSaves();
	}
    private void CalculateModifiers()
    {

    }
    private void CalculateSaves()
    {
        //fortitude = baseFortitude + conMod;
        //reflex = baseReflex + dexMod;
        //will = baseWill + wisMod;
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
        character.Strength = GUILayout.TextField(character.Strength);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Dexterity");
        character.Dexterity = GUILayout.TextField(character.Dexterity);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Constitution");
        character.Constitution = GUILayout.TextField(character.Constitution);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Intelligence");
        character.Intelligence = GUILayout.TextField(character.Intelligence);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Wisdom");
        character.Wisdom = GUILayout.TextField(character.Wisdom);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Charisma");
        character.Charisma = GUILayout.TextField(character.Charisma);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    private void DrawCharacterDetails()
    {
        GUILayout.BeginArea(detailsRect);
        detailsScroll = GUILayout.BeginScrollView(detailsScroll);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("MENU"))
        {
            Application.LoadLevel("Menu");
        }
        if (GUILayout.Button("SAVE CHARACTER"))
        {         
            character.Save();
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name:"); character.Name = GUILayout.TextField(character.Name);
        //GUILayout.Label("Owner:"); owner = GUILayout.TextField(owner);
        //GUILayout.Label("Race:"); race = GUILayout.TextField(race);
        ////GUILayout.EndHorizontal();
        ////GUILayout.BeginHorizontal();
        //GUILayout.Label("Gender:"); gender = GUILayout.TextField(gender);
        //GUILayout.Label("Age:"); age = GUILayout.TextField(age);
        //GUILayout.Label("Eye:"); eye = GUILayout.TextField(eye);
        //GUILayout.Label("Hair:"); hair = GUILayout.TextField(hair);
        ////GUILayout.EndHorizontal();
        ////GUILayout.BeginHorizontal();
        //GUILayout.Label("Scars/Tats:"); scars = GUILayout.TextField(scars);
        //GUILayout.Label("Height:"); height = GUILayout.TextField(height);
        //GUILayout.Label("Weight:"); weight = GUILayout.TextField(weight);
        //GUILayout.Label("Speed:"); speed = GUILayout.TextField(speed);
        //GUILayout.Label("Exp:"); experience = GUILayout.TextField(experience);
        //GUILayout.Label("Money:"); money = GUILayout.TextField(money);
        GUILayout.EndHorizontal();

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    private void DrawSkills()
    {
        GUILayout.BeginArea(skillsRect);
        if (GUILayout.Button("ADD SKILL"))
        {
            character.Skills.Add(new Skill());
        }
        //if (GUILayout.Button("SAVE SKILLS"))
        //{
        //    Skills.Instance.SkillsList = skills;
        //    Skills.Instance.SaveBaseSkills();
        //}     
        skillsScroll = GUILayout.BeginScrollView(skillsScroll);
        foreach (Skill skill in character.Skills)
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
        foreach(Class c in character.Classes)
        {
            totalLevel += c.Level;
        }
    }
}
