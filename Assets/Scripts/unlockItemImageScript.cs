using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlockItemImageScript : MonoBehaviour {

    Image itemImage;
    string itemName;
    string itemType;

	// Use this for initialization
	void Awake () {

        itemImage = GetComponent<Image>();
        itemName = CoffeeStoreScript.unlockedItemName;
        itemType = CoffeeStoreScript.ItemType;
        itemImage.sprite = Resources.Load<Sprite>(itemType + "/" + itemName);
        itemImage.CrossFadeAlpha(0, 0, false);
        StartCoroutine(startFadeItem());
    }

    IEnumerator startFadeItem()
    {
        yield return new WaitForSeconds(2f);
        itemImage.CrossFadeAlpha(1, 1, false);
    }

}
