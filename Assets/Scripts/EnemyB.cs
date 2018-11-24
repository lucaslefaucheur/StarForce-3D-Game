using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour {

    public GameObject bullet;
    public Transform spawn;
    private float nextFire;

    public GameObject EnemyHitParticle;
    private AudioSource DamageSound;
    private Vector3 InitialPosition;

    private int phase;
    bool die = false;

    void Start ()
    {
        phase = 0;
        InitialPosition = transform.position;
        DamageSound = GetComponent<AudioSource>();
    }

    void Update ()
    {
        // if the enemy has been touched
        if (die)
        {
            // fall to the ground
            if (transform.position.y > 0.2f)
            {
                transform.Translate(0, -1 * Time.deltaTime, 0);
            }
            else
            {
                transform.Translate(0, 0, 3 * Time.deltaTime);
                Destroy(gameObject, 10.0f); // destroy the enemy ship
            }
        }
        else
        {
            // move from the left side to the right side
            if (phase == 0)
            {
                transform.Translate(-5 * Time.deltaTime, 0, 0);
                if (transform.position.x >= 13)
                {
                    transform.position += new Vector3(0, 0, -6);
                    phase = 1;
                }
            }

            // move from the right side to the left side 
            if (phase == 1)
            {
                transform.Translate(5 * Time.deltaTime, 0, 0);
                if (transform.position.x <= -13)
                {
                    transform.position += new Vector3(0, 0, -5);
                    phase = 2;
                }
                // TODO: Instantiate a bullet (ONLY ONE)
            }

            // move from the left side to the midlle
            if (phase == 2)
            {
                transform.Translate(-5 * Time.deltaTime, 0, 0);
                if (transform.position.x >= InitialPosition.x + 19)
                {
                    phase = 3;
                }
            }

            if (phase == 3)
            {
                // shoot a bullet at a random interval 
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + Random.Range(2, 6);
                    Instantiate(bullet, spawn.position, spawn.rotation);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if an enemy ship is touched by a player's bolt 
        if (other.tag == "PlayerBullet" && die == false)
        {
            DamageSound.Play(); // play a sound 
            Instantiate(EnemyHitParticle, gameObject.transform.position, gameObject.transform.rotation); // emit a particle effect
            Destroy(other.gameObject); // destroy the player's bolt
            die = true;
            GameObject.FindGameObjectWithTag("GameController").SendMessage("EnemyTakeDamage", 200); // notify the game controller
        }
    }
}
