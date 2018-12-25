using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour {

    public Texture2D fadeOutTexture;
    public float fadeTime = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    private void Start()
    {
       // SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnGUI()
    {
        alpha += fadeDir * fadeTime * Time.deltaTime;

        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeTime);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BeginFade(-1);
    }

   /* void OnLevelWasLoaded()
    {
        BeginFade(-1);
    }*/

}
