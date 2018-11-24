using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    void Update () {
        // move the background image down -> to give the illusion that we are moving up
        transform.Translate(0, -3 * Time.deltaTime, 0);
        
        // reposition the background image on top of the other -> to give the illusion of inifite background
        if (transform.position.z <= -10)
        {
            transform.position += new Vector3(0, 0, 40);
        }
    }
}
