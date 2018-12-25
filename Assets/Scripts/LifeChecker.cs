using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeChecker : MonoBehaviour {

    public bool godMode = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
                PlayerScript.addShot();

            if (!collider.GetComponent<EnemyPosScript>().isDead)
            {
                if (!godMode)
                {
                    PlayerScript.numOfLives--;
                }
            }
        }
    }
}
