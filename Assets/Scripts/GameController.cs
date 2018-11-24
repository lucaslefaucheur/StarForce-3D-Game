using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject EnemyShipA;
    public GameObject EnemyShipB;
    public GameObject EnemyShipBoss;
    
    private bool EnemyA, Boss;
    private int nbrOfEnemies, bossLives, playerLives;

    private int score, multiplier;

    public Text scoreText, bossLivesText, playerLivesText, endText;

    void Start () {
        nbrOfEnemies = 0;
        bossLives = 5;
        playerLives = 5;
        multiplier = 1;
        score = 0;
        scoreText.text = score.ToString();
        playerLivesText.text = playerLives.ToString();
        EnemyA = true;
        SpawnWave();
        Boss = false;
	}

	void Update () {
        if (nbrOfEnemies <= 0)
        {
            if (multiplier < 8)
            {
                SpawnWave();
                multiplier *= 2;
            }
            else if (!Boss)
            {
                SpawnBoss();
            }
        }
    }
    
    void SpawnWave ()
    {
        // spawn 5 enemy ships A
        if (EnemyA)
        {
            Instantiate(EnemyShipA, new Vector3(0, 0.5f, 30), EnemyShipA.transform.rotation);
            Instantiate(EnemyShipA, new Vector3(-2, 1.0f, 32), EnemyShipA.transform.rotation);
            Instantiate(EnemyShipA, new Vector3(2, 1.0f, 32), EnemyShipA.transform.rotation);
            Instantiate(EnemyShipA, new Vector3(-4, 1.5f, 34), EnemyShipA.transform.rotation);
            Instantiate(EnemyShipA, new Vector3(4, 1.5f, 34), EnemyShipA.transform.rotation);
            nbrOfEnemies = 5;
            EnemyA = false;
        }

        // spawn 5 enemy ships B
        else
        {
            Instantiate(EnemyShipB, new Vector3(-15, 1.5f, 20), EnemyShipA.transform.rotation);
            Instantiate(EnemyShipB, new Vector3(-17, 1, 18), EnemyShipA.transform.rotation);
            Instantiate(EnemyShipB, new Vector3(-19, 0.5f, 16), EnemyShipA.transform.rotation);
            Instantiate(EnemyShipB, new Vector3(-21, 1, 18), EnemyShipA.transform.rotation);
            Instantiate(EnemyShipB, new Vector3(-23, 1.5f, 20), EnemyShipA.transform.rotation);
            nbrOfEnemies = 5;
            EnemyA = true;
        }
    }
    
    void SpawnBoss ()
    {
        // spawn 1 boss ship 
        Instantiate(EnemyShipBoss, new Vector3(0, 1, 30), EnemyShipBoss.transform.rotation);
        Boss = true;
    }

    void EnemyTakeDamage (int n)
    {
        score += (n * multiplier); // update the score
        scoreText.text = score.ToString();
        nbrOfEnemies--; // reduce the number of enemies
    }

    void PlayerTakeDamage()
    {
        playerLives--;
        playerLivesText.text = playerLives.ToString();
        if (playerLives <= 0) // if the number of lifes of the boss is 0
            End(false); // end the game
    }

    void BossTakeDamage()
    {
        score += (300 * multiplier); // update the score
        scoreText.text = score.ToString();
        bossLives--; // reduce the number of lifes of the boss
        bossLivesText.text = bossLives.ToString();
        if (bossLives <= 0) // if the number of lifes of the boss is 0
            End(true); // end the game
    }
    
    void End(bool win)
    {
        if (win)
            endText.text = "You Win";
        else
            endText.text = "Game Over";
        StartCoroutine(LoadLevelAfterDelay(2));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
