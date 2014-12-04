using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class SheetGUI : MonoBehaviour {
    private List<Character> characters = new List<Character>();
    private Character character = new Character();
    private string totalLevel = "0", totalInitiative = "0", baseAttackBonus = "0";
    private int strMod, dexMod, conMod, intMod, wisMod, chaMod;
    private int fortitude, reflex, will;
    private string strTemp = "0", dexTemp = "0", conTemp = "0", intTemp = "0", wisTemp = "0", chaTemp = "0";
    private int strTempMod, dexTempMod, conTempMod, intTempMod, wisTempMod, chaTempMod;
    
    private string load = "", campaign = "";


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
        if (Global.Instance.DungeonMaster)
        {
            characters = (List<Character>)XmlHandler.Instance.Load("Campaigns//" + Global.Instance.Campaign + ".xml", typeof(List<Character>));
            character = new Character();
        }
        else
        {
            character = (Character)XmlHandler.Instance.Load("Characters//" + Global.Instance.CharacterName + ".xml", typeof(Character));
            if (character == null)
            {
                character = new Character();
                Debug.Log("No character to load");
                character.Name = Global.Instance.CharacterName;
                characters.Add(character);
            }
        }
        strTemp = character.Strength;
        dexTemp = character.Dexterity;
        conTemp = character.Constitution;
        intTemp = character.Intelligence;
        wisTemp = character.Wisdom;
        chaTemp = character.Charisma;
	}

	void Update () {
        CalculateModifiers();
        CalculateSaves();
	}
    private void CalculateModifiers()
    {
        strMod = Mathf.FloorToInt((int.Parse(character.Strength) / 2) - 5);
        dexMod = Mathf.FloorToInt((int.Parse(character.Dexterity) / 2) - 5);
        conMod = Mathf.FloorToInt((int.Parse(character.Constitution) / 2) - 5);
        intMod = Mathf.FloorToInt((int.Parse(character.Intelligence) / 2) - 5);
        wisMod = Mathf.FloorToInt((int.Parse(character.Wisdom) / 2) - 5);
        chaMod = Mathf.FloorToInt((int.Parse(character.Charisma) / 2) - 5);

        strTempMod = Mathf.FloorToInt((int.Parse(strTemp) / 2) - 5);
        dexTempMod = Mathf.FloorToInt((int.Parse(dexTemp) / 2) - 5);
        conTempMod = Mathf.FloorToInt((int.Parse(conTemp) / 2) - 5);
        intTempMod = Mathf.FloorToInt((int.Parse(intTemp) / 2) - 5);
        wisTempMod = Mathf.FloorToInt((int.Parse(wisTemp) / 2) - 5);
        chaTempMod = Mathf.FloorToInt((int.Parse(chaTemp) / 2) - 5);
    }
    private void CalculateSaves()
    {
        fortitude = int.Parse(character.BaseFortitude) + conTempMod + int.Parse(character.BonusFortitude);
        reflex = int.Parse(character.BaseReflex) + dexTempMod + int.Parse(character.BonusReflex);
        will = int.Parse(character.BaseWill) + wisTempMod + int.Parse(character.BonusWill);
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
        GUILayout.Label("FORT: <b>" + fortitude + "</b>");
        GUILayout.Label("Base:"); character.BaseFortitude = GUILayout.TextField(character.BaseFortitude);
        GUILayout.Label("Bonus:"); character.BonusFortitude = GUILayout.TextField(character.BonusFortitude);
        GUILayout.Label("Mod: " + conTempMod);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("REFLEX: <b>" + reflex + "</b>");
        GUILayout.Label("Base:"); character.BaseReflex = GUILayout.TextField(character.BaseReflex);
        GUILayout.Label("Bonus:"); character.BonusReflex = GUILayout.TextField(character.BonusReflex);
        GUILayout.Label("Mod: " + dexTempMod);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("WILL: <b>" + will + "</b>");
        GUILayout.Label("Base:"); character.BaseWill = GUILayout.TextField(character.BaseWill);
        GUILayout.Label("Bonus:"); character.BonusWill = GUILayout.TextField(character.BonusWill);
        GUILayout.Label("Mod: " + wisTempMod);
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
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Strength");
        character.Strength = GUILayout.TextField(character.Strength);
        GUILayout.Label(" / " + strMod);
        GUILayout.Label("Temp");
        strTemp = GUILayout.TextField(strTemp);
        GUILayout.Label(" / " + strTempMod);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Dexterity");
        character.Dexterity = GUILayout.TextField(character.Dexterity);
        GUILayout.Label(" / " + dexMod);
        GUILayout.Label("Temp");
        dexTemp = GUILayout.TextField(dexTemp);
        GUILayout.Label(" / " + dexTempMod);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Constitution");
        character.Constitution = GUILayout.TextField(character.Constitution);
        GUILayout.Label(" / " + conMod);
        GUILayout.Label("Temp");
        conTemp = GUILayout.TextField(conTemp);
        GUILayout.Label(" / " + conTempMod);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Intelligence");
        character.Intelligence = GUILayout.TextField(character.Intelligence);
        GUILayout.Label(" / " + intMod);
        GUILayout.Label("Temp");
        intTemp = GUILayout.TextField(intTemp);
        GUILayout.Label(" / " + intTempMod);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Wisdom");
        character.Wisdom = GUILayout.TextField(character.Wisdom);
        GUILayout.Label(" / " + wisMod);
        GUILayout.Label("Temp");
        wisTemp = GUILayout.TextField(wisTemp);
        GUILayout.Label(" / " + wisTempMod);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Charisma");
        character.Charisma = GUILayout.TextField(character.Charisma);
        GUILayout.Label(" / " + chaMod);
        GUILayout.Label("Temp");
        chaTemp = GUILayout.TextField(chaTemp);
        GUILayout.Label(" / " + chaTempMod);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        //base attack and stuff

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
    //Still needs work
    private void DrawCharacterDetails()
    {
        GUILayout.BeginArea(detailsRect);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("MENU"))
        {
            Application.LoadLevel("Menu");
        }
        if (GUILayout.Button("SAVE CHARACTER"))
        {
            character.Save();
        }
        if (GUILayout.Button("NEW CHARACTER"))
        {
            character = new Character();
            characters.Add(character);
        }
        foreach(Character c in characters)
        {
            if (GUILayout.Button(c.Name))
            {
                character = c;
            }
        }
        load = GUILayout.TextField(load);
        if (GUILayout.Button("ADD CHARACTER"))
        {
            Character temp = (Character)XmlHandler.Instance.Load("Characters//" + load + ".xml", typeof(Character));
            if (temp == null)
            {
                temp = new Character();
                Debug.Log("No character to load");
                temp.Name = load;
            }
            characters.Add(temp);
        }
        campaign = GUILayout.TextField(campaign);
        if (GUILayout.Button("SAVE CAMPAIGN"))
        {
            XmlHandler.Instance.Save("Campaigns//" + campaign + ".xml", typeof(List<Character>), characters);
        }
        GUILayout.EndHorizontal();
        detailsScroll = GUILayout.BeginScrollView(detailsScroll);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name:"); character.Name = GUILayout.TextField(character.Name);
        GUILayout.Label("Owner:"); character.Owner = GUILayout.TextField(character.Owner);
        GUILayout.Label("Race:"); character.Race = GUILayout.TextField(character.Race);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Gender:"); character.Gender = GUILayout.TextField(character.Gender);
        GUILayout.Label("Age:"); character.Age = GUILayout.TextField(character.Age);
        GUILayout.Label("Eye:"); character.Eye = GUILayout.TextField(character.Eye);
        GUILayout.Label("Hair:"); character.Hair = GUILayout.TextField(character.Hair);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Scars/Tats:"); character.Scars = GUILayout.TextField(character.Scars);
        GUILayout.Label("Height:"); character.Height = GUILayout.TextField(character.Height);
        GUILayout.Label("Weight:"); character.Weight = GUILayout.TextField(character.Weight);
        GUILayout.Label("Speed:"); character.Speed = GUILayout.TextField(character.Speed);
        GUILayout.Label("Exp:"); character.Experience = GUILayout.TextField(character.Experience);
        GUILayout.Label("Money:"); character.Money = GUILayout.TextField(character.Money);
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
