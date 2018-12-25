using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour {

    ScrollRect scroll;

	// Use this for initialization
	void Start () {
        scroll = GetComponent<ScrollRect>();
        scroll.horizontalNormalizedPosition = -2.2f;
    }
	
	// Update is called once per frame
	void Update () {
        scroll.velocity = new Vector2(-250, 0);

        if (scroll.horizontalNormalizedPosition > 3f)
            scroll.horizontalNormalizedPosition = -2;
    }
}
