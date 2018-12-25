using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelScript : MonoBehaviour
{

    Image panel;
    static bool isBig = false;
    public static bool isReverse = false;
    public float scaleSpeed = 5f;
    public float scaleSize = 1.25f;

    // Use this for initialization
    void Start()
    {
        panel = GetComponent<Image>();
        panel.transform.localScale = Vector3.zero;
        StartCoroutine(fixSize());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReverse)
            openPanel();
        else if (isReverse)
            closePanel();
    }

    void openPanel()
    {
        if (panel.transform.localScale.x < scaleSize && !isBig)
        {
            Vector3 temp = panel.transform.localScale;
            temp.x += scaleSpeed * Time.deltaTime;
            temp.y += scaleSpeed * Time.deltaTime;
            panel.transform.localScale = temp;
        }
        if (isBig && panel.transform.localScale.x > 1f)
        {
           if (panel.transform.localScale.x < 1.05f)
            {
                Vector3 temp = new Vector3(1, 1, 0);
                panel.transform.localScale = temp;
            }
            else
            {
                Vector3 temp = panel.transform.localScale;
                temp.x -= scaleSpeed * Time.deltaTime;
                temp.y -= scaleSpeed * Time.deltaTime;
                panel.transform.localScale = temp;
            }
        }
        if (panel.transform.localScale.x >= scaleSize)
        {
            isBig = true;
        }
        /*if (panel.transform.localScale.x < 1f) //
            isBig = false;*/
    }

    void closePanel()
    {
        if (panel.transform.localScale.x < scaleSize && !isBig)
        {
            Vector3 temp = panel.transform.localScale;
            temp.x += scaleSpeed * Time.deltaTime;
            temp.y += scaleSpeed * Time.deltaTime;
            panel.transform.localScale = temp;
        }
        else if (isBig && panel.transform.localScale.x > 0f)
        {
            if (panel.transform.localScale.x <= 0.1f)
            {
                Vector3 temp = Vector3.zero;
                panel.transform.localScale = temp;
            }
            else
            {
                Vector3 temp = panel.transform.localScale;
                temp.x -= scaleSpeed * Time.deltaTime;
                temp.y -= scaleSpeed * Time.deltaTime;
                panel.transform.localScale = temp;
            }
        }
        if (panel.transform.localScale.x >= scaleSize)
        {
            isBig = true;
        }

    }

    IEnumerator fixSize()
    {
        yield return new WaitForSeconds(0.8f);
        if (panel.transform.localScale.x < 1f)
            for (float i = panel.transform.localScale.x; i < 1f; i += scaleSpeed * Time.deltaTime)
            {
                Vector3 fortemp = panel.transform.localScale;
                fortemp.x += i;
                fortemp.y += i;
                panel.transform.localScale = fortemp;
            }
        else if (panel.transform.localScale.x > scaleSize)
            for (float i = panel.transform.localScale.x; i > 1f; i += scaleSpeed * Time.deltaTime)
            {
                Vector3 fortemp = panel.transform.localScale;
                fortemp.x -= i;
                fortemp.y -= i;
                panel.transform.localScale = fortemp;
            }
        panel.transform.localScale = new Vector3(1,1,1);
    }

    public static void doReverse()
    {
        isReverse = !isReverse;
        isBig = false;
    }
}
