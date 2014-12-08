using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;

public class DataManager : MonoBehaviour {
    private static DataManager instance;
    public static DataManager Instance { get { return instance; } }

    private List<string> characters, campaigns, feats, skills, items, spells, weapons, armors, 
        localCharacters, localCampaigns, localFeats, localSkills, localItems, localSpells, localWeapons, localArmors;

    //PHP URLs  ///TODO: items, spells, weapons, armor
    private string getCharacterURL = "http://hazlett206.ddns.net/DND/LoadCharacter.php", sendCharacterURL = "http://hazlett206.ddns.net/DND/SaveCharacter.php",
        getCampaignURL = "http://hazlett206.ddns.net/DND/LoadCampaign.php", sendCampaignURL = "http://hazlett206.ddns.net/DND/SaveCampaign.php",
        getFeatURL = "http://hazlett206.ddns.net/DND/GetCampaigns.php", sendFeatURL,
        getSkillURL = "http://hazlett206.ddns.net/DND/GetCampaigns.php", sendSkillURL,
        getCharactersURL = "http://hazlett206.ddns.net/DND/GetCharacters.php", getCampaignsURL = "http://hazlett206.ddns.net/DND/GetCampaigns.php",
        getFeatsURL = "http://hazlett206.ddns.net/DND/LoadFeats.php", getSkillsURL = "http://hazlett206.ddns.net/DND/LoadSkills.php";
    //Local directories  ///TODO: items, spells, weapons, armor
    private string characterDirectory = @"Characters/", campaignDirectory = @"Campaigns/",
        featDirectory = @"Feats/", skillDirectory = @"Skills/",
        itemDirectory = @"Items/", spellDirectory = @"Spells/",
        weaponDirectory = @"Weapons/", armorDirectory = @"Armors/";

    internal string CharacterURL { get { return getCharacterURL; } }
    internal string CampaignURL { get { return getCampaignURL; } }
    internal string FeatURL { get { return getFeatURL; } }
    internal string SkillURL { get { return getSkillURL; } }
    internal List<string> Characters { get { return characters; } }
    internal List<string> Campaigns { get { return campaigns; } }
    internal List<string> Feats { get { return feats; } }
    internal List<string> Skills { get { return skills; } }
    internal List<string> LocalCharacters { get { return localCharacters; } }
    internal List<string> LocalCampaigns { get { return localCampaigns; } }
    internal List<string> LocalFeats { get { return localFeats; } }
    internal List<string> LocalSkills { get { return localSkills; } }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        Refresh();
    }

    public void Refresh()
    {
        characters = new List<string>();
        campaigns = new List<string>();
        feats = new List<string>();
        skills = new List<string>();
        localCharacters = new List<string>();
        localCampaigns = new List<string>();
        localFeats = new List<string>();
        localSkills = new List<string>();

#if !UNITY_WEBPLAYER
        localCharacters = GetList(characterDirectory);
        localCampaigns = GetList(campaignDirectory);
        localFeats = GetList(featDirectory);
        localSkills = GetList(skillDirectory);
        localItems = GetList(itemDirectory);
        localSpells = GetList(spellDirectory);
        localWeapons = GetList(weaponDirectory);
        localArmors = GetList(armorDirectory);
#endif

        StartCoroutine(RefreshCharacters());
        StartCoroutine(RefreshCampaigns());
        //StartCoroutine(RefreshFeats());
        //StartCoroutine(RefreshSkills());
        //StartCoroutine(RefreshItems());
        //StartCoroutine(RefreshSpells());
        //StartCoroutine(RefreshWeaponss());
        //StartCoroutine(RefreshArmors());
    }

    private IEnumerator RefreshCharacters()
    {
        WWW www = new WWW(getCharactersURL);
        yield return www;
        if (www.error == null)
        {
            characters = (List<string>)DeserializeList(www.text);
        }
    }
    private IEnumerator RefreshCampaigns()
    {
        WWW www = new WWW(getCampaignsURL);
        yield return www;
        if (www.error == null)
        {
            campaigns = (List<string>)DeserializeList(www.text);
        }
    }
    private IEnumerator RefreshFeats()
    {
        WWW www = new WWW(getFeatsURL);
        yield return www;
        if (www.error == null)
        {
            feats = (List<string>)DeserializeList(www.text);
        }
    }
    private IEnumerator RefreshSkills()
    {
        WWW www = new WWW(getSkillsURL);
        yield return www;
        if (www.error == null)
        {
            skills = (List<string>)DeserializeList(www.text);
        }
    }
    //private IEnumerator RefreshSpells()
    //{
    //    WWW www = new WWW(getSpellsURL);
    //    yield return www;
    //    if (www.error == null)
    //    {
    //        spells = (List<string>)DeserializeList(www.text);
    //    }
    //}
    //private IEnumerator RefreshItems()
    //{
    //    WWW www = new WWW(getItemsURL);
    //    yield return www;
    //    if (www.error == null)
    //    {
    //        items = (List<string>)DeserializeList(www.text);
    //    }
    //}
    //private IEnumerator RefreshWeapons()
    //{
    //    WWW www = new WWW(getWeaponsURL);
    //    yield return www;
    //    if (www.error == null)
    //    {
    //        weapons = (List<string>)DeserializeList(www.text);
    //    }
    //}
    //private IEnumerator RefreshArmors()
    //{
    //    WWW www = new WWW(getArmorsURL);
    //    yield return www;
    //    if (www.error == null)
    //    {
    //        armors = (List<string>)DeserializeList(www.text);
    //    }
    //}
    /// <summary>
    /// Syncs the local directories with the server. 
    /// If overwrite=true the server files will overwrite local versions with the same name
    /// </summary>
    /// <param name="overwrite"></param>
    internal void SyncWithServer(bool overwrite = false)
    {

    }
    /// <summary>
    /// Syncs the server to the local files you have.
    /// If overwrite=true the local files will overwrite the server versions with the same name
    /// ONLY ADMIN SHOULD BE ABLE TO OVERWRITE THE SERVER
    /// </summary>
    /// <param name="overwrite"></param>
    internal void SyncToServer(bool overwrite = false)
    {

    }

    //Server methods
    internal void SendCharacter(Character character)
    {
        
    }

    internal void SendCampaign(Campaign campaign)
    {

    }

    internal void SendFeat(Feat feat)
    {

    }

    internal void SendSkill(Skill skill)
    {

    }


    //Local methods
    internal Character LoadCharacter(string name)
    {
        return null;
    }
    internal void SaveCharacter(Character character)
    {

    }
    internal Campaign LoadCampaign(string name)
    {
        return null;
    }
    internal void SaveCampaign(Campaign campaign)
    {

    }
    internal Feat LoadFeat()
    {
        return null;
    }
    internal void SaveFeat(Feat feat)
    {

    }
    internal Skill LoadSkill()
    {
        return null;
    }
    internal void SaveSkill()
    {

    }

    private List<string> GetList(string directory)
    {        
        List<string> list = new List<string>();
#if !UNITY_WEBPLAYER
        try
        {
            string[] files = Directory.GetFiles(directory);
            foreach (string file in files)
            {
                string name = file;
                name = name.Replace(directory, "");
                name = name.Replace(".xml", "");
                list.Add(name);
            }
        }
        catch (Exception) { }
#endif
        return list;
    }

    private List<string> DeserializeList(string text)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);
        XmlSerializer xmls = new XmlSerializer(typeof(List<string>));
        XmlReader reader = new XmlNodeReader(doc);
        List<string> list = xmls.Deserialize(reader) as List<string>;
        if (list == null)
        {
            list = new List<string>();
        }
        return list;
    }
}
