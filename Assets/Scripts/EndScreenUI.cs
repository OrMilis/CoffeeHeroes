using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour {

    Text[] text;

    public GameObject Ads;
    public GameObject AdsBtn;

    public Sprite noAds;
    public Sprite noAdsBtn;

    // Use this for initialization
    void Start()
    {
        text = GetComponentsInChildren<Text>();
        if (PlayerPrefs.GetInt("noAds") == 1)
            changeToNoAds();
    }

    void Update()
    {
        foreach (Text textChild in text)
        {
            if (textChild.name.Equals("LastScore"))
                textChild.text = PlayerScript.distance.ToString("F0");
            if (textChild.name.Equals("HighScore"))
                textChild.text = PlayerPrefs.GetInt("hiScoreKey").ToString();
        }
    }

    public void retryButtonAction()
    {
        if (FirstLaunchScript.ins.isPaused)
            FirstLaunchScript.ins.unpauseGame();
        SceneManager.LoadScene("Game");
        PlayerScript.isStart = true;
    }

    public void returnToMenub()
    {
        ButtonPanelScript.doReverse();
        SceneManager.LoadScene("Menu");
    }

    public void goToInvb()
    {
        SceneManager.LoadScene("Inventory");
    }

    public void goToStoreb()
    {
        SceneManager.LoadScene("Coffee Store");
    }

    public void changeToNoAds()
    {
        Ads.GetComponent<Image>().sprite = noAds;
        AdsBtn.GetComponent<Image>().sprite = noAdsBtn;
    }
}
