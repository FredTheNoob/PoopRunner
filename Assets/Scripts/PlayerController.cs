using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
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
    
    public bool isDead = false;
    private bool isAccelerated = false;
    
    public float mapRotSpeed = 100f;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // We raycast beneath our player, and if there is a block beneath us:
        /*if (Physics.Raycast(this.transform.position, Vector3.down, 0.5f))
        {
            // We check if the player is on the ground
            if (playerGrounded)
            {
                // He is, then set the y velocity to 0
                rb.velocity = new Vector3(-movementSpeed, 0, rb.velocity.z);
                // And put the player slightly above the ground
                this.transform.position = new Vector3(this.transform.position.x, 0.431076f, this.transform.position.z);
            }
        }
        // If the player is in the air or there is no block beneath us
        else
        {
            // In case the player is on the ground, that means a block isn't beneath us
            if (playerGrounded)
            {
                // Kill the player
                //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
                inHole = true;
            }
            // If the player isn't on the ground he is currently in the air
            else
            {
                rb.velocity = new Vector3(-movementSpeed, rb.velocity.y, rb.velocity.z);
            }
        }*/

        // We start by checking the health of the player
        StartCoroutine(checkHealth());

        // We wish to gradually increase the speed of the player, to make it more difficult. Therefore we add this multiplayer to the movementSpeed every frame
        movementSpeed += speedMultiplier;
        // We set a temp bool to true, since we are now sure that the player is moving
        isAccelerated = true;
        
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

        // We check the z-pos to see if the map needs to be rotated
        CheckZPos();
        
        // If the key R is pressed
        if (Input.GetKey(KeyCode.R)) {
            // The user wants to retry again
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // This IEnumerator is responsible for checking the health of the player, or more specifically if the player has stopped moving
    IEnumerator checkHealth()
    {
        // If the temp bool is still false (which it will be the first frame, to ensure acceleration is built up)
        if (!isAccelerated)
        {
            // We wait for to seconds, to let the player accelerate
            yield return new WaitForSeconds(2);
        }
        
        // If the player doesn't move, he was most likely stopped by an object
        if (rb.velocity.x == 0)
        {
            // Therefore we set the isDead bool to true
            isDead = true;
        }
    }

    // This void is responsible for checking the z position of the player, this is to rotate the map if needed
    private void CheckZPos()
    {
        // If the z is smaller than 0.7
        if (rb.position.z < 0.7f)
        {
            // The user wants to go left, therefore we call a function that does that and parse the Vector3.left argument
            RotateMap(Vector3.left);
            //StartCoroutine(RotateMe(Vector3.left * 15, 0.4f));
        }  
        // If the z is bigger than 8.2
        else if (rb.position.z > 8.2f)
        {
            // User wants to go right, so we parse that to the function
            RotateMap(Vector3.right);
            //StartCoroutine(RotateMe(Vector3.left * -15, 0.4f));
        }
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var import = FindObjectOfType<LaneGenerator>().EmptyObj;
        var fromAngle = import.transform.rotation;
        var toAngle = Quaternion.Euler(import.transform.eulerAngles + byAngles);
        for(var t = 0f; t <= 1; t += Time.deltaTime/inTime) {
            import.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }

        import.transform.rotation = toAngle;
    }
    
    // This function is responsible for the map rotation itself, it takes a direction as an argument
    private void RotateMap(Vector3 _direction)
    {
        // If we find an object that has recently been removed, we just return (to avoid unnecessary errors in the console) - Cleanup
        if (FindObjectOfType<LaneGenerator>().EmptyObj == null) return;

        // This variable is how fast the map will rotate
        var turnSpeed = mapRotSpeed * Time.deltaTime;
        // We then find our EmptyObj, which is a parent of all lanes, and rotate it
        FindObjectOfType<LaneGenerator>().EmptyObj.Rotate(_direction * turnSpeed);
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
