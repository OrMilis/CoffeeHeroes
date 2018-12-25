using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class ShurikenPos : MonoBehaviour {

    float speed = 8f;
    GameObject player;
    Transform playerPos;
    Vector3 prePos;
    Vector3 shurikenRot = Vector3.zero;
    public float shurikenRotSpeed = -360f;
    
    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Shuriken/" + PlayerPrefs.GetString("Current_Shuriken"));
    }

    // Use this for initialization
    void Start()
    {
        //sprites = Resources.LoadAll<Sprite>("Assets/Graphics/Shurikens/Throw.png");
       /* Debug.Log(sprites.Length);*/
        //GetComponent<SpriteRenderer>().sprite = sprites[1];
    }
	// Update is called once per frame
	void Update () {
        if (PlayerScript.isDead)
            Destroy(this.gameObject);
       
    }

    void FixedUpdate()
    {
        Vector3 currPos = transform.position;
        currPos.x += (PlayerScript.forwardSpeed * Time.deltaTime) + 2f * Time.deltaTime;
        currPos.y += 2f * Time.deltaTime;
        currPos.z += speed * Time.deltaTime;
        transform.position = currPos;

        shurikenRot.z += shurikenRotSpeed*Time.deltaTime;
        transform.localEulerAngles = shurikenRot;
        
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

}
