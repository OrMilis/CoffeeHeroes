using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.UI;

public class FirstLaunchScript : MonoBehaviour {

    public bool isFirstLaunch = true;
    public bool isMuted = false;
    public bool isPaused = false;
    public int timesPlayed;

    bool isSaving = false;

    public static FirstLaunchScript ins;

    public GameObject TutorialPanel;
    public GameObject AboutPanel;
    public GameObject BiscuitStorePanel;
    public Sprite ReturnBtn;

    public string cloudFileName = "CloudData";
    public SaveData cloudSavedData;

    public GameObject RateUsPopup;

    public GameObject Popup;

    public bool isConnectedToGoogleServices = false;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);
        ins = this;

        timesPlayed = 0;

        //PlayerPrefs.DeleteKey("noAds");

        AboutPanel = GameObject.Find("AboutPanel");

        if (isFirstLaunch)
        {
            setKeys();
            Camera.main.GetComponent<MenuScript>().showTutorial();
            isFirstLaunch = false;
        }

        //Cloud save
        enableCloudSave();
        PlayGamesPlatform.Activate();
        isConnectedToGoogleServices = connectToGoogleServices();
    }

    void setKeys()
    {
        if (!PlayerPrefs.HasKey("First_Time_Launch"))
            PlayerPrefs.SetInt("First_Time_Launch", 0);
        if (!PlayerPrefs.HasKey("currency"))
            PlayerPrefs.SetInt("currency", 0);
        if (!PlayerPrefs.HasKey("Current_Shuriken"))
            PlayerPrefs.SetString("Current_Shuriken", "Default");
        if (!PlayerPrefs.HasKey("Current_Player"))
            PlayerPrefs.SetString("Current_Player", "Default");
        if (!PlayerPrefs.HasKey("Current_Enemy"))
            PlayerPrefs.SetString("Current_Enemy", "Default");
        if (!PlayerPrefs.HasKey("BonusBiscuits"))
            PlayerPrefs.SetInt("BonusBiscuits", 1);
        if (!PlayerPrefs.HasKey("ShowAd"))
            PlayerPrefs.SetInt("ShowAd", 6);
        if (!PlayerPrefs.HasKey("ThrowBags"))
            PlayerPrefs.SetInt("ThrowBags", 0);
        if (!PlayerPrefs.HasKey("PlayerBags"))
            PlayerPrefs.SetInt("PlayerBags", 0);
        if (!PlayerPrefs.HasKey("EnemyBags"))
            PlayerPrefs.SetInt("EnemyBags", 0);
        if (!PlayerPrefs.HasKey("CurrentBag"))
            PlayerPrefs.SetString("CurrentBag", "Throw");
        if (!PlayerPrefs.HasKey("ShowTutorial"))
            PlayerPrefs.SetInt("ShowTutorial", 1);
        if (!PlayerPrefs.HasKey("ShowRateUs"))
            PlayerPrefs.SetInt("ShowRateUs", 1);
        if(!PlayerPrefs.HasKey("noAds"))    //1 = No ads (Dont show ads/ show skipable video), 0 = Show ads 
            PlayerPrefs.SetInt("noAds", 0);
    }

    public void onSceneChangedToMenu()
    {
        if (SceneManager.GetActiveScene().name.Equals("Menu") && PlayerPrefs.GetInt("ShowRateUs") == 1 && timesPlayed >= 8)
        {
            Instantiate(RateUsPopup, GameObject.FindGameObjectWithTag("MainCanvas").transform);
        }
    }

    public void muteUnmute()
    {
        if (isMuted)
        {
            AudioListener.volume = 0;
            isMuted = true;
        }
        if (!isMuted)
        {
             AudioListener.volume = 1;
            isMuted = false;
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        PauseScript.ins.PausePanel.SetActive(true);
        PauseScript.ins.PauseBtn.SetActive(false);
        PauseScript.ins.ClickObj.SetActive(false);

        isPaused = true;

        PauseScript.ins.checkAudio();
    }

    public void unpauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        PauseScript.ins.PausePanel.SetActive(false);
        PauseScript.ins.PauseBtn.SetActive(true);
        PauseScript.ins.ClickObj.SetActive(true);
    }

    public void ToAchievements()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public void ToLeaderboard()
    {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(CoffeeHerosResources.leaderboard_distance);
    }

    public void showBiscuitStore()
    {
        GameObject BiscuitStore = Instantiate(BiscuitStorePanel, GameObject.FindGameObjectWithTag("MainCanvas").transform);
        BiscuitStore.name = "BiscuitStorePanel";
        if (PlayerPrefs.GetInt("noAds") == 0)
        {
            Vector3 BiscuitStoreVector = BiscuitStore.GetComponent<RectTransform>().anchoredPosition;
            BiscuitStoreVector.y += 150;
            BiscuitStore.GetComponent<RectTransform>().anchoredPosition = BiscuitStoreVector;
        }
    }

    public void showAboutPanel()
    {
        GameObject About = Instantiate(AboutPanel, GameObject.FindGameObjectWithTag("MainCanvas").transform);
        if (SceneManager.GetActiveScene().name.Equals("Game"))
        {
            About.transform.SetSiblingIndex(About.transform.GetSiblingIndex());
            About.transform.GetChild(0).GetComponent<Image>().sprite = ReturnBtn;
        }
        else
            About.transform.SetSiblingIndex(About.transform.GetSiblingIndex() - 1);
        About.name = "AboutPanel";
    }

    public void showTutorialPanel()
    {
        if(SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            MenuScript.ins.showTutorialPanel();
        }
        else
        {
            GameObject Tutorial = Instantiate(TutorialPanel, GameObject.FindGameObjectWithTag("MainCanvas").transform);
            Tutorial.name = "TutorialIns";
        }
        
    }

    public void returnToMenu()
    {
        if (SceneManager.GetActiveScene().name.Equals("Menu"))
            MenuScript.ins.closeTutorialAndPlay();
        else
        {
            GameObject Tutorial = GameObject.Find("TutorialIns");
            if (Tutorial != null)
            {
                Destroy(Tutorial);
            }
        }

        GameObject BiscuitStore = GameObject.Find("BiscuitStorePanel");
        GameObject About = GameObject.Find("AboutPanel");

        isPaused = false;

        if (BiscuitStore != null)
        {
            Destroy(BiscuitStore);
        }

        if (About != null)
        {
            Destroy(About);
        }
    }

    public bool connectToGoogleServices()
    {
        if(!isConnectedToGoogleServices)
            PlayGamesPlatform.Instance.Authenticate((bool success) => {
                isConnectedToGoogleServices = success;
                LoadFromCloud();
            });
        return isConnectedToGoogleServices;
    }

    public void Buy100()
    {
        IAPScript.ins.Buy100Biscuits();
    }

    public void Buy250()
    {
        IAPScript.ins.Buy250Biscuits();
    }

    public void Buy400()
    {
        IAPScript.ins.Buy400Biscuits();
    }

    //Enable cloud save
    public void enableCloudSave()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        .Build();
        PlayGamesPlatform.InitializeInstance(config);
    }
    /**/
    public void LoadFromCloud()
    {
        isSaving = false;
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(cloudFileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SavedGameOpened);
    }

    public void SaveToCloud()
    {
        if (Social.localUser.authenticated)
        {
            isSaving = true;
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(cloudFileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SavedGameOpened);
        }

        else
        {

        }
    }

    private void SavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            if (isSaving)
            {
                byte[] data = ToBytes();
                SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
                SavedGameMetadataUpdate updatedMetadata = builder.Build();
                ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                savedGameClient.CommitUpdate(game, updatedMetadata, data, SavedGameWritten);
            }
            else
            {
                ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                savedGameClient.ReadBinaryData(game, SavedGameLoaded);
                
            }
        }
    }
    /**/
    private void SavedGameLoaded(SavedGameRequestStatus status, byte[] data)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            ProcessCloudData(data);
        }
        else
        {

        }
    }
    /**/
    private void SavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {

        }
        else
        {
        }
    }
    /**/
    private void ProcessCloudData(byte[] cloudData)
    {
        if (cloudData == null)
        {

        }
        else
        {
            cloudSavedData = FromBytes(cloudData);

            if (PlayerPrefs.GetInt("First_Time_Launch") == 0)
            {
                PlayerPrefs.SetInt("currency", cloudSavedData.current_Biscuits);
                PlayerPrefs.SetInt("ThrowBags", cloudSavedData.current_Weapon_Bags);
                PlayerPrefs.SetInt("PlayerBags", cloudSavedData.current_Hero_Bags);
                PlayerPrefs.SetInt("EnemyBags", cloudSavedData.current_Enemy_Bags);

                PlayerPrefs.SetInt("First_Time_Launch", 1);
            }

            if(cloudSavedData.high_Score > PlayerPrefs.GetInt("hiScoreKey"))
                PlayerPrefs.SetInt("hiScoreKey", cloudSavedData.high_Score);

            XMLManager.ins.checkSave(cloudSavedData.cloudDB.listE, XMLManager.ins.itemDB.listE);
            XMLManager.ins.checkSave(cloudSavedData.cloudDB.listS, XMLManager.ins.itemDB.listS);
            XMLManager.ins.checkSave(cloudSavedData.cloudDB.listP, XMLManager.ins.itemDB.listP);
            XMLManager.ins.SaveItemsLocal();
        }
    }

    private byte[] ToBytes()
    {
        setSaveData();
        MemoryStream mStream = new MemoryStream();
        BinaryFormatter serializer = new BinaryFormatter();
        serializer.Serialize(mStream, cloudSavedData);
        return mStream.ToArray();
    }

    private SaveData FromBytes(byte[] cloudData)
    {
        MemoryStream mStream = new MemoryStream();
        BinaryFormatter serializer = new BinaryFormatter();
        mStream.Write(cloudData, 0, cloudData.Length);
        mStream.Position = 0;
        SaveData tempSavedData = serializer.Deserialize(mStream) as SaveData;
        mStream.Close();

        return tempSavedData;
    }

    public void setSaveData()
    {
        ItemDatabase saveDB = XMLManager.ins.itemDB;
        int Biscuits = PlayerPrefs.GetInt("currency");
        int Weapon_Bags = PlayerPrefs.GetInt("ThrowBags");
        int Hero_Bags = PlayerPrefs.GetInt("PlayerBags");
        int Enemy_Bags = PlayerPrefs.GetInt("EnemyBags");
        int high_Score;

        if (cloudSavedData.high_Score > PlayerPrefs.GetInt("hiScoreKey"))
            high_Score = cloudSavedData.high_Score;
        else
            high_Score = PlayerPrefs.GetInt("hiScoreKey");


        cloudSavedData = new SaveData(saveDB, Biscuits, Weapon_Bags, Hero_Bags, Enemy_Bags, high_Score);
    }

    //Test purposes
    public void showNoAds()
    {
        GameObject popup = Instantiate(Popup, GameObject.FindGameObjectWithTag("MainCanvas").transform);
        popup.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("noAds").ToString()  + ", " + PlayerPrefs.GetInt("ShowAd").ToString();
        PlayerPrefs.SetInt("noAds", 0);
    }

}

[System.Serializable]
public class SaveData
{
    public ItemDatabase cloudDB;
    public int current_Biscuits;
    public int current_Weapon_Bags;
    public int current_Hero_Bags;
    public int current_Enemy_Bags;
    public int high_Score;

    public SaveData() { }

    public SaveData(ItemDatabase db, int biscuits, int WeaponBags, int HeroBags, int EnemyBags, int hiScore) 
    {
        this.cloudDB = db;
        this.current_Biscuits = biscuits;
        this.current_Weapon_Bags = WeaponBags;
        this.current_Hero_Bags = HeroBags;
        this.current_Enemy_Bags = EnemyBags;
        this.high_Score = hiScore;
    }

    public override string ToString()
    {
        return "B: " + current_Biscuits + "W: " + current_Weapon_Bags + "H: " + current_Hero_Bags +
            "E: " + current_Enemy_Bags + "Hi: " + high_Score + "\n" + cloudDB.ToString();
    }
}