using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour {

    Image image;
	void Start ()
    {
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (image.name.Equals("FullHeart_3") && PlayerScript.numOfLives == 2)
            image.enabled = false;
        else if (image.name.Equals("FullHeart_2") && PlayerScript.numOfLives == 1)
            image.enabled = false;
        else if (image.name.Equals("FullHeart_1") && PlayerScript.numOfLives == 0)
            image.enabled = false;

        /*if (image.name.Equals("FullHeart_3") || image.name.Equals("FullHeart_2")
                || image.name.Equals("FullHeart_1") && PlayerScript.numOfLives == 3)
            image.enabled = true;
        else if (image.name.Equals("FullHeart_2") || image.name.Equals("FullHeart_1") && PlayerScript.numOfLives == 2)
            image.enabled = true;
        else if (image.name.Equals("FullHeart_1") && PlayerScript.numOfLives == 1)
            image.enabled = true;*/

    }
}
