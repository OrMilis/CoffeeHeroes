using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAniScript : MonoBehaviour {

    public static float armRotation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        armRotation = this.transform.rotation.eulerAngles.z;
        if (armRotation > 45)
            armRotation = armRotation - 360;
    }
}
