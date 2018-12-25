using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpyImage : MonoBehaviour {

    Image image;
    static bool isBig = false;
    public float scaleSpeed = 0.02f;
    public float scaleSize = 1.25f;
    public string ItemType;

    // Use this for initialization
    void Start () {

        image = GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {

        setImage();
        jumyAni();

    }

    void setImage()
    {
        image.sprite = Resources.Load<Sprite>(ItemType + "/" + PlayerPrefs.GetString("Current_" + ItemType));
    }

    void jumyAni()
    {
        if (image.transform.localScale.x < scaleSize && !isBig)
        {
            Vector3 temp = image.transform.localScale;
            temp.x += scaleSpeed * Time.deltaTime;
            temp.y += scaleSpeed * Time.deltaTime;
            image.transform.localScale = temp;
        }
        if (isBig && image.transform.localScale.x > 1f)
        {
            if (image.transform.localScale.x < 1.02f)
            {
                Vector3 temp = new Vector3(1, 1, 0);
                image.transform.localScale = temp;
                isBig = false;
            }
            else
            {
                Vector3 temp = image.transform.localScale;
                temp.x -= scaleSpeed * Time.deltaTime;
                temp.y -= scaleSpeed * Time.deltaTime;
                image.transform.localScale = temp;
            }

        }
        if (image.transform.localScale.x >= scaleSize)
        {
            isBig = true;
        }
        if (image.transform.localScale.x < 1f)
            isBig = false;
    }
}
