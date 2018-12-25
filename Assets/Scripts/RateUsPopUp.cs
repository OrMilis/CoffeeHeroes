using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsPopUp : MonoBehaviour {

    public void dontShowAgain()
    {
        PlayerPrefs.SetInt("ShowRateUs", 0);
        Destroy(this.gameObject);
    }

    public void remindMeLater()
    {
        FirstLaunchScript.ins.timesPlayed = 0;
        Destroy(this.gameObject);
    }

    public void rateUs()
    {
        PlayerPrefs.SetInt("ShowRateUs", 0);
        Application.OpenURL("market://details?id=com.RabbitRabbit.CoffeeHeros");
        Destroy(this.gameObject);
    }
}
