using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownScriptGO : MonoBehaviour {

    public float maxY;
    public float minY;
    Transform trans;
    Vector3 basePos;
    private bool isFinishSide = false;
    public float change;

    // Use this for initialization
    void Start () {
        trans = this.transform;
        basePos = trans.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        goUp();
	}

    void goUp()
    {

        if (trans.localPosition.y <= (minY) && !isFinishSide)
        {
            change = change * -1;
            isFinishSide = true;
        }
        if (trans.localPosition.y >= (maxY) && !isFinishSide)
        {
            change = change * -1;
            isFinishSide = true;
        }

        if (trans.localPosition.y < minY)
            trans.localPosition = new Vector3(basePos.x, minY, 0);

        if (trans.localPosition.y > maxY)
            trans.localPosition = new Vector3(basePos.x, maxY, 0);

        trans.localPosition = new Vector3(basePos.x, trans.localPosition.y + change * Time.deltaTime, 0);

        isFinishSide = false;
    }
}
