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

    private bool inHole = false;
    private bool playerGrounded = true;
    // Update is called once per frame
    void FixedUpdate()
    {
        movementSpeed += Time.deltaTime;

        if (!inHole) {
            rb.velocity = new Vector3(-movementSpeed, rb.velocity.y, rb.velocity.z);
        }       

        if (rb.position.y < 0.22f) {
            inHole = true;
        }

        if (Input.GetKey(KeyCode.A) && !inHole) {
            rb.AddForce(0, 0, -sideWaysSpeed); 
        }
        if (Input.GetKey(KeyCode.D) && !inHole) {
            rb.AddForce(0, 0, sideWaysSpeed); 
        }
        if (Input.GetKey(KeyCode.Space) && !inHole && playerGrounded) {
            rb.AddForce(0, jumpForce, 0);
            playerGrounded = false;
        }

        if (Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Ground") {
            playerGrounded = true;
        }
    }
}
