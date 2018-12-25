using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour {

    // Use this for initialization
    public ItemBlock blockPrefab;
    public static ItemDisplay ins;
    ScrollRect GridBG;
    public string ItemType;

    List<ItemEntry> ItemList;

    private void Awake()
    {
        ins = this;
        GridBG = GameObject.Find("GridBG").GetComponent<ScrollRect>();
    }

	void Start () {
        Display();
        if (this.enabled)
            GridBG.GetComponent<ScrollRect>().content = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public void Display()
    {
        ItemList = checkListToDisplay();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            Debug.Log("Hello");
        }
        foreach (ItemEntry item in ItemList)
        {
            ItemBlock newBlock = Instantiate<ItemBlock>(blockPrefab);
            newBlock.transform.SetParent(transform, false);
            newBlock.name = item.itemName;
            newBlock.Display(item);
        }
        StartCoroutine(Reposition());
    }

    List<ItemEntry> checkListToDisplay()
    {
        if (XMLManager.ins.selectedFile.Equals("Shuriken"))
            return XMLManager.ins.itemDB.listS;
        if (XMLManager.ins.selectedFile.Equals("Player"))
            return XMLManager.ins.itemDB.listP;
        if (XMLManager.ins.selectedFile.Equals("Enemy"))
            return XMLManager.ins.itemDB.listE;
        return null;

    }

   public IEnumerator Reposition()
    {
        yield return new WaitForSeconds(0.01f);
        GridBG.verticalNormalizedPosition = 5f;
        
    }
}
