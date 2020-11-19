using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject gameOverUI;

    public Rigidbody rb;
    
    private int score;
    private bool isDead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Starting in 0 seconds.
        // Run StartCounting every 0.1 seconds
        InvokeRepeating("StartCounting", 0f, 0.1f);
        InvokeRepeating("checkHealth", 2f, 0.1f);
    }

    void StartCounting()
    {
        // If the player is still alive   
        if (isDead == false)
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

    void checkHealth()
    {
        // If the player doesn't move, he was most likely stopped by an object
        if (rb.velocity.magnitude < 0.5f)
        {
            // Therefore we set the isDead bool to true
            isDead = true;
            //Time.timeScale = 0;
            //rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
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
