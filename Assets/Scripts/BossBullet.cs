using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

    public float speed;

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0); // set the movement of the boss's bolt
        Destroy(gameObject, 6); // destroy the boss's bolt after 6 seconds 
    }
}
