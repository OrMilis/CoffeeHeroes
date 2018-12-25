using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownScript : MonoBehaviour {

    RectTransform panel;
    float change = 30f;
    bool isFinishSide = false;
    public bool isUp;
    public bool isRight;
    Vector2 basePos;

	// Use this for initialization
	void Start () {
        panel = GetComponent<RectTransform>();
        basePos = panel.anchoredPosition;
    }
	
	// Update is called once per frame
	void Update () {

        if (isUp)
            goUp();
        else if (isRight)
            goSide();
    }

    void goUp()
    {

        if (panel.anchoredPosition.y <= (basePos.y - 10f) && !isFinishSide)
        {
            change = change * -1;
            isFinishSide = true;
        }
        if (panel.anchoredPosition.y >= (basePos.y + 15f) && !isFinishSide)
        {
            change = change * -1;
            isFinishSide = true;
        }

        if (panel.anchoredPosition.y < basePos.y - 10f)
            panel.anchoredPosition = new Vector2(basePos.x, basePos.y - 10f);

        if (panel.anchoredPosition.y > basePos.y + 15f)
            panel.anchoredPosition = new Vector2(basePos.x, basePos.y + 15f);

        panel.anchoredPosition = new Vector2(basePos.x, panel.anchoredPosition.y + change * Time.deltaTime);

        isFinishSide = false;
    }

    void goSide()
    {

        if (panel.anchoredPosition.x <= (basePos.x - 10f) && !isFinishSide)
        {
            change = change * -1;
            isFinishSide = true;
        }
        if (panel.anchoredPosition.x >= (basePos.x + 15f) && !isFinishSide)
        {
            change = change * -1;
            isFinishSide = true;
        }

        if (panel.anchoredPosition.x < basePos.x - 10f)
            panel.anchoredPosition = new Vector2(basePos.x - 10f, basePos.y);

        if (panel.anchoredPosition.x > basePos.x + 15f)
            panel.anchoredPosition = new Vector2(basePos.x + 15f, basePos.y);

            panel.anchoredPosition = new Vector2(panel.anchoredPosition.x + change * Time.deltaTime, basePos.y);

        isFinishSide = false;
    }
}
