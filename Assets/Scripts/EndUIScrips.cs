using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndUIScrips : MonoBehaviour {

    Image image;
    bool isActive = false;

	// Use this for initialization
	void Start () {
        isActive = false;
        image = GetComponent<Image>();
        image.enabled = !image.enabled;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!isActive)
            if (PlayerScript.isDead)
            {
                image.enabled = !image.enabled;
                isActive = !isActive;
            }
        if (Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began && isActive)
            {
                SceneManager.LoadScene(0);
            }
        }
	}
}
