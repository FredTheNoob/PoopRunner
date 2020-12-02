using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    // This int is how long to wait until the collector starts to collect trash (behind the player)
    public int destroyAfter = 3;

    // This bool is used to check if the IEnumerator finished running
    private bool startDestroying = false;

    // When the game starts
    private void Start() {
        // Start the IEnumerator
        StartCoroutine(DestroyLanes());
    }

    private void OnTriggerEnter(Collider col) {
        // When the bool is true
        if (startDestroying) {
            // Destroy anything that enters the trash collector
            Destroy(col.gameObject);
        }
    }

    IEnumerator DestroyLanes() {
        // Wait some seconds
        yield return new WaitForSeconds(destroyAfter);
        // Set bool to true
        startDestroying = true;
    }
}
