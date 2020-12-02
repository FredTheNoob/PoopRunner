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

    public Rigidbody[] rigidbodies;
    private Rigidbody selectedBody;
    
    private int score;
    private bool isDead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        int index = FindObjectOfType<SkinSelector>().boughtIndex;

        switch (index)
        {
            case 0:
                //DEFAULT
                selectedBody = rigidbodies[0];
                break;

            case 1:
                // TOILETPAPER
                selectedBody = rigidbodies[1];
                break;

            case 2:
                // HEADSET
                selectedBody = rigidbodies[2];
                break;

            case 3:
                // HAT
                selectedBody = rigidbodies[3];
                break;
        }

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
        if (selectedBody.velocity.magnitude < 0.5f)
        {
            Time.timeScale = 0;
            FindObjectOfType<AudioManager>().Play("hit");
            // Therefore we set the isDead bool to true
            isDead = true;
            selectedBody.velocity = new Vector3(0f, selectedBody.velocity.y, selectedBody.velocity.z);
            
            // Stop running this method
            CancelInvoke("checkHealth");
        }
    }

    // This listener is for the try again button
    public void btnTryAgain()
    {
        Time.timeScale = 1;
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void btnShop() {
        SceneManager.LoadScene("MainMenu");
    }

    // This listener is for the quit button
    public void btnQuit()
    {
        // Close the application
        Application.Quit();
    }
}
