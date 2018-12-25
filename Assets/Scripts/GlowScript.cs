using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowScript : MonoBehaviour {

    Image image;
    public float maxAlpha = 1f;
    public float minAlpha = 0f;
    public float glowDuration = 1f;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (image.canvasRenderer.GetAlpha() >= maxAlpha)
            image.CrossFadeAlpha(minAlpha, glowDuration, false);
        if(image.canvasRenderer.GetAlpha() < minAlpha + 0.10f)
            image.CrossFadeAlpha(maxAlpha, glowDuration, false);
        }
}
