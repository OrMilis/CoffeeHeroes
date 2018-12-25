using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   /* void OnTriggerEnter(Collider other)
    {

        Transform enemy = this.transform.parent.transform;
        Transform otherEnemy = other.transform.parent.transform;

        if (other.tag.Equals("EnemyChecker"))
        {
            if (!enemy.GetComponent<EnemyPosScript>().isDead && enemy.position.x > otherEnemy.position.x)
            {
                Vector3 pos = enemy.position;
                pos.x += Random.Range(1f, 8f);
                enemy.position = pos;
                Debug.Log("Teleport: " + this.gameObject.name);
            }
        }
    }*/
}
