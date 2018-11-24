using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    
    private Vector3 target;
    public float speed;
    
    void Start ()
    {
        target = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(0.2f, 2.3f), -3);
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); // set the movement of the enemy's bolt
        Destroy(gameObject, 6); // destroy the enemy's bolt after 6 seconds 
    }
}
