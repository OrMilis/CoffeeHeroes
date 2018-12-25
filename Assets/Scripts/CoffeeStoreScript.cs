using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

public class CoffeeStoreScript : MonoBehaviour {

    Animator animator;
    bool isAniEnd = true;
    public GameObject unlockedItemPrefab;
    public static string unlockedItemName;
    public static string ItemType;
    public ParticleSystem prati;
    public ScrollRect scrollRect;
    AudioSource CoffeeMachineFX;
    public AudioMixerSnapshot FadeOutSnap;
    public AudioMixerSnapshot ResetSnapshot;

    // Use this for initialization
    void Awake() {
        Screen.SetResolution(720, 1280, true);
        Camera.main.aspect = 0.5625f;

        XMLManager.ins.LoadLocalItems();
        prati.Stop();
        CoffeeMachineFX = GetComponent<AudioSource>();

       if(PlayerPrefs.GetInt("noAds") == 0)
        {
            moveUIUp();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isAniEnd)
        {
            ButtonPanelScript.doReverse();
            SceneManager.LoadScene("Menu");
        }
    }

    public void makeCoffee()
    {
        if (isAniEnd)
        {
            if (PlayerPrefs.GetInt(PlayerPrefs.GetString("CurrentBag") + "Bags") > 0 && checkListNotEmptyBefore(PlayerPrefs.GetString("CurrentBag")))
            {
                isAniEnd = false;
                scrollRect.horizontal = false;
                ResetSnapshot.TransitionTo(0f);
                CoffeeMachineFX.Play();
                prati.Play();
                StartCoroutine(setIsAniEnd());
                destroyChild();
                unlockItem(PlayerPrefs.GetString("CurrentBag"));
                GameObject newItem = Instantiate<GameObject>(unlockedItemPrefab);
                newItem.name = "Unlocked item";
                newItem.transform.SetParent(GameObject.Find("ItemPanel").transform, false);
            }
        }
    }

    IEnumerator setIsAniEnd()
    {
        yield return new WaitForSeconds(2.5f);
        prati.Stop();
        FadeOutSnap.TransitionTo(1f);
        isAniEnd = true;
        scrollRect.horizontal = true;
    }

    public void destroyChild()
    {
        if (GameObject.Find("ItemPanel").transform.childCount != 0) {
            GameObject child = GameObject.Find("ItemPanel").transform.GetChild(0).gameObject;
            foreach (Transform obj in child.transform)
            {
                if (obj.name.Contains("Background"))
                    obj.GetComponent<GlowScript>().enabled = false;
                Image image = obj.gameObject.GetComponent<Image>();
                image.CrossFadeAlpha(0, 0.4f, false);
                Destroy(image, 1f);
            }
            Destroy(child, 1f);
        }
    }


    public void unlockItem(string BagName)
    {
        List<ItemEntry> listS = XMLManager.ins.itemDB.listS;
        List<ItemEntry> listP = XMLManager.ins.itemDB.listP;
        List<ItemEntry> listE = XMLManager.ins.itemDB.listE;
        List<ItemEntry> listPlocked = getLockedItems(listP);
        List<ItemEntry> listSlocked = getLockedItems(listS);
        List<ItemEntry> listElocked = getLockedItems(listE);


        if (BagName.Equals("Throw") && listSlocked.Count > 0)
        {
            unlockInList(listS, listSlocked, "Shuriken");
            PlayerPrefs.SetInt(BagName + "Bags", PlayerPrefs.GetInt(BagName + "Bags") - 1);
        }
        else if (BagName.Equals("Player") && listPlocked.Count > 0)
        {
             unlockInList(listP, listPlocked, "Player");
            PlayerPrefs.SetInt(BagName + "Bags", PlayerPrefs.GetInt(BagName + "Bags") - 1);
        }
        else if (BagName.Equals("Enemy") && listElocked.Count > 0)
        {
           unlockInList(listE, listElocked, "Enemy");
           PlayerPrefs.SetInt(BagName + "Bags", PlayerPrefs.GetInt(BagName + "Bags") - 1);
        }
      XMLManager.ins.SaveItemsLocal();
    }

    void unlockInList(List<ItemEntry> itemList, List<ItemEntry> lockedItemList, string itemType)
    {
        int RNG = UnityEngine.Random.Range(0, lockedItemList.Count);
        int itemPos = itemList.IndexOf(lockedItemList[RNG]);
        itemList[itemPos].isUnlocked = true;
        ItemType = itemType;
        unlockedItemName = itemList[itemPos].itemName.ToString();
    }

    private List<ItemEntry> getLockedItems(List<ItemEntry> itemList)
    {
        List<ItemEntry> list = new List<ItemEntry>();
        foreach (ItemEntry item in itemList)
        {
            if (!item.isUnlocked)
                list.Add(item);
        }
        return list;
    }

    bool checkListNotEmptyBefore(string BagName)
    {
        List<ItemEntry> listS = XMLManager.ins.itemDB.listS;
        List<ItemEntry> listP = XMLManager.ins.itemDB.listP;
        List<ItemEntry> listE = XMLManager.ins.itemDB.listE;
        List<ItemEntry> listPlocked = getLockedItems(listP);
        List<ItemEntry> listSlocked = getLockedItems(listS);
        List<ItemEntry> listElocked = getLockedItems(listE);

        if (BagName.Equals("Throw"))
            if (listSlocked.Count > 0)
                return true;
        if (BagName.Equals("Player"))
            if (listPlocked.Count > 0)
                return true;
        if (BagName.Equals("Enemy"))
            if (listElocked.Count > 0)
                return true;
        return false;
    }

    public void returnToMenu()
    {
        ButtonPanelScript.doReverse();
        SceneManager.LoadScene("Menu");
    }

    public void goToInventory()
    {
        SceneManager.LoadScene("Inventory");
    }


    private void moveUIUp()
    {
        foreach(Transform obj in GameObject.FindGameObjectWithTag("MainCanvas").transform)
        {
            Vector3 temp = obj.gameObject.GetComponent<RectTransform>().localPosition;
            temp.y += 100;
            obj.gameObject.GetComponent<RectTransform>().localPosition = temp;
        }
    }
}
