using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float horizontalBound = 10f;
    float verticalBound = 6f;
    Vector2 startingPosition;
    float radius = 5f;
    float elapsedTime = 0;
    Rigidbody2D rb;
    Vector2 direction;
    float speed = 3000f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        startingPosition = RandomSpawn();
        transform.position = startingPosition;
        //StartCoroutine(launchObject());
        direction = new Vector2(Screen.width / 2, Screen.height / 2) - startingPosition;
    }

    void Update()
    {
        transform.position = startingPosition + new Vector2(radius * Mathf.Sin(elapsedTime), radius * Mathf.Cos(elapsedTime));
        elapsedTime += Time.deltaTime;
    }

    IEnumerator launchObject()
    {
        yield return new WaitForSeconds(1f);
        rb.AddForce(direction.normalized * speed);
    }

    Vector2 RandomSpawn()
    {
        return new Vector2(Random.Range(-horizontalBound, horizontalBound), -verticalBound);
    }
}
