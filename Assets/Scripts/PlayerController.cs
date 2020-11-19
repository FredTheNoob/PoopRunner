using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public float movementSpeed;
    public float sideWaysSpeed;
    public float speedMultiplier = 0.01f;

    public float jumpForce;
    
    private bool inHole = false;
    private bool playerGrounded = true;

    public float mapRotSpeed = 100f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.position.y > 0.1f && Input.GetKey(KeyCode.Space) == false)
        {
            rb.AddForce(0, -jumpForce / 50, 0);
        }

        // We wish to gradually increase the speed of the player, to make it more difficult. Therefore we add this multiplayer to the movementSpeed every frame
        movementSpeed += speedMultiplier;
        
        // If the player isn't in a hole
        if (!inHole)
        {
            // We wish to move the player negatively on the x-axis
            rb.velocity = new Vector3(-movementSpeed, rb.velocity.y, rb.velocity.z);
        }
        
        // If the key A is pressed and the player is not in a hole
        if (Input.GetKey(KeyCode.A) && !inHole) {
            // Add a speed on the z, which makes the player move to the left
            rb.AddForce(0, 0, -sideWaysSpeed); 
        }
        // If the key D is pressed
        if (Input.GetKey(KeyCode.D) && !inHole) {
            // Add a speed on the z, to make the player move right
            rb.AddForce(0, 0, sideWaysSpeed); 
        }
        // If space is pressed (the player wants to jump)
        if (Input.GetKey(KeyCode.Space) && !inHole && playerGrounded) {
            // We then add a force on the y-axis, this makes the player go upwards, we use a jumpForce variable, which is public so that we can easily access it in the Inspector
            rb.AddForce(0, jumpForce, 0);
            // We are no longer grounded
            playerGrounded = false;
        }

        // If the z is smaller than 0.7
        if (rb.position.z < 0.7f)
        {
            // The user wants to go left, therefore we call a function that does that and parse the Vector3.left argument
            RotateMap(Vector3.left);
        }  
        // If the z is bigger than 8.2
        else if (rb.position.z > 8.2f)
        {
            // User wants to go right, so we parse that to the function
            RotateMap(Vector3.right);
        }
        
        // If the key R is pressed
        if (Input.GetKey(KeyCode.R)) {
            // The user wants to retry again
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // This void is responsible for checking the z position of the player, this is to rotate the map if needed
    private void RotateMap(Vector3 _direction)
    {
        // If we find an object that has recently been removed, we just return (to avoid unnecessary errors in the console) - Cleanup
        if (FindObjectOfType<LaneGenerator>().EmptyObj == null) return;

        var import = FindObjectOfType<LaneGenerator>().EmptyObj;

        var turnSpeed = mapRotSpeed * Time.deltaTime;
        
        import.transform.Rotate(_direction * turnSpeed);
    }

    // This event listens for collision
    private void OnCollisionEnter(Collision col)
    {
        // If we hit the ground (after a jump)
        if (col.gameObject.tag == "Ground")
        {
            // We are now grounded again
            playerGrounded = true;
        }
    }
}
