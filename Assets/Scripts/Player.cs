using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public float speed;
    public float xMin, xMax, yMin, yMax;
    private Rigidbody rb;

    public GameObject bullet;
    public GameObject PlayerHitParticle;
    public Transform spawn;
    
    public CameraShake cameraShake;
    public AudioSource DamageSound;

    void Start () {
		rb = GetComponent<Rigidbody>();
        DamageSound = GetComponent<AudioSource>();
	}
	
	void Update () {
        // if user presses space key 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // shoot one bolt at the center of the player's ship 
            Instantiate(bullet, spawn.position, spawn.rotation);
        }
    }
    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        rb.velocity = movement * speed;
        
        // take care that the player's ship does not exceed the boundaries
        transform.position = new Vector3
        (
        Mathf.Clamp(transform.position.x, xMin, xMax),
        Mathf.Clamp(transform.position.y, yMin, yMax),
        0.0f
        );
        transform.rotation = Quaternion.Euler(0.0f, 25 * transform.position.x, 0);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // if the player is touched by an enemy's bolt
        if (other.tag == "EnemyBullet")
        {
            DamageSound.Play(); // play a sound
            Instantiate(PlayerHitParticle, gameObject.transform.position, gameObject.transform.rotation); // emit a particle effect
            Destroy(other.gameObject); // destroy the enemy ship
            StartCoroutine(cameraShake.Shake(.15f, .4f)); // shake the camera
            GameObject.FindGameObjectWithTag("GameController").SendMessage("PlayerTakeDamage"); // notify the game controller
        }

        // if the player touches the ground 
        if (other.tag == "Ground")
        {
            DamageSound.Play(); // play a sound
            StartCoroutine(cameraShake.Shake(.15f, .4f)); // shake the camera
            GameObject.FindGameObjectWithTag("GameController").SendMessage("PlayerTakeDamage"); // notify the game controller
        }
    }
}
