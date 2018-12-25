using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour {

    Text highScoreText;
    bool isActive = false;

	// Use this for initialization
	void Start () {

        isActive = false;
        highScoreText = GetComponent<Text>();
        highScoreText.enabled = !highScoreText.enabled;

	}
	
	// Update is called once per frame
	void Update () {
        if (!isActive)
        {
            if (PlayerScript.isDead)
            {
                isActive = !isActive;
                highScoreText.text = "Best: " + PlayerPrefs.GetInt("hiScoreKey") + "m";
                highScoreText.enabled = !highScoreText.enabled;
            }
        }
	}
}
