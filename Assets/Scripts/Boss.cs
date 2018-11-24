using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public GameObject bullet;
    public Transform spawn;
    private float nextFire;
    private float fireRate = 3;
    
    public GameObject EnemyHitParticle;
    
    public AudioSource DamageSound;
    
    private Vector3 InitialPosition;

    void Start ()
    {
        InitialPosition = transform.position;
        DamageSound = GetComponent<AudioSource>();
    }

    void Update ()
    {
        // move towards the player
        if (transform.position.z > (InitialPosition.z - 25))
        {
            transform.Translate(0, 0, 2 * Time.deltaTime);
        }

        // shoot a bullet at a regular interval
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, spawn.position, spawn.rotation);
            Instantiate(bullet, new Vector3(spawn.position.x - 1, spawn.position.y, spawn.position.z + 1), spawn.rotation);
            Instantiate(bullet, new Vector3(spawn.position.x + 1, spawn.position.y, spawn.position.z + 1), spawn.rotation);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // if the boss is touched by a player's bolt
        if (other.tag == "PlayerBullet")
        {
            DamageSound.Play(); // play a sound
            Instantiate(EnemyHitParticle, gameObject.transform.position, gameObject.transform.rotation); // emit a particle effect
            Destroy(other.gameObject); // destroy the player's bolt
            GameObject.FindGameObjectWithTag("GameController").SendMessage("BossTakeDamage"); // notify the game controller
        }
    }
}
