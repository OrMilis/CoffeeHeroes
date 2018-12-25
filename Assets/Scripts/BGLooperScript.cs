using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLooperScript : MonoBehaviour {

    int BGnumberPanels = 6; //Sprite size = 10.798


    private void Awake()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("Triggered: " + collider.name);

        if (collider.tag == "Enemy")
        {
            //float tempPos = Random.Range(8.4f, 9f);
            /*Vector3 pos = collider.transform.position;
            pos.x = playerTransform.position.x + 6.5f;
            collider.transform.position = pos;
            collider.GetComponent<EnemyPosScript>().isDead = false;*/
        }

        else if (collider.tag.Equals("EnemyChecker")) { }

        else if (collider.tag.Equals("Background"))
        {
            Vector3 pos = collider.transform.position;
            pos.x += 8.4f * BGnumberPanels;
            collider.transform.position = pos;
        }

        else
        {
            float widthOfBGObject = ((BoxCollider)collider).size.x;
            Vector3 pos = collider.transform.position;
            pos.x += widthOfBGObject * BGnumberPanels;
            collider.transform.position = pos;
        }
    }
}
