using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class PlayerScript : MonoBehaviour {
    Vector3 velocity;
    UnityEngine.Transform playerPos;
    UnityEngine.Transform playerChar;
    public GameObject prefab;
    public GameObject endUIScreen;
    public GameObject AdsPanel;
    public GameObject HealthUI;
    public GameObject AmmoUI;


    public GameObject bottomPanel;
    public GameObject adsPanel;

    Vector3 startPoint;
    Vector3 angles;
    public static float forwardSpeed;
    public float baseSpeed = 5f;
    public float maxSpeed = 15f;
    public float HUDdistance = 5f;
    public float maxSpeedDistance = 300f;
    public static float distance;
    public static int numOfLives;
    public static int numOfShots;
    private string hiScoreKey = "hiScoreKey";
    public static int oneRunBiscuit;
    public static bool isDead;
    public static bool isStart = false;
    public bool godMod = false;
    float timer;
    const float waitTime = 3f;
    bool showTutorial;

    Animator rigAni;
    public GameObject Rig;

    // Use this for initialization
    void Awake () {

        Time.timeScale = 1;
        FirstLaunchScript.ins.isPaused = false;

        startGame();

        buildRig();

        GameObject player = this.gameObject;
        rigAni = Rig.GetComponent<Animator>();

        if (player == null)
        {
            Debug.LogError("Cant find object with Tag!");
            return;
        }

        playerPos = player.transform;
        startPoint = playerPos.transform.position;

        
        playerPos.transform.position = startPoint;
        playerChar = playerPos.GetChild(0);

        FirstLaunchScript.ins.timesPlayed++;

        /*endUIScreen.SetActive(false);
        AdsPanel.SetActive(false);*/
    }
	
	// Update is called once per frame
	void Update () {
        if (isStart)
            startGame();
        Vector3 currnetPos = playerPos.transform.position;
        Vector3 charPos = playerChar.transform.position;
        distance = Vector3.Distance(startPoint, currnetPos) / HUDdistance;
        AchievementManager.ins.runXMetersAchievement(distance);
        //Debug.Log("Distance: " + distance.ToString("F0"));


        if (!isDead && numOfShots > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out hit))
                    if(hit.transform.name == "ClickObj")
                        shootEnemy(charPos);

            }
            /*if(Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    shootEnemy(charPos);
            }*/
        }


        if (Input.GetKeyDown(KeyCode.Escape) && isDead)
        {
            SceneManager.LoadScene("Menu");
            ButtonPanelScript.doReverse();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isDead)
        {
            if (Time.timeScale != 0)
                FirstLaunchScript.ins.pauseGame();
            else
                FirstLaunchScript.ins.unpauseGame();
        }

        if (!isDead)
        {
            if (numOfLives <= 0 && !godMod)
            {
                endGame();
            }
            shotTimer();
        }
    }
        


    void FixedUpdate()
    {
        if (isStart)
            startGame();
        if(!isDead)
         forwardSpeed += ((Mathf.Pow(maxSpeed / 5, 2) - Mathf.Pow(baseSpeed / 5, 2)) / (2 * maxSpeedDistance)) / 10;

        if (forwardSpeed > maxSpeed)
            forwardSpeed = maxSpeed;

        velocity.x += forwardSpeed;
        transform.position = velocity * Time.deltaTime;
        // Debug.Log("Speed: " + forwardSpeed.ToString("F3"));
    }

    void shootEnemy(Vector3 charPos)
    {
        StartCoroutine(shoot());
        rigAni.CrossFade("RigShot", 0.25f);
        Instantiate(prefab, playerChar.transform.position + new Vector3(0f, 0f, 0.1f), playerChar.transform.rotation);
        numOfShots--;

    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(0.2f);
        if(!isDead)
            rigAni.CrossFade("RigWalk", 0.25f);
        else
            rigAni.CrossFade("RigDeath", 0.25f);
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(0.1f);
        rigAni.CrossFade("RigDeath", 0.25f);
    }

    void buildRig()
    {
        foreach (Transform child in Rig.transform)
        {
            if (!child.name.Contains("Leg"))
            {   
                Sprite sprite = Resources.Load<Sprite>("Player/" + child.name + "/" + PlayerPrefs.GetString("Current_Player"));
                if (sprite == null)
                    sprite = Resources.Load<Sprite>("Player/" + child.name + "/" + "Default");
                else
                    child.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
            //rigAni.Play("RigWalk");
        //rigAni.
    }

    void checkHiScore(float currentScore)
    {
        if (currentScore > PlayerPrefs.GetInt(hiScoreKey))
        {
            PlayerPrefs.SetInt(hiScoreKey, Mathf.RoundToInt(currentScore));
        }
        PlayerPrefs.Save();
    }

    void endGame()
    {
        isDead = true;
        forwardSpeed = 0;
        StartCoroutine(death());
        SkySpeedScript.setSpeed(0);
        EnemyPosScript.setSpeed(0);
        FirstLaunchScript.ins.SaveToCloud();
        AchievementManager.ins.killCount = 0;
        //EndScreenUI.setPanelActive();
        setEndScreenActive();
        if(distance > 10)
            checkShowAd();

        checkHiScore(distance);

        if (Social.localUser.authenticated)
            Social.ReportScore((long)distance, CoffeeHerosResources.leaderboard_distance, (bool success) => { });

        PlayerPrefs.SetInt("BonusBiscuits", 1);
        PlayerPrefs.Save();
    }

    void startGame()
    {
        velocity = Vector3.zero;
        forwardSpeed = baseSpeed;
        isDead = false;
        numOfLives = 3;
        numOfShots = 3;
        distance = 0;
        timer = 0;
        oneRunBiscuit = 0;
        SkySpeedScript.setSpeed(SkySpeedScript.constSpeed) ;
        EnemyPosScript.setSpeed(EnemyPosScript.constSpeed);
        isStart = false;
    }

    public static void addShot()
    {
        if (numOfShots < 3)
            numOfShots++;
    }

    private void shotTimer()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            addShot();
            timer = 0;
        }
    }

    private void setEndScreenActive()
    {
        endUIScreen.SetActive(true);
        HealthUI.SetActive(false);
        AmmoUI.SetActive(false);
        PauseScript.ins.PauseBtn.SetActive(false);

        if (PlayerPrefs.GetInt("noAds") == 0)
        {
            Vector3 bottomVector = bottomPanel.GetComponent<RectTransform>().anchoredPosition;
            Vector3 adsVector = adsPanel.GetComponent<RectTransform>().anchoredPosition;
            bottomVector.y += 150;
            adsVector.y += 150;
            bottomPanel.GetComponent<RectTransform>().anchoredPosition = bottomVector;
            adsPanel.GetComponent<RectTransform>().anchoredPosition = adsVector;
        }
    }
    
    private void checkShowAd()
    {
        int doubleBiscuitChance = (int)((Random.value * 10) % 10);
        bool isChanged = false;
        if (PlayerPrefs.GetInt("ShowAd") == -1)
        {
            AdsPanel.SetActive(true);
            isChanged = true;
        }
        else if (PlayerPrefs.GetInt("ShowAd") < 3)
        {
            if (doubleBiscuitChance >= 7)
                AdsPanel.SetActive(true);
            else
                PlayerPrefs.SetInt("ShowAd", PlayerPrefs.GetInt("ShowAd") - 1);
            isChanged = true;
        }
        if(!isChanged)
            PlayerPrefs.SetInt("ShowAd", PlayerPrefs.GetInt("ShowAd") - 1);
    }
}

