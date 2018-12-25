using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryScript : MonoBehaviour {

    Image current;

    // Use this for initialization
    void Start () {
        Screen.SetResolution(720, 1280, true);
        Camera.main.aspect = 0.5625f;

        if (PlayerPrefs.GetInt("noAds") == 0)
        {
            moveUIUp();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            returnToMenu();
        }
    }

    public void returnToMenu()
    {
        ButtonPanelScript.doReverse();
        SceneManager.LoadScene("Menu");
    }

    private void moveUIUp()
    {
        foreach (Transform obj in GameObject.FindGameObjectWithTag("MainCanvas").transform)
        {
            Vector3 temp = obj.gameObject.GetComponent<RectTransform>().localPosition;
            temp.y += 80;
            obj.gameObject.GetComponent<RectTransform>().localPosition = temp;
        }
    }
}
