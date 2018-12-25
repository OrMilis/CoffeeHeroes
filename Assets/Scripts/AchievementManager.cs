using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

    public static AchievementManager ins;

    public int killCount = 0;

    // Use this for initialization
    void Awake () {
        ins = this;
        setAchievemenKeys();
    }

    public void setAchievemenKeys()
    {
        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.totalWeaponBags))
            PlayerPrefs.SetInt(AchievementPlayerKeys.totalWeaponBags, 0);

        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.totalHeroBags))
            PlayerPrefs.SetInt(AchievementPlayerKeys.totalHeroBags, 0);

        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.totalEnemyBags))
            PlayerPrefs.SetInt(AchievementPlayerKeys.totalEnemyBags, 0);

        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.isNoviceAchievementUnlocked))
            PlayerPrefs.SetInt(AchievementPlayerKeys.isNoviceAchievementUnlocked, 0);

        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.isSaviorAchievementUnlocked))
            PlayerPrefs.SetInt(AchievementPlayerKeys.isSaviorAchievementUnlocked, 0);

        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.isChampionAchievementUnlocked))
            PlayerPrefs.SetInt(AchievementPlayerKeys.isChampionAchievementUnlocked, 0);

        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.isCheaterAchievementUnlocked))
            PlayerPrefs.SetInt(AchievementPlayerKeys.isCheaterAchievementUnlocked, 0);

        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.isExecutionerAchievementUnlocked))
            PlayerPrefs.SetInt(AchievementPlayerKeys.isExecutionerAchievementUnlocked, 0);

        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.isNoMercyAchievementUnlocked))
            PlayerPrefs.SetInt(AchievementPlayerKeys.isNoMercyAchievementUnlocked, 0);

        if (!PlayerPrefs.HasKey(AchievementPlayerKeys.isSlayerAchievementUnlocked))
            PlayerPrefs.SetInt(AchievementPlayerKeys.isSlayerAchievementUnlocked, 0);
    }

    public void runXMetersAchievement(float distance)
    {
        if (PlayerPrefs.GetInt(AchievementPlayerKeys.isNoviceAchievementUnlocked) == 0 &&
                                                                                distance >= 100f && distance < 250f)        //Double check a range.
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_novice, 100.0f, (bool success) => {
                if (success)
                    PlayerPrefs.SetInt(AchievementPlayerKeys.isNoviceAchievementUnlocked, 1);
            });     
        }
        else if (PlayerPrefs.GetInt(AchievementPlayerKeys.isSaviorAchievementUnlocked) == 0 &&
                                                                                distance >= 250f && distance < 500f)        //Double check a range.
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_savior, 100.0f, (bool success) => {
                if (success)
                    PlayerPrefs.SetInt(AchievementPlayerKeys.isSaviorAchievementUnlocked, 1);
            });      
        }
        else if (PlayerPrefs.GetInt(AchievementPlayerKeys.isChampionAchievementUnlocked) == 0 &&
                                                                                distance >= 500f && distance < 1000f)       //Double check a range.         
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_champion, 100.0f, (bool success) => {
                if (success)
                    PlayerPrefs.SetInt(AchievementPlayerKeys.isChampionAchievementUnlocked, 1);
            });   
        }
        else if (PlayerPrefs.GetInt(AchievementPlayerKeys.isCheaterAchievementUnlocked) == 0 &&
                                                                                distance >= 1000f)                          //Double check a range.
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_cheater, 100.0f, (bool success) => {
                if (success)
                    PlayerPrefs.SetInt(AchievementPlayerKeys.isCheaterAchievementUnlocked, 1);
            });
        }
    }

    public void checkBagAchievements()
    {
        int totalWeaponBags = PlayerPrefs.GetInt(AchievementPlayerKeys.totalWeaponBags);
        int totalHeroBags = PlayerPrefs.GetInt(AchievementPlayerKeys.totalHeroBags);
        int totalEnemyBags = PlayerPrefs.GetInt(AchievementPlayerKeys.totalEnemyBags);

        if (totalWeaponBags == 2 && totalHeroBags == 2 && totalEnemyBags == 2)
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_wage_earner, 100.0f, (bool success) =>{ });
        }
        if (totalWeaponBags == 5 && totalHeroBags == 5 && totalEnemyBags == 5)
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_money_maker, 100.0f, (bool success) =>{ });
        }
        if (totalWeaponBags == 10 && totalHeroBags == 10 && totalEnemyBags == 10)
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_well_heeled, 100.0f, (bool success) => { });
        }
    }

    public void killsAchievements()
    {
        if (PlayerPrefs.GetInt(AchievementPlayerKeys.isExecutionerAchievementUnlocked) == 0 &&
                                                                            killCount >= 50 && killCount < 100)     //Double check a range.
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_executioner, 100.0f, (bool success) => {
                if (success)
                    PlayerPrefs.SetInt(AchievementPlayerKeys.isExecutionerAchievementUnlocked, 1);
            }); 
        }
        else if (PlayerPrefs.GetInt(AchievementPlayerKeys.isNoMercyAchievementUnlocked) == 0 &&
                                                                            killCount >= 100 && killCount < 200)    //Double check a range.
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_no_mercy, 100.0f, (bool success) => {
                if (success)
                    PlayerPrefs.SetInt(AchievementPlayerKeys.isNoMercyAchievementUnlocked, 1);
            });    
        }
        else if (PlayerPrefs.GetInt(AchievementPlayerKeys.isSlayerAchievementUnlocked) == 0 &&
                                                                        killCount >= 200)                           //Double check a range.
        {
            Social.ReportProgress(CoffeeHerosResources.achievement_slayer, 100.0f, (bool success) => {
                if (success)
                    PlayerPrefs.SetInt(AchievementPlayerKeys.isSlayerAchievementUnlocked, 1);
            });      
        }
    }
}

public static class AchievementPlayerKeys
{
    public const string totalWeaponBags = "Total_Weapon_Coffee_Bags_Bought";
    public const string totalHeroBags = "Total_Hero_Coffee_Bags_Bought";
    public const string totalEnemyBags = "Total_Enemy_Coffee_Bags_Bought";

    // 1 - True 0 - False
    public const string isNoviceAchievementUnlocked = "isNoviceUnlocked";
    public const string isSaviorAchievementUnlocked = "isSaviorUnlocked";
    public const string isChampionAchievementUnlocked = "isChampionUnlocked";
    public const string isCheaterAchievementUnlocked = "isCheaterUnlocked";
    public const string isExecutionerAchievementUnlocked = "isExecutionerUnlocked";
    public const string isNoMercyAchievementUnlocked = "isNoMercyUnlocked";
    public const string isSlayerAchievementUnlocked = "isSlayerUnlocked";
}
