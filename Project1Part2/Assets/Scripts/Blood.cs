using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private float refillAmount = 1;
    private Rigidbody2D rb;
    private int forceX = 40;
    private int forceY = 40;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.value * forceX, Random.value * forceY));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.FeedPlayer(refillAmount);
            Destroy(this.gameObject);
        }
    }
}
