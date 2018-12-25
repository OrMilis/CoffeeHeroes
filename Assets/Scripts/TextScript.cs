using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    Text distanceText;
    bool isDisable = false;
	// Use this for initialization
	void Start () {

        distanceText = GetComponent<Text>();


        distanceText.GetComponent<Text>().text = PlayerScript.distance.ToString() + " m";

    }
	
	// Update is called once per frame
	void Update () {
        distanceText.GetComponent<Text>().text = PlayerScript.distance.ToString("F0") + " m";
        if (!isDisable)
            if (PlayerScript.isDead)
            {
                distanceText.enabled = !distanceText.enabled;
                isDisable = !isDisable;
            }
    }
}
