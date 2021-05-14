using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   // indicator of powerUp
    public GameObject powerUpIndicator;

    private Rigidbody playerRB;
    private float speed = 5.0f;

    // camera child of focalPoint
    private GameObject focalPoint;

    private bool powerUpCollected = false;
    private float powerStrength = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        // we trying to move our player in direction of the camera
        focalPoint = GameObject.Find("Focal Point");
        
    }

    // Update is called once per frame
    void Update()
    {
        // getting user input 
        float forwardInput = Input.GetAxis("Vertical");

        // we getting the forward position of there the camera facing
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);

        // setting up the powerupIndictor position equal to player position
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }
    // Checking for collectable item by tag
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            powerUpCollected = true;
            // destroys powerup game object
            Destroy(other.gameObject);
            // Then we pick up powerup the coroutine starts
            StartCoroutine(PowerUpCount());
            // Setting up the powerup object visibility 
            powerUpIndicator.gameObject.SetActive(true);
        }
    }
    // Checking for collision with another game object by tag
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && powerUpCollected)
        {
            // getting the enemy rigid body
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            // creating the Vector direction by subtracting player position from enemy position
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            // applying force to enemy body to move it from player
            enemyRigidBody.AddForce(awayFromPlayer * powerStrength, ForceMode.Impulse);

            powerUpIndicator.gameObject.SetActive(false);
        }
    }

    IEnumerator PowerUpCount()
    {
        // yield to be able to run this timeout
        yield return new WaitForSeconds(7);
        powerUpCollected = false;
    }
}
