using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySpeedScript : MonoBehaviour {

    private static float speed;
    public const float constSpeed = 4.8f;

    private void Start()
    {
        speed = constSpeed;
    }

    // Update is called once per frame
    void FixedUpdate () {
        //speed = (float) (PlayerScript.forwardSpeed * 0.9);
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
	}

    public static void setSpeed(float speed)
    {
        SkySpeedScript.speed = speed;
    }
}
