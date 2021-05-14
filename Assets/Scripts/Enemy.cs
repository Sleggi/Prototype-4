using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRB;
    private float speed = 3.0f;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // If enemy falls down from the game field
        if(transform.position.y < -10 )
        {
            Destroy(gameObject);
        }
        // we trying to move enemy from their position to players position
        // so if player is too far way from enemy enemy moves faster becouse of position subtraction we use .normalized for normalizing the
        // magnitude of this vector
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce( lookDirection * speed);
    }
}
