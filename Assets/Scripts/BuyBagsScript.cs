using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBagsScript : MonoBehaviour {

    public string BagName;
    public int BagPrice;


    public void buyBag()
    {
        if (PlayerPrefs.GetInt("currency") >= BagPrice)
        {
            PlayerPrefs.SetInt("currency", PlayerPrefs.GetInt("currency") - BagPrice);
            PlayerPrefs.SetInt(BagName + "Bags", PlayerPrefs.GetInt(BagName + "Bags") + 1);
            if (BagName.Equals("Throw"))
            {
                PlayerPrefs.SetInt(AchievementPlayerKeys.totalWeaponBags,
                    PlayerPrefs.GetInt(AchievementPlayerKeys.totalWeaponBags) + 1);
            }
            else if (BagName.Equals("Player"))
            {
                PlayerPrefs.SetInt(AchievementPlayerKeys.totalHeroBags,
                    PlayerPrefs.GetInt(AchievementPlayerKeys.totalHeroBags) + 1);
            }
            else if (BagName.Equals("Enemy"))
            {
                PlayerPrefs.SetInt(AchievementPlayerKeys.totalEnemyBags,
                    PlayerPrefs.GetInt(AchievementPlayerKeys.totalEnemyBags) + 1);
            }
            AchievementManager.ins.checkBagAchievements();
            FirstLaunchScript.ins.SaveToCloud();
        }
    }
}
