using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Text;

public class XMLManager : MonoBehaviour {
    
    public static XMLManager ins;
    public TextAsset defaultFile;
    public FileStream fStream;
    public string fileName;
    public string selectedFile = "Shuriken";

    public ItemDatabase itemDB;
    // Use this for initialization

    void Awake () {
        ins = this;
        DontDestroyOnLoad(this.gameObject);
	}

    private void Start()
    {
        LoadLocalItems();
        updateSave();
    }

    //save
    public void SaveItemsLocal() {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/SaveFile.dat", FileMode.Create);
        bf.Serialize(stream, itemDB);
        stream.Close();

        if (Social.localUser.authenticated)
        {
            FirstLaunchScript.ins.SaveToCloud();
        }
    }  

    public void LoadLocalItems() {
        if (File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
        {
            FileStream stream = new FileStream(Application.persistentDataPath + "/SaveFile.dat", FileMode.Open);
            BinaryFormatter serializer = new BinaryFormatter();
            itemDB = serializer.Deserialize(stream) as ItemDatabase;
            stream.Close();
        }

        else
        {
            StringReader stream = new StringReader(defaultFile.text);
            XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
            itemDB = serializer.Deserialize(stream) as ItemDatabase;
            stream.Close();
        }
        if(!Social.localUser.authenticated)
            SaveItemsLocal();

        setCurrentSkin();
    }

    void updateSave()
    {
        ItemDatabase tempDB;
        StringReader stream = new StringReader(defaultFile.text);
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        tempDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();

        checkList(itemDB.listS, tempDB.listS);
        checkList(itemDB.listP, tempDB.listP);
        checkList(itemDB.listE, tempDB.listE);

        SaveItemsLocal();
    }

    void checkList(List<ItemEntry> itemDBList, List<ItemEntry> tempDBList)
    {
        foreach (ItemEntry itemT in tempDBList)
        {
            bool isExist = false;
            foreach (ItemEntry itemI in itemDBList)
            {
                if (itemI.itemName.Equals(itemT.itemName))
                    isExist = true;
            }
            if (!isExist)
                itemDBList.Add(itemT);
        }
    }

    public void checkSave(List<ItemEntry> cloudItemDBList, List<ItemEntry> localItemDBList)
    {
        foreach (ItemEntry itemLocal in localItemDBList)
        {
            bool isOpen = false;
            foreach (ItemEntry itemCloud in cloudItemDBList)
            {
                if (itemCloud.itemName.Equals(itemLocal.itemName))
                    if(itemCloud.isUnlocked)
                        isOpen = true;
            }
            if (isOpen)
                itemLocal.isUnlocked = true;
        }
    }

    public void setCurrentSkin()
    {
        List<ItemEntry> list = checkList();
        foreach (ItemEntry item in list)
        {
            if (item.itemName.Equals(PlayerPrefs.GetString("Current_" + selectedFile)) && item.isUnlocked)
            {
                item.isCurrent = true;
            }
            else
                item.isCurrent = false;
        }
        XMLManager.ins.SaveItemsLocal();
    }

    List<ItemEntry> checkList()
    {
        if (XMLManager.ins.selectedFile.Equals("Shuriken"))
            return XMLManager.ins.itemDB.listS;
        if (XMLManager.ins.selectedFile.Equals("Player"))
            return XMLManager.ins.itemDB.listP;
        if (XMLManager.ins.selectedFile.Equals("Enemy"))
            return XMLManager.ins.itemDB.listE;
        return null;

    }

    public void selectFilePlayer()
    {
        selectedFile = "Player";
    }

    public void selectFileShuriken()
    {
        selectedFile = "Shuriken";
    }

    public void selectFileEnemy()
    {
        selectedFile = "Enemy";
    }

}

[System.Serializable]
public class ItemEntry {
    public string itemName;
    public bool isUnlocked;
    public bool isCurrent;

    public override string ToString()
    {
        return "Item Name: " + itemName + "isUnlocked: " + isUnlocked + "isCurrent: " + isCurrent;
    }
}

[System.Serializable]
public class ItemDatabase {
    [XmlArray("Shurikens")]
    public List<ItemEntry> listS = new List<ItemEntry>();

    [XmlArray("Players")]
    public List<ItemEntry> listP = new List<ItemEntry>();

    [XmlArray("Enemies")]
    public List<ItemEntry> listE = new List<ItemEntry>();

    override public string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Weapon List:\n");
        foreach (ItemEntry item in listS)
            sb.Append(item.ToString() + "\n");
        sb.Append("Weapon List:\n");
        foreach (ItemEntry item in listP)
            sb.Append(item.ToString() + "\n");
        sb.Append("Weapon List:\n");
        foreach (ItemEntry item in listE)
            sb.Append(item.ToString() + "\n");
        return sb.ToString();
    }
}