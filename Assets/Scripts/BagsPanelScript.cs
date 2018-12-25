using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagsPanelScript : MonoBehaviour {

    public string BagName;
    //public GameObject Arrows;
    public GameObject[] Arrows = new GameObject[3];
    public static bool isSelected;
    Text num;

	// Use this for initialization
	void Start () {
        num = GetComponentInChildren<Text>();
        foreach (GameObject obj in Arrows)
        {
            if (obj.name.Contains(PlayerPrefs.GetString("CurrentBag")))
                obj.SetActive(true);
            else
                obj.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!num.text.Equals(PlayerPrefs.GetInt(BagName + "Bags").ToString()))
            num.text = PlayerPrefs.GetInt(BagName + "Bags").ToString("0#");
        if (PlayerPrefs.GetInt(BagName + "Bags") == 0)
            this.GetComponent<Button>().interactable = false;
        else if (PlayerPrefs.GetInt(BagName + "Bags") > 0 && !this.GetComponent<Button>().interactable)
            this.GetComponent<Button>().interactable = true;
    }

    public void selectBag() {
        PlayerPrefs.SetString("CurrentBag", BagName);
        PlayerPrefs.Save();
        foreach(GameObject obj in Arrows)
        {
            if (obj.name.Contains(PlayerPrefs.GetString("CurrentBag")))
                obj.SetActive(true);
            else
                obj.SetActive(false);
        }
    }
}
