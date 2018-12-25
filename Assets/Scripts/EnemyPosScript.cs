using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosScript : MonoBehaviour {

    public static float speed;
    public const float constSpeed = 2f;
    public bool isDead = false;
    private Vector3 startPos;

    float previousRandom;

    public GameObject player;
    Transform playerTransform;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemy/" + PlayerPrefs.GetString("Current_Enemy"));
        playerTransform = player.transform;
    }

    void Start()
    {
        Vector3 pos = this.transform.position;
        previousRandom = Random.Range(-1f, 1f);
        pos.x += previousRandom;
        this.transform.position = pos;
        startPos = pos;
    }
    // Use this for initialization
    void OnTriggerEnter(Collider collision)
    {
        /* if (collision.tag.Equals("EnemyChecker"))
         {
             Transform thisObj = this.transform;
             Transform colliderObj = collision.transform;
             if(thisObj.position.x > colliderObj.position.x)
             {
                 Vector3 pos = thisObj.position;
                 pos.x += Random.Range(1f, 8f);
                 thisObj.position = pos;
             }
         }*/


        if (collision.tag.Equals("shuriken"))
        {
            isDead = true;
            PlayerPrefs.SetInt("currency", PlayerPrefs.GetInt("currency") + (1 * PlayerPrefs.GetInt("BonusBiscuits")));
            PlayerScript.oneRunBiscuit += 1 * PlayerPrefs.GetInt("BonusBiscuits");
            AchievementManager.ins.killCount += 1;
            AchievementManager.ins.killsAchievements();
        }

        if (collision.tag == "Looper")
        {
            previousRandom += Random.Range(-1 -previousRandom, 1 - previousRandom);

            Vector3 pos = transform.position;

            pos.x = playerTransform.position.x + 8.5f + previousRandom;
            pos.y = startPos.y;

            transform.position = pos;

            isDead = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;

        if (isDead)
            pos.y = startPos.y - this.GetComponent<Renderer>().bounds.size.y / 2;

        transform.position = pos;
    }

    public static void setSpeed(float speed)
    {
        EnemyPosScript.speed = speed;
    }

}
