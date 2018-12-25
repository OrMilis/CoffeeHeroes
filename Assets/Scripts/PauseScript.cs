using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    public static PauseScript ins;

    public GameObject ClickObj;

    public GameObject PausePanel;
    public GameObject PauseBtn;
    public GameObject PauseAudio;
    public Sprite AudioOn;
    public Sprite AudioOff;

    // Use this for initialization
    void Awake () {
        ins = this;
	}
	
    public void returnToMenu()
    {
        Time.timeScale = 1;
        FirstLaunchScript.ins.isPaused = false;
        ButtonPanelScript.doReverse();
        SceneManager.LoadScene("Menu");
    }

    public void changeAudio()
    {
        bool isMuted = FirstLaunchScript.ins.isMuted;
        if (!isMuted)
        {
            FirstLaunchScript.ins.isMuted = true;
        }
        else if (isMuted)
        {
            FirstLaunchScript.ins.isMuted = false;
        }
        checkAudio();
        FirstLaunchScript.ins.muteUnmute();
    }

    public void checkAudio()
    {
        Image btnImage = PauseAudio.GetComponent<Image>();
        bool isMuted = FirstLaunchScript.ins.isMuted;
        if (isMuted)
            btnImage.sprite = AudioOff;
        else
            btnImage.sprite = AudioOn;
    }
}
