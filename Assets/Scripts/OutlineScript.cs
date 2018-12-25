using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour {
    [Range(0,4)]
    public float OutlineSize;

	// Use this for initialization
	void Awake () {
        Vector3 position = this.transform.position;
        position *= OutlineSize;
        this.transform.position = position;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Shuriken/" + PlayerPrefs.GetString("Current_Shuriken"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
