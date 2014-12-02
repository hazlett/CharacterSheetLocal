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

    //Both sides (header)
    private Vector2 detailsScroll = new Vector2();
    private Rect detailsRect = new Rect(0, 0, Screen.width, Screen.height * 0.125f);
    //Left side
    private Vector2 statsScroll = new Vector2();
    private Rect statsRect = new Rect(0, Screen.height * 0.125f, Screen.width * 0.25f, Screen.height * 0.25f);
    private Vector2 weaponsScroll = new Vector2();
    private Rect weaponsRect = new Rect(0, Screen.height * 0.375f, Screen.width * 0.5f, Screen.height * 0.25f);
    private Vector2 featsScroll = new Vector2();
    private Rect featsRect = new Rect(0, Screen.height * 0.625f, Screen.width * 0.5f, Screen.height * 0.25f);
    private Vector2 inventoryScroll = new Vector2();
    private Rect inventoryRect = new Rect(0, Screen.height * 0.875f, Screen.width * 0.5f, Screen.height * 0.125f);
    //Right side
    private Vector2 savesScroll = new Vector2();
    private Rect savesRect = new Rect(Screen.width * 0.5f, Screen.height * 0.125f, Screen.width * 0.5f, Screen.height * 0.25f);
    private Vector2 skillsScroll = new Vector2();
    private Rect skillsRect = new Rect(Screen.width * 0.5f, Screen.height * 0.375f, Screen.width * 0.5f, Screen.height * 0.5f);
    private Vector2 languagesScroll = new Vector2();
    private Rect languagesRect = new Rect(Screen.width * 0.5f, Screen.height * 0.875f, Screen.width * 0.5f, Screen.height * 0.125f);

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
        DrawWeapons();
        DrawFeats();
        DrawInventory();
        DrawSaves();
        DrawLanguages();
    }

    private void DrawWeapons() 
    {
        GUILayout.BeginArea(weaponsRect);
        weaponsScroll = GUILayout.BeginScrollView(weaponsScroll);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Armor:"); character.EquippedArmor.Name = GUILayout.TextField(character.EquippedArmor.Name);
        GUILayout.Label("AC:"); character.EquippedArmor.ACBonus = GUILayout.TextField(character.EquippedArmor.ACBonus);
        GUILayout.Label("DR:"); character.EquippedArmor.DR = GUILayout.TextField(character.EquippedArmor.DR);
        GUILayout.Label("ACP:"); character.EquippedArmor.ArmorCheckPenalty = GUILayout.TextField(character.EquippedArmor.ArmorCheckPenalty);
        GUILayout.Label("Max DEX:"); character.EquippedArmor.MaxDex = GUILayout.TextField(character.EquippedArmor.MaxDex);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Notes:"); character.EquippedArmor.Description = GUILayout.TextField(character.EquippedArmor.Description);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ADD WEAPON"))
        {
            character.Weapons.Add(new Weapon());
        }
        GUILayout.EndHorizontal();
        Weapon remove = null;
        foreach(Weapon weapon in character.Weapons)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Weapon:"); weapon.Name = GUILayout.TextField(weapon.Name);
            if (weapon.Ranged)
            {
                if (GUILayout.Button("Ranged"))
                {
                    weapon.Ranged = !weapon.Ranged;
                }
            }
            else
            {
                if (GUILayout.Button("Melee"))
                {
                    weapon.Ranged = !weapon.Ranged;
                }
            }
            GUILayout.Label("Hit Bonus:"); weapon.HitBonus = GUILayout.TextField(weapon.HitBonus);
            GUILayout.Label("Damage:"); weapon.Damage = GUILayout.TextField(weapon.Damage);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Notes:"); weapon.Description = GUILayout.TextField(weapon.Description);
            if (GUILayout.Button("REMOVE WEAPON"))
            {
                remove = weapon;
            }
            GUILayout.EndHorizontal();
        }
        if (remove != null)
        {
            character.Weapons.Remove(remove);
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    private void DrawFeats()
    {
        GUILayout.BeginArea(featsRect);
        featsScroll = GUILayout.BeginScrollView(featsScroll);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ADD FEAT"))
        {
            character.Feats.Add(new Feat());
        }
        GUILayout.EndHorizontal();
        Feat remove = null;
        foreach (Feat feat in character.Feats)
        {
            GUILayout.BeginHorizontal();
            feat.Name = GUILayout.TextField(feat.Name);
            GUILayout.Label("Description:"); feat.Description = GUILayout.TextField(feat.Description);
            if (GUILayout.Button("REMOVE"))
            {
                remove = feat;
            }
            GUILayout.EndHorizontal();
        }
        if (remove != null)
        {
            character.Feats.Remove(remove);
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    private void DrawInventory()
    {
        GUILayout.BeginArea(inventoryRect);
        inventoryScroll = GUILayout.BeginScrollView(inventoryScroll);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ADD ITEM"))
        {
            character.Inventory.Add(new Item());
        }
        GUILayout.EndHorizontal();
        Item remove = null;
        foreach (Item item in character.Inventory)
        {
            GUILayout.BeginHorizontal();
            item.Name = GUILayout.TextField(item.Name);
            GUILayout.Label("Description:"); item.Description = GUILayout.TextField(item.Description);
            if (GUILayout.Button("REMOVE"))
            {
                remove = item;
            }
            GUILayout.EndHorizontal();
        }
        if (remove != null)
        {
            character.Inventory.Remove(remove);
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    private void DrawSaves()
    {
        GUILayout.BeginArea(savesRect);
        savesScroll = GUILayout.BeginScrollView(savesScroll);
 
        GUILayout.BeginHorizontal();
        GUILayout.Label("HP:"); character.CurrentHP = GUILayout.TextField(character.CurrentHP);
        GUILayout.Label(" / "); character.MaxHP = GUILayout.TextField(character.MaxHP);
        GUILayout.Label("DR:"); character.DR = GUILayout.TextField(character.DR);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("AC:"); character.AC = GUILayout.TextField(character.AC); GUILayout.Label("MISC:"); GUILayout.TextField(character.ACMiscBonus);
        GUILayout.Label("Shield:"); character.ShieldBonus = GUILayout.TextField(character.ShieldBonus);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Base:"); character.BaseFortitude = GUILayout.TextField(character.BaseFortitude);
        GUILayout.Label("Bonus:"); character.BonusFortitude = GUILayout.TextField(character.BonusFortitude);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Base:"); character.BaseReflex = GUILayout.TextField(character.BaseReflex);
        GUILayout.Label("Bonus:"); character.BonusReflex = GUILayout.TextField(character.BonusReflex);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Base:"); character.BaseWill = GUILayout.TextField(character.BaseWill);
        GUILayout.Label("Bonus:"); character.BonusWill = GUILayout.TextField(character.BonusWill);
        GUILayout.EndHorizontal();
    
    
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    private void DrawLanguages()
    {
        GUILayout.BeginArea(languagesRect);
        languagesScroll = GUILayout.BeginScrollView(languagesScroll);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ADD LANGUAGE"))
        {
            character.Languages.Add("");
        }
        GUILayout.EndHorizontal();
        string remove = null;
        GUILayout.BeginHorizontal();
        for(int i = 0; i < character.Languages.Count; i++)
        {
            character.Languages[i] = GUILayout.TextField(character.Languages[i]);
        }
        GUILayout.EndHorizontal();
        if (remove != null)
        {
            character.Languages.Remove(remove);
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    //Still needs work
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
    //Still needs work
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
