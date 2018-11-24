using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    
    // load the game scene when a mode is selected
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 1);
    }
}
