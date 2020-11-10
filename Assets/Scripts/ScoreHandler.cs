using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject gameOverUI;
    
    private int score;
    
    // Start is called before the first frame update
    void Start()
    {
        //gameOverUI.SetActive(false);
        // Starting in 0 seconds.
        // Run StartCounting every 0.1 seconds
        InvokeRepeating("StartCounting", 0f, 0.1f);
    }

    void StartCounting()
    {
        // If the player is still alive   
        if (FindObjectOfType<PlayerController>().isDead == false)
        {
            // Keep counting
            score++;
            scoreText.text = score.ToString();
        }
        // If the player is dead
        else
        {
            // Gameover
            gameOverUI.SetActive(true);
        }
    }

    // This listener is for the try again button
    public void btnTryAgain()
    {
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // This listener is for the quit button
    public void btnQuit()
    {
        // Close the application
        Application.Quit();
    }
}
