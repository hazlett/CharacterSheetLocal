using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerGUI : MonoBehaviour {
    //GUI STUFF
    private Vector2 skillsScroll = new Vector2();
    private Rect skillsRect = new Rect(Screen.width * 0.5f, 25, Screen.width * 0.5f, Screen.height);
    private Vector2 featsScroll = new Vector2();
    private Rect featsRect = new Rect(0, 25, Screen.width * 0.5f, Screen.height);
    
    private List<Skill> skills;
    private List<Feat> feats;
	void Start () {
        skills = Skills.Instance.LoadBaseSkills();
        feats = (List<Feat>)XmlHandler.Instance.Load("feats.xml", typeof(List<Feat>));
	}
	
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("MENU"))
        {
            Application.LoadLevel("Menu");
        }
        GUILayout.EndHorizontal();
        DrawSkills();
        DrawFeats();
    }

    private void DrawSkills()
    {
        GUILayout.BeginArea(skillsRect);
        if (GUILayout.Button("SAVE SKILLS"))
        {
            Skills.Instance.SaveBaseSkills(skills);
        }  
        if (GUILayout.Button("ADD SKILL"))
        {
            skills.Add(new Skill());
        }   
        skillsScroll = GUILayout.BeginScrollView(skillsScroll);
        Skill remove = null;
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
            if (GUILayout.Button("REMOVE"))
            {
                remove = skill;
            }
            GUILayout.EndHorizontal();
        }
        if (remove != null)
        {
            skills.Remove(remove);
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void DrawFeats()
    {
        GUILayout.BeginArea(featsRect);
        if (GUILayout.Button("SAVE FEATS"))
        {
            XmlHandler.Instance.Save("feats.xml", typeof(List<Feat>), feats);
        }
        if (GUILayout.Button("ADD FEAT"))
        {
            feats.Add(new Feat());
        }  
        
        featsScroll = GUILayout.BeginScrollView(featsScroll);
        Feat remove = null;
        foreach (Feat feat in feats)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("Name:"); feat.Name = GUILayout.TextField(feat.Name);
            GUILayout.Label("Description:"); feat.Description = GUILayout.TextField(feat.Description);

            if (GUILayout.Button("REMOVE"))
            {
                remove = feat;
            }

            GUILayout.EndHorizontal();
        }
        if (remove != null)
        {
            feats.Remove(remove);
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
}
