using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShurikenUIScript : MonoBehaviour {

    Image image;

	// Use this for initialization
	void Start () {

        image = GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {

        if (image.name == "FullShuriken_3" && PlayerScript.numOfShots == 2)
            image.enabled = false;
        if (image.name == "FullShuriken_2" && PlayerScript.numOfShots == 1)
            image.enabled = false;
        if (image.name == "FullShuriken_1" && PlayerScript.numOfShots == 0)
            image.enabled = false;

        switch (PlayerScript.numOfShots)
        {
            case 3:
                if (image.name == "FullShuriken_3")
                    image.enabled = true;
                break;
            case 2:
                if (image.name == "FullShuriken_2")
                    image.enabled = true;
                break;
            case 1:
                if (image.name == "FullShuriken_1")
                    image.enabled = true;
                break;
        }
            

    }
}
