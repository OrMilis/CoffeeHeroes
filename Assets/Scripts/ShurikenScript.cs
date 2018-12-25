using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour {

    public GameObject prefab;
    GameObject prefabClone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            prefabClone = Instantiate(prefab, transform);
            Destroy(prefabClone, 3);
        }
	}
}
