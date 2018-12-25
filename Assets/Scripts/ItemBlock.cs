using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemBlock : MonoBehaviour {

    Image image;
    Button btn;
    string itemName;

    void Awake()
    {
        image = FindObjectOfType<Image>().GetComponent<Image>();
        btn = GetComponent<Button>();
    }

    public void Display(ItemEntry item)
    {
        itemName = item.itemName;
        if (item.isUnlocked)
        {
            image.preserveAspect = true;
            image.sprite = Resources.Load<Sprite>(XMLManager.ins.selectedFile + "/" + item.itemName);
        }
        else
        {
            image.enabled = false;
            GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/InvUI/LockedSkin");
            btn.interactable = false;
        }
    }

    public void setCurrentSkin()
    {
        PlayerPrefs.SetString("Current_" + XMLManager.ins.selectedFile, itemName);
        XMLManager.ins.setCurrentSkin();
        //ItemDisplay.ins.startUpdateDisplay();
        XMLManager.ins.LoadLocalItems();
        //ItemDisplay.ins.Display();
    }
}
