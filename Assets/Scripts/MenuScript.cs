using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Sprite Audio1stSprite;
    public Sprite Audio2ndSprite;
    public Toggle TutorialToggel;
    public GameObject TutorialPanel;
    public static AsyncOperation asopGame;
    public GameObject XMLManager;
    public GameObject FirstLaunchGO;
    public static MenuScript ins;

    public GameObject bottomPanel;
    public GameObject scrollingText;

    // Use this for initialization
    private void Awake()
    {
        if (GameObject.Find("XMLManager") == null)
            Instantiate(XMLManager).name = "XMLManager";
        if (GameObject.Find("FirstLaunchGO") == null)
            Instantiate(FirstLaunchGO).name = "FirstLaunchGO";
        ins = this;
    }

    void Start () {

        Screen.SetResolution(720, 1280, true);
        Camera.main.aspect = 0.5625f;

        if(PlayerPrefs.GetInt("noAds") == 0)
        {
            Vector3 bottomVector = bottomPanel.GetComponent<RectTransform>().anchoredPosition;
            Vector3 textVector = scrollingText.GetComponent<RectTransform>().anchoredPosition;

            bottomVector.y += 150;
            textVector.y += 150;

            bottomPanel.GetComponent<RectTransform>().anchoredPosition = bottomVector;
            scrollingText.GetComponent<RectTransform>().anchoredPosition = textVector;
        }

        XMLManager = GameObject.Find("XMLManager");
        FirstLaunchGO = GameObject.Find("FirstLaunchGO");

        if (!FirstLaunchGO.GetComponent<FirstLaunchScript>().isFirstLaunch)
        {
            FirstLaunchGO.GetComponent<FirstLaunchScript>().muteUnmute();
        }

        checkAudio();
        FirstLaunchScript.ins.onSceneChangedToMenu();

        asopGame = SceneManager.LoadSceneAsync("Game");
        asopGame.allowSceneActivation = false;

        /* StartCoroutine(loadGame());
         StartCoroutine(loadStore());*/

    }

    // Update is called once per frame
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (FirstLaunchScript.ins.isFirstLaunch)
                closeTutorialAndPlay();
            else
            {
                PlayerPrefs.Save();
                Application.Quit();
            }
        }
    }

    public void startGame()
    {
        StartCoroutine(startGameFun());
    }

    public void openCoffeeStore()
    {
        StartCoroutine(openCoffeeStoreScene());
    }

    public void openInventory()
    {
        StartCoroutine(openInventoryScene());
    }

    IEnumerator startGameFun()
    {
        //float fadeTime = GameObject.Find("Menu").GetComponent<Fading>().BeginFade(1);
        ButtonPanelScript.doReverse();
        yield return new WaitForSeconds(0.25f);
        //SceneManager.LoadScene("Game");
        asopGame.allowSceneActivation = true;
    }

    IEnumerator openCoffeeStoreScene()
    {
        //asopGame.allowSceneActivation = true;
        //SceneManager.UnloadScene("Game");
       // float fadeTime = GameObject.Find("Menu").GetComponent<Fading>().BeginFade(1);
        ButtonPanelScript.doReverse();
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("Coffee Store");
    }

    IEnumerator openInventoryScene()
    {
        float fadeTime = GameObject.Find("Menu").GetComponent<Fading>().BeginFade(1);
        ButtonPanelScript.doReverse();
        yield return new WaitForSeconds(fadeTime * 0.2f);
        SceneManager.LoadScene("Inventory");
        //asopGame.allowSceneActivation = true;
    }

    public void changeAudio()
    {
        bool isMuted = FirstLaunchGO.GetComponent<FirstLaunchScript>().isMuted;
        if (!isMuted)
        {
            FirstLaunchGO.GetComponent<FirstLaunchScript>().isMuted = true;
        }
        else if (isMuted)
        {
            FirstLaunchGO.GetComponent<FirstLaunchScript>().isMuted = false;
        }
        checkAudio();
        FirstLaunchGO.GetComponent<FirstLaunchScript>().muteUnmute();
    }

    public void checkAudio()
    {
        Image btnImage = GameObject.Find("Audio").GetComponent<Image>();
        bool isMuted = FirstLaunchGO.GetComponent<FirstLaunchScript>().isMuted;
        if (isMuted)
            btnImage.sprite = Audio2ndSprite;
        else
            btnImage.sprite = Audio1stSprite;
    }

   public void closeTutorialAndPlay()
    {
        if (TutorialToggel.isOn)
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);
            PlayerPrefs.Save();
        }
        TutorialPanel.GetComponent<Image>().CrossFadeAlpha(0, 0.5f, false);
        StartCoroutine(setTutorialOff());
        foreach (Transform gameObject in TutorialPanel.transform)
        {
            gameObject.gameObject.SetActive(false);
        }
    }

    IEnumerator setTutorialOff()
    {
        yield return new WaitForSeconds(0.5f);
        TutorialPanel.SetActive(false);
    }

    public void showTutorial()
    {
        if (PlayerPrefs.GetInt("ShowTutorial") == 1)
        {
            TutorialPanel.SetActive(true);
        }
    }

    public void showTutorialPanel()
    {
        TutorialPanel.SetActive(true);
        foreach (Transform gameObject in TutorialPanel.transform)
        {
            gameObject.gameObject.SetActive(true);
        }
    }
}
