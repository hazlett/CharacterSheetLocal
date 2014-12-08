using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudHandler {

    private static CloudHandler instance = new CloudHandler();
    public static CloudHandler Instance { get { return instance; } }

    private string getCharacterURL, sendCharacterURL,
        sendCampaignURL, getCampaignURL = "http://hazlett206.ddns.net/DND/SaveCampaign.php",
        getFeatURL, sendFeatURL, 
        getSkillsURL, sendSkillsURL;


    public Character GetCharacter(string name)
    {
        return null;
    }
    public void SendCharacter(Character character)
    {

    }
    public Campaign GetCampaign(string name)
    {
        return null;
    }
    public void SendCampaign(Campaign campaign)
    {

    }
    public Feat GetFeat()
    {
        return null;
    }
    public void SendFeat(Feat feat)
    {

    }
    public Skill GetSkill()
    {
        return null;
    }
    public void SendSkill()
    {

    }

}
