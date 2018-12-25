using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerView : MonoBehaviour {

    Transform player;
    public AudioMixerSnapshot startSnap;
    public AudioMixerSnapshot midSnap;
    public AudioMixerSnapshot endSnap;
    public GameObject game;
    public GameObject note;
    bool isEnd;
    float offsetX;
    float volume;


    // Use this for initialization
    void Start () {

        Screen.SetResolution(720, 1280, true);
        Camera.main.aspect = 0.5625f;

        GameObject player_go = GameObject.FindGameObjectWithTag("Player");

        startSnap.TransitionTo(0.01f);
        isEnd = false;

        if (player_go == null)
        {
            Debug.LogError("Cant find object with Tag!");
            return;
        }

        player = player_go.transform;
        offsetX =  transform.position.x - player.position.x;

        StartCoroutine(snapToMid());
	}
	
	// Update is called once per frame
	void Update () {
       
        if (player != null)
        {
            Vector3 pos = transform.position;
            pos.x = player.position.x + offsetX;
            transform.position = pos;
        }

        if (PlayerScript.isDead && !isEnd)
            StartCoroutine(snapToEnd());

	}

    IEnumerator snapToMid()
    {
        yield return new WaitForSeconds(1.4f);
        midSnap.TransitionTo(3f);
        yield return new WaitForSeconds(0.4f);
        note.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        game.GetComponent<AudioSource>().Play();
    }

    IEnumerator snapToEnd()
    {
        yield return null;
        isEnd = true;
        endSnap.TransitionTo(1f);
    }
}
