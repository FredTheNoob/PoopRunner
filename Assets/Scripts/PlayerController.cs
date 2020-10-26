using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public float movementSpeed;
    public float sideWaysSpeed;

    public float jumpForce;

    private float timer;

    // Update is called once per frame
    void FixedUpdate()
    {

        movementSpeed += Time.deltaTime;

        rb.velocity = new Vector3(-movementSpeed, rb.velocity.y, rb.velocity.z);

        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(0, 0, -sideWaysSpeed); 
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(0, 0, sideWaysSpeed); 
        }
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddForce(0, jumpForce, 0); 
        }

        if (Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
