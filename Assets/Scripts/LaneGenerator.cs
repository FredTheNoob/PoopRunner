using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LaneGenerator : MonoBehaviour
{
    public GameObject[] startLanes;
    // Array of lanes containing gameobjects
    public GameObject[] lanes;
    // This is the object that will be put
    public GameObject laneObj;
    // A reference to all spawn points (transforms)
    public GameObject laneRef;

    // This is a reference to the spike gameobject
    public GameObject spike;

    // This value determines how many lanes to spawn per second
    public float timerQueue;
    // This is a internal value that counts up with the Time.DeltaTime
    private float timer;
    public float DestroyMapAfter;
    public Transform EmptyObj;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject lane in startLanes)
        {
            Destroy(lane, DestroyMapAfter);
        }
        
        // Generate lanes and do this 50 times (to have a start map)
        for (int i = 0; i < 50; i++)
        {
            // Call the generate function
            Generate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the timer exceeds the threshold
        if (timer >= timerQueue)
        {
            // Call the generate function
            Generate();

            // Reset the timer
            timer = 0;
        }
        else
        {
            // If not keep counting in seconds
            timer += Time.deltaTime;
        }
    }

    // This function takes care of generating the lanes
    private void Generate()
    {
        // Do a foreach loop through the lanes array
        foreach (GameObject lane in lanes)
        {
            // Get a random number between 0 and 2 (33% chance)
            int rnd1 = Random.Range(0, 10);

            // If the number is not 0
            if (rnd1 != 0)
            {
                // Spawn a lane
                GameObject laneChild = (GameObject) Instantiate(laneObj, lane.transform.position, lane.transform.rotation);
                Destroy(laneChild, DestroyMapAfter);

                laneChild.transform.parent = EmptyObj;
                
                // Get another random number between 0 and 4 (20% chance)
                int rnd2 = Random.Range(0, 20);
                // If the number is 0
                if (rnd2 == 0)
                {
                    if (lane.transform.rotation.eulerAngles.y == 180) {
                        GameObject laneChild2 = (GameObject) Instantiate(spike, lane.transform.position + new Vector3(0, -0.5f, 0), lane.transform.rotation);
                        laneChild2.transform.parent = EmptyObj;
                        Destroy(laneChild2, DestroyMapAfter);
                    }
                    else {
                        // Make a spike with an offset of 0.5 of the y-axis
                        GameObject laneChild3 = (GameObject) Instantiate(spike, lane.transform.position + new Vector3(0, 0.5f, 0), lane.transform.rotation);
                        laneChild3.transform.parent = EmptyObj;
                        Destroy(laneChild3, DestroyMapAfter);
                    }
                }
            }
        }

        // Move onto the next lane
        laneRef.transform.position += new Vector3(-laneObj.transform.localScale.x, 0, 0);
    }
}
