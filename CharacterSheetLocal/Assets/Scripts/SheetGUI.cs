using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SheetGUI : MonoBehaviour {

    private string name, owner, race, gender, age, eye, hair, scars, height, weight, speed;
    private List<string> feats, skills, languages, inventory;
    private List<Class> classes;
    private List<int> levels;
    private int totalLevel, experience, money, 
        strength, dexterity, constitution, intelligence, wisdom, charisma, 
        ac, hp, dr,
        fortitude, reflex, will, baseFortitude, baseReflex, baseWill, 
        baseAttackBonus, initiative;
	void Start () {
        classes = new List<Class>();
        levels = new List<int>();
        feats = new List<string>();
        skills = new List<string>();
        languages = new List<string>();
        inventory = new List<string>();
	}

	void Update () {
        if (Input.GetKeyUp(KeyCode.S))
        {
            Skills.Instance.SaveSkills();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            Skills.Instance.Load();
        }
	}

    void OnGUI()
    {

    }

    private void Save()
    {
        totalLevel = 0;
        foreach(int level in levels)
        {
            totalLevel += level;
        }
    }
}
