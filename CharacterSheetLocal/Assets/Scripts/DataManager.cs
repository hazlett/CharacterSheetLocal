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
    private int loading = 0, syncing = 0;
    private bool charactersListLoaded = false, loaded = false;
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

    internal string characterDirectory = @"Characters/", campaignDirectory = @"Campaigns/",
        featDirectory = @"Feats/", skillDirectory = @"Skills/",
        itemDirectory = @"Items/", spellDirectory = @"Spells/",
        weaponDirectory = @"Weapons/", armorDirectory = @"Armors/";

    private List<Character> campaignCharacters, allCharacters, allCloudCharacters, allLocalCharacters;
    internal List<Character> CampaignCharacters { get { return campaignCharacters; } }
    internal List<Character> AllCharacters { get { return allCharacters; } }
    internal List<Character> AllCloudCharacters { get { return allCloudCharacters; } }
    internal List<Character> AllLocalCharacters { get { return allLocalCharacters; } }
    public bool Loading { get { return !((loading == 0) && (loaded)); } }
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
#if UNITY_ANDROID
        characterDirectory = Application.persistentDataPath + "/Characters/"; campaignDirectory = Application.persistentDataPath + "/Campaigns/";
        featDirectory = Application.persistentDataPath + "/Feats/"; skillDirectory = Application.persistentDataPath + "/Skills/";
        itemDirectory = Application.persistentDataPath + "/Items/"; spellDirectory = Application.persistentDataPath + "/Spells/";
        weaponDirectory = Application.persistentDataPath + "/Weapons/"; armorDirectory = Application.persistentDataPath + "/Armors/";
#endif
        Refresh();
    }
    void Update()
    {
        if (charactersListLoaded)
        {
            LoadAllCharacters();
        }
    }

    internal void LoadAllCharacters()
    {
        charactersListLoaded = false;
        loaded = false;
        foreach (string name in characters)
        {
            StartCoroutine(AddCharacterToAll(name));
        }
#if !UNITY_WEBPLAYER
        foreach (string name in localCharacters)
        {
            Character c = LoadCharacter(name);

            if (c != null)
            {
                if (!allCharacters.Contains(c))
                {
                    allCharacters.Add(c);
                }
                allLocalCharacters.Add(c);
            }
        }
#endif
    }
    private IEnumerator AddCharacterToAll(string name)
    {
        loading++;
        WWWForm form = new WWWForm();
        form.AddField("fileName", name);
        WWW www = new WWW(getCharacterURL, form);
        yield return www;
        if (www.error == null)
        {
            Character c = DeserializeToCharacter(www.text);
            if (c != null)
            {
                allCloudCharacters.Add(c);
                allCharacters.Add(c);               
            }
        }
        else
        {
#if !UNITY_WEBPLAYER
            AddLocalCharacterToCampaign(name);
#endif
        }
        loading--;
        loaded = true;
    }
    internal Character DeserializeToCharacter(string text)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);

        Character obj = new Character();
        XmlSerializer serializer = new XmlSerializer(typeof(Character));
        XmlReader reader = new XmlNodeReader(doc);

        obj = serializer.Deserialize(reader) as Character;

        return obj;
    }
    private void AddLocalCharacterToCampaign(string name)
    {
        Character c = LoadCharacter(name);
        if (c != null)
        {
            CampaignCharacters.Add(c);
        }
    }
    public void Refresh()
    {
        var connectionTestResult = Network.TestConnection();
        Debug.Log(connectionTestResult.ToString());
        characters = new List<string>();
        campaigns = new List<string>();
        feats = new List<string>();
        skills = new List<string>();
        localCharacters = new List<string>();
        localCampaigns = new List<string>();
        localFeats = new List<string>();
        localSkills = new List<string>();
        allLocalCharacters = new List<Character>();

#if !UNITY_WEBPLAYER
        localCharacters = GetList(characterDirectory);
        localCharacters.Sort();
#if !UNITY_ANDROID
        localCampaigns = GetList(campaignDirectory);
        localFeats = GetList(featDirectory);
        localSkills = GetList(skillDirectory);
        localItems = GetList(itemDirectory);
        localSpells = GetList(spellDirectory);
        localWeapons = GetList(weaponDirectory);
        localArmors = GetList(armorDirectory);
#endif
 
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
        allCharacters = new List<Character>();
        allCloudCharacters = new List<Character>();
        charactersListLoaded = false;
        WWW www = new WWW(getCharactersURL);
        yield return www;
        if (www.error == null)
        {
            characters = (List<string>)DeserializeList(www.text);
            if (characters == null)
            {
                characters = new List<string>();
            }
        }
        characters.Sort();
        charactersListLoaded = true;
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
    /// Syncs the server to local. 
    /// If overwrite=true the server files will overwrite local versions with the same name
    /// </summary>
    /// <param name="overwrite"></param>
    internal void SyncWithServer(bool overwrite = false)
    {
#if !UNITY_WEBPLAYER
        allLocalCharacters = new List<Character>();
        foreach (Character c in allCloudCharacters)
        {
            if (!localCharacters.Contains(c.Name) || overwrite)
            {
                c.Save();
            }
        }
        Refresh();
#endif
    }
    /// <summary>
    /// Syncs the local to the server.
    /// If overwrite=true the local files will overwrite the server versions with the same name
    /// ONLY ADMIN SHOULD BE ABLE TO OVERWRITE THE SERVER
    /// </summary>
    /// <param name="overwrite"></param>
    internal void SyncToServer(bool overwrite = false)
    {
#if !UNITY_WEBPLAYER
        string overwriteString = "false";
        syncing = 0;
        if (overwrite)
        {
            overwriteString = "true";
        }
        foreach (Character c in allLocalCharacters)
        {
            StartCoroutine(SendCharacter(c, overwriteString));
        }
#endif
    }

    //Server methods
    internal IEnumerator SendCharacter(Character character, string overwrite)
    {
        syncing++;
        XmlSerializer xmls = new XmlSerializer(typeof(Character));
        StringWriter writer = new StringWriter();
        xmls.Serialize(writer, character);
        writer.Close();
        WWWForm form = new WWWForm();
        form.AddField("name", character.Name);
        form.AddField("file", writer.ToString());
        form.AddField("overwrite", overwrite);
        WWW www = new WWW(sendCharacterURL, form);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("Send Successful");
        }
        else
        {
            Debug.Log("Send ERROR: " + www.error);
        }
        syncing--;
        if (syncing == 0)
        {
            Refresh();
        }
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
        return XmlHandler.Instance.Load(characterDirectory + name + ".xml", typeof(Character)) as Character;
    }
    internal void SaveCharacter(Character character)
    {
        character.Save();
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
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);                
            }
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
