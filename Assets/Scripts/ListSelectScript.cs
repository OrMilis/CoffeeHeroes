using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListSelectScript : MonoBehaviour {

    // Use this for initialization
    public static ListSelectScript ins;
    public GameObject Plist;
    public GameObject Slist;
    public GameObject Elist;

    void Awake () {
        ins = this;
        //GetComponent<ScrollRect>().content = GameObject.Find("Display" + XMLManager.ins.selectedFile).gameObject.GetComponent<RectTransform>();
        setShurikenList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setPlayerList()
    {
        if (!Plist.activeSelf)
        {
            Slist.SetActive(false);
            Elist.SetActive(false);
            XMLManager.ins.selectedFile = "Player";
            Plist.SetActive(true);
            Plist.GetComponent<ItemDisplay>().StartCoroutine((ItemDisplay.ins.Reposition()));
            GetComponent<ScrollRect>().content = Plist.GetComponent<RectTransform>();
        }
    }

    public void setShurikenList()
    {
        if (!Slist.activeSelf)
        {
            Plist.SetActive(false);
            Elist.SetActive(false);
            XMLManager.ins.selectedFile = "Shuriken";
            Slist.SetActive(true);
            Slist.GetComponent<ItemDisplay>().StartCoroutine((ItemDisplay.ins.Reposition()));
            GetComponent<ScrollRect>().content = Slist.GetComponent<RectTransform>();
        }
    }

    public void setEnemyList()
    {
        if (!Elist.activeSelf)
        {
            Plist.SetActive(false);
            Slist.SetActive(false);
            XMLManager.ins.selectedFile = "Enemy";
            Elist.SetActive(true);
            Elist.GetComponent<ItemDisplay>().StartCoroutine((ItemDisplay.ins.Reposition()));
            GetComponent<ScrollRect>().content = Elist.GetComponent<RectTransform>();
        }
    }
}
