using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateScript : MonoBehaviour {


    Image image;
    public int rotationSpeed = 20;
    Vector3 baseRot = Vector3.zero;

	// Use this for initialization
	void Awake() {
        image = GetComponent<Image>();
        image.CrossFadeAlpha(0, 0, false);
        StartCoroutine(activateScript());
	}
	
	// Update is called once per frame
	void Update () {
        baseRot.z += rotationSpeed * Time.deltaTime;
        image.rectTransform.localEulerAngles = baseRot ;
	}

    IEnumerator activateScript()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<GlowScript>().enabled = true;
    }
}
